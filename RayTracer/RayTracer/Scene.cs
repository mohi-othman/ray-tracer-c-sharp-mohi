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
        public Color BackgroundColor { get; set; }

        public Scene(int sizeX, int sizeY, Camera sceneCamera, Color ambientLight, List<Primitive> sceneObjects, List<LightSource> sceneLights, Shaders.Shader sceneShader, Color backgroundColor)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SceneCamera = sceneCamera;
            SceneObjects = sceneObjects;
            SceneLights = sceneLights;
            AmbientLight = ambientLight;
            SceneShader = sceneShader;
            BackgroundColor = backgroundColor;
        }

        public View Render()
        {
            var result = SceneCamera.GetView(SizeX, SizeY);
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
                    var lightDir = ((1 / t) * dist).Normalize();
                    var lightRay = new Ray(collision.HitPoint + lightDir * Globals.epsilon, lightDir);

                    //Check if object in shadows
                    var shadowCollision = Trace(lightRay);

                    if (!shadowCollision.IsCollision)
                    {
                        newColor += SceneShader.GetColor(collision.HitObject, light, ray.Direction, lightRay.Direction, normal, AmbientLight);
                    }
                }

                //Reflection
                if (collision.HitObject.Material.ReflectionCoeff > 0 && level <= Globals.maxDepth)
                {
                    var reflectionDirection = (ray.Direction - (2 * (ray.Direction * normal) * normal)).Normalize();
                    var reflectionRay = new Ray(collision.HitPoint + reflectionDirection * Globals.epsilon, reflectionDirection);

                    var newPoint = RayTrace(reflectionRay, refractionIndex, level + 1);

                    //newColor += newPoint.color * collision.HitObject.Material.ReflectionCoeff * collision.HitObject.Material.Color;
                    newColor += newPoint.color * collision.HitObject.Material.ReflectionCoeff * collision.HitObject.Material.DiffuseColor;
                }

                //Refraction
                if (collision.HitObject.Material.RefractionIndex > 0 && level <= Globals.maxDepth)
                {
                    var n = refractionIndex / collision.HitObject.Material.RefractionIndex;
                    var cosI = -(normal * ray.Direction);
                    var cosT2 = 1 - n * n * (1 - cosI * cosI);
                    if (cosT2 > 0)
                    {
                        var refractionDirection = (n * ray.Direction) + (n * cosI - Math.Sqrt(cosT2)) * normal;
                        var refractionRay = new Ray(collision.HitPoint + refractionDirection * Globals.epsilon, refractionDirection);
                        var refractionPoint = RayTrace(refractionRay, collision.HitObject.Material.RefractionIndex, level+1);
                        var absorbance = collision.HitObject.Material.DiffuseColor * .15 * -refractionPoint.depth;
                        var transparency = new Color(Math.Exp(absorbance.R), Math.Exp(absorbance.G), Math.Exp(absorbance.B));
                        newColor += refractionPoint.color * transparency;
                    }
                }

                return new View.Point { color = newColor, depth = collision.Distance };
            }
            else
                return new View.Point { color = BackgroundColor, depth = Globals.infinity };
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

