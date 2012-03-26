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
        public Camera SceneCamera { get; set; }
        public List<SolidObject> SceneObjects { get; set; }
        public List<LightSource> SceneLights { get; set; }
        public Vector3D TopLeftOrigin { get; set; }

        public Scene(int sizeX, int sizeY, Camera sceneCamera, List<SolidObject> sceneObjects, List<LightSource> sceneLights)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            SceneCamera = sceneCamera;
            SceneObjects = sceneObjects;
            SceneLights = sceneLights;
        }

        public View Render()
        {
            var result = new View(SizeX, SizeY);
            var level = 0;

            for (double x = 0; x < SizeX; x++)
            {
                for (double y = 0; y < SizeY; y++)
                {
                    var targetPixel = new Vector3D(x, y, 0);
                    var ray = SceneCamera.GenerateRay(targetPixel);

                    var collision = RayTrace(ray, level);

                    if (collision.IsCollision)
                    {
                        //Lighting
                        var normal = collision.GetNormal();
                        double red = 0;
                        double blue = 0;
                        double green = 0;

                        foreach (LightSource light in SceneLights)
                        {
                            var dist = light.Location - collision.HitPoint;
                            if (normal * dist <= 0)
                                continue; //light source is behind 

                            var t = Math.Sqrt(dist * dist);
                            if (t < 0)
                                continue; //Avoid division by zero

                            var lightRay = new Ray(collision.HitPoint, (1 / t) * dist);

                            //Check if object in shadows
                            var shadowCollision = RayTrace(lightRay, level);
                            if (!shadowCollision.IsCollision)
                            {
                                //lambert
                                var lambert = (lightRay.Direction * normal) * collision.HitObject.Material.LambertCoeff; //Lambertian coeffecient
                                red += lambert * (collision.HitObject.Color.R / 255) * (light.Color.R / 255);
                                green += lambert * (collision.HitObject.Color.G / 255) * (light.Color.G / 255);
                                blue += lambert * (collision.HitObject.Color.B / 255) * (light.Color.B / 255);
                            }
                        }

                        var d = Vector3D.Distance(collision.HitPoint, ray.Origin);
                        var r = (int)Math.Ceiling(red) * 255;
                        var g = (int)Math.Ceiling(green) * 255;
                        var b = (int)Math.Ceiling(blue) * 255;
                        result.Pixels[(int)x, (int)y] = new View.Point { color = new Color(r, g, b), depth = d };
                    }
                    else
                        result.Pixels[(int)x, (int)y] = new View.Point { color = new Color(), depth = Globals.infinity };

                }
            }

            return result;
        }

        public Collision RayTrace(Ray ray, int level)
        {
            var minT = Globals.infinity;
            SolidObject hitObject = null;

            foreach (SolidObject obj in SceneObjects)
            {
                var t = obj.Intersection(ray);
                if (t != SolidObject.NoColision && t < minT)
                {
                    minT = t;
                    hitObject = obj;
                }
            }

            if (hitObject == null)
                return new Collision(false, null, null);
            else
            {
                var hitPoint = ray.Origin + minT * ray.Direction;
                return new Collision(true, hitPoint, hitObject);
            }

        }

    }
}

