﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Scene
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public double PixelSize { get; set; }
        public Color AmbientLight { get; set; }
        public Camera SceneCamera { get; set; }
        public List<Primitive> SceneObjects { get; set; }
        public List<Light> SceneLights { get; set; }
        public Vector3D TopLeftOrigin { get; set; }
        public Shaders.Shader SceneShader { get; set; }
        public Color BackgroundColor { get; set; }

        public Scene(int sizeX, int sizeY, double pixelSize, Camera sceneCamera, Color ambientLight, List<Primitive> sceneObjects, List<Light> sceneLights, Shaders.Shader sceneShader, Color backgroundColor)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SceneCamera = sceneCamera;
            SceneObjects = sceneObjects;
            SceneLights = sceneLights;
            AmbientLight = ambientLight;
            SceneShader = sceneShader;
            BackgroundColor = backgroundColor;
            PixelSize = pixelSize;
        }

        public View Render()
        {
            var result = SceneCamera.GetView(SizeX, SizeY, PixelSize);
            var level = 0;

            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    var targetPixel = result.Pixels[x, y].realCoordinate;

                    var ray = SceneCamera.GenerateRay(targetPixel);
                                        
                    var point = RayTrace(ray, 1, level);

                    result.Pixels[(int)x, (int)y] = point;

                }
            }

            return result;
        }

        public View.Point RayTrace(Ray ray, double refractionIndex, int level)
        {
            var collision = Trace(ray);

            if (collision.IsCollision)
            {
                //Lighting
                var hitPoint = collision.HitPoint;
                var normal = collision.Normal;
                var color = new Color();

                foreach (Light light in SceneLights)
                {
                    var lightDir = light.GetLightDirection(hitPoint);
                    var lightRay = new Ray(hitPoint + lightDir * Globals.epsilon, lightDir);
                    
                    //Check if object in shadows
                    var shadowCollision = Trace(lightRay);
                   
                    if (!shadowCollision.IsCollision)                    
                    {
                        color += SceneShader.GetColor(collision.HitObject, light, ray.Direction, lightRay.Direction, normal);
                    }
                }

                //Reflection
                if (collision.HitObject.Material.ReflectionCoeff > 0 && level <= Globals.maxDepth)
                {
                    var reflectionDirection = (ray.Direction - (2 * (ray.Direction * normal) * normal)).Normalize();
                    var reflectionRay = new Ray(hitPoint + reflectionDirection * Globals.epsilon, reflectionDirection);

                    var reflectionPoint = RayTrace(reflectionRay, refractionIndex, level + 1);
                                        
                    color += reflectionPoint.color * collision.HitObject.Material.ReflectionCoeff * collision.HitObject.Material.DiffuseColor;
                }

                //Refraction
                if (collision.HitObject.Material.RefractionCoeff > 0 && level <= Globals.maxDepth)
                {
                    var refIndex = refractionIndex / collision.HitObject.Material.RefractionIndex;
                    var refNormal = normal * (collision.IsInsidePrimitive ? -1 : 1);

                    var cosI = -(refNormal * ray.Direction);
                    var sinT2 = refIndex * refIndex * (1 - cosI * cosI);
                    if (sinT2 <= 1)
                    {
                        var cosT = Math.Sqrt(1 - sinT2);
                        var refractionDirection = (refIndex * ray.Direction) + (refIndex * cosI - cosT) * refNormal;
                        var refractionRay = new Ray(hitPoint + refractionDirection * Globals.epsilon, refractionDirection);
                        var refractionPoint = RayTrace(refractionRay, collision.HitObject.Material.RefractionIndex, level + 1);

                        var absorbance = collision.HitObject.Material.DiffuseColor * 0.15 * -refractionPoint.depth;
                        var transparency = new Color(Math.Exp(absorbance.R), Math.Exp(absorbance.G), Math.Exp(absorbance.B));
                        color += refractionPoint.color * transparency;
                    }
                }

                color += AmbientLight;

                return new View.Point { color = color, depth = collision.Distance };
            }
            else
                return new View.Point { color = BackgroundColor, depth = Globals.infinity };
        }

        public Collision Trace(Ray ray)
        {
            var minT = Globals.infinity;
            Collision hit = null;

            foreach (Primitive obj in SceneObjects)
            {
                var c = obj.Intersection(ray);
                if (c.IsCollision && c.Distance < minT)
                {
                    minT = c.Distance;
                    hit = c;
                }
            }

            if (hit == null)
                return new Collision(false);
            else
            {
                return hit;
            }

        }



    }
}

