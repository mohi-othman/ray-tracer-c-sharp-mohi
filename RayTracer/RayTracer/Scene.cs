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

            for (double x = 0; x < SizeX; x++)
            {
                for (double y = 0; y < SizeY; y++)
                {
                    var targetPixel = new Vector3D(x, y, 0);
                    var ray = SceneCamera.GenerateRay(targetPixel);

                    var collision = Trace(ray);

                    if (collision.IsCollision)
                    {
                        var d = Vector3D.Distance(collision.HitPoint, ray.Origin);
                        var c = (int)Math.Ceiling(((1000 - d) / 100) * 255);

                        if (c < 0)
                            c = 0;
                        if (c > 255)
                            c = 255;
                        result.Pixels[(int)x, (int)y] = new View.Point { color = new Color(c, c, c), depth = d };
                    }
                    else
                        result.Pixels[(int)x, (int)y] = new View.Point { color = new Color(), depth = Globals.infinity };

                }
            }

            return result;
        }

        public Collision Trace(Ray ray)
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

