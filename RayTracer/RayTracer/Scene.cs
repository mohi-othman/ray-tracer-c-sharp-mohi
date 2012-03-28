using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Scene
    {
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public Color AmbientLight { get; set; }
        public Camera SceneCamera { get; set; }
        public List<Primitive> SceneObjects { get; set; }
        public List<LightSource> SceneLights { get; set; }
        public Vector3D TopLeftOrigin { get; set; }
        public Shaders.Shader SceneShader { get; set; }

        const double worldHeight = 6.0;
        const double worldWidth = 8.0;

        public Scene(int sizeX, int sizeY, Camera sceneCamera, Color ambientLight, List<Primitive> sceneObjects, List<LightSource> sceneLights, Shaders.Shader sceneShader)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SceneCamera = sceneCamera;
            SceneObjects = sceneObjects;
            SceneLights = sceneLights;
            AmbientLight = ambientLight;
            SceneShader = sceneShader;
        }

        public View Render()
        {
            var result = new View(SizeX, SizeY);
            var level = 0;

            var xd = worldWidth / SizeX;
            var yd = worldHeight / SizeY;

            for (double x = 0; x < SizeX; x++)
            {
                for (double y = 0; y < SizeY; y++)
                {
                    var wx = x * xd - worldWidth / 2;
                    var wy = worldHeight / 2 - y * yd;

                    var targetPixel = new Vector3D(wx, wy, 0);

                    var ray = SceneCamera.GenerateRay(targetPixel);

                    var point = RayTrace(ray, level);

                    result.Pixels[(int)x, (int)y] = point;

                }
            }

            return result;
        }

        public View.Point RayTrace(Ray ray, int level)
        {
            var collision = Trace(ray);

            if (collision.IsCollision)
            {
                //Lighting
                var normal = collision.GetNormal();

                var newColor = new Color();

                foreach (LightSource light in SceneLights)
                {
                    var dist = light.Location - collision.HitPoint;
                    if (normal * dist <= 0)
                        continue; //light source is behind 

                    var t = Math.Sqrt(dist * dist);
                    if (t < 0)
                        continue; //Avoid division by zero
                    var lightDir = (1 / t) * dist;
                    var lightRay = new Ray(collision.HitPoint + lightDir * Globals.epsilon, lightDir);

                    //Check if object in shadows
                    var shadowCollision = Trace(lightRay);

                    if (!shadowCollision.IsCollision)
                    {
                        newColor += SceneShader.GetColor(collision.HitObject, light, ray.Direction, lightRay.Direction, normal);
                    }
                }

                //Reflection
                if (collision.HitObject.Material.ReflectionCoeff > 0 && level <= Globals.maxDepth)
                {
                    var reflectionDirection = ray.Direction - (2 * (ray.Direction * normal) * normal);
                    var reflectionRay = new Ray(collision.HitPoint + reflectionDirection * Globals.epsilon, reflectionDirection);

                    var newPoint = RayTrace(reflectionRay, level + 1);

                    //newColor += newPoint.color * collision.HitObject.Material.ReflectionCoeff * collision.HitObject.Material.Color;
                    newColor += newPoint.color * collision.HitObject.Material.ReflectionCoeff * collision.HitObject.Material.Color;
                }

                //Refraction
                if (collision.HitObject.Material.RefractionCoeff > 0 && level <= Globals.maxDepth)
                {
                }

                newColor += AmbientLight;

                return new View.Point { color = newColor, depth = collision.Distance };
            }
            else
                return new View.Point { color = new Color(), depth = Globals.infinity };
        }

        public Collision Trace(Ray ray)
        {
            var minT = Globals.infinity;
            Primitive hitObject = null;

            foreach (Primitive obj in SceneObjects)
            {
                var t = obj.Intersection(ray);
                if (t != Primitive.NoColision && t < minT)
                {
                    minT = t;
                    hitObject = obj;
                }
            }

            if (hitObject == null)
                return new Collision(false, null, null, 0);
            else
            {
                var hitPoint = ray.Origin + minT * ray.Direction;
                var distance = Vector3D.Distance(hitPoint, ray.Origin);
                return new Collision(true, hitPoint, hitObject, distance);
            }

        }



    }
}

