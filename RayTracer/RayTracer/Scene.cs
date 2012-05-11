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
        public double PixelSize { get; set; }
        public Color AmbientLight { get; set; }
        public Camera SceneCamera { get; set; }
        public List<Primitive> SceneObjects { get; set; }
        public List<Light> SceneLights { get; set; }
        public Vector3D TopLeftOrigin { get; set; }
        public Shaders.Shader SceneShader { get; set; }
        public Color BackgroundColor { get; set; }
        public double TransformationScalingFactor { get; set; }

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

            TransformationScalingFactor = 1;//default
        }

        public View Render()
        {
            var result = SceneCamera.GetView(SizeX, SizeY, PixelSize);
            var level = 0;

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    var targetPixel = result.Pixels[x, y].realCoordinate;

                    var ray = SceneCamera.GenerateRay(targetPixel);

                    var point = RayTrace(ray, 1, level);

                    result.Pixels[(int)x, (int)y] = point;

                }
            }

            return result;
        }

        public List<View> Animate(double timeFrame, int fps)
        {
            var frameCount = (int)Math.Floor(timeFrame * fps);
            var deltaScale = 1.0 / (frameCount-1);
            var currentScale = 0.0;

            var result = new List<View>();

            for (int i = 0; i < frameCount; i++)
            {                
                TransformationScalingFactor = currentScale;
                var v = Render();
                result.Add(v);
                currentScale += deltaScale;
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
                    var lightDir = light.GetLightDirection(hitPoint).Normalize();
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
                    color += RenderReflection(ray, normal, hitPoint, collision.HitObject, refractionIndex, level);
                }

                //Refraction
                if (collision.HitObject.Material.RefractionIndex > 0 && level <= Globals.maxDepth)
                {
                    color += RenderRefraction(ray, normal, hitPoint, collision.HitObject, refractionIndex, level, collision.IsInsidePrimitive);
                }

                color += AmbientLight;

                return new View.Point { color = color, depth = collision.Distance };
            }
            else
                return new View.Point { color = BackgroundColor, depth = Globals.infinity };
        }

        public Color RenderReflection(Ray ray, Vector3D normal, Vector3D hitPoint, Primitive hitObject, double refractionIndex, int level)
        {
            var reflectionDirection = (ray.Direction - (2 * (ray.Direction * normal) * normal)).Normalize();
            var reflectionRay = new Ray(hitPoint + reflectionDirection * Globals.epsilon, reflectionDirection);

            var reflectionPoint = RayTrace(reflectionRay, refractionIndex, level + 1);

            return reflectionPoint.color * hitObject.Material.ReflectionCoeff * hitObject.Material.DiffuseColor;
        }

        public Color RenderRefraction(Ray ray, Vector3D normal, Vector3D hitPoint, Primitive hitObject, double refractionIndex, int level, bool isInsidePrimitive)
        {
            var refIndex = refractionIndex / hitObject.Material.RefractionIndex;
            var refNormal = normal * (isInsidePrimitive ? -1 : 1);

            var cosI = -(refNormal * ray.Direction);
            var sinT2 = refIndex * refIndex * (1 - cosI * cosI);
            if (sinT2 <= 1)
            {
                var cosT = Math.Sqrt(1 - sinT2);
                var refractionDirection = (refIndex * ray.Direction) + (refIndex * cosI - cosT) * refNormal;
                var refractionRay = new Ray(hitPoint + refractionDirection * Globals.epsilon, refractionDirection);
                var refractionPoint = RayTrace(refractionRay, hitObject.Material.RefractionIndex, level + 1);

                var absorbance = hitObject.Material.DiffuseColor * 0.15 * -refractionPoint.depth;
                var transparency = new Color(Math.Exp(absorbance.R), Math.Exp(absorbance.G), Math.Exp(absorbance.B));
                return refractionPoint.color * transparency;
            }
            return new Color();
        }

        public Collision Trace(Ray ray)
        {
            var minT = Globals.infinity;
            Collision hit = null;

            foreach (Primitive obj in SceneObjects)
            {
                var intersectRay = new Ray(ray.Origin, ray.Direction);

                foreach (TransformationMatrix t in obj.Transformations)
                {
                    var transform = t.ScaleMatrix(TransformationScalingFactor);

                    var invTransform = transform.GetInverse();
                    var dirMatrix = new Vector3DMatrix();
                    var originMatrix = new Vector3DMatrix();

                    dirMatrix.FromVector(intersectRay.Direction);
                    originMatrix.FromPoint(intersectRay.Origin);

                    var invDir = dirMatrix.Transform(invTransform);
                    var invOrigin = originMatrix.Transform(invTransform);

                    intersectRay.Direction = invDir.ToVector3D();
                    intersectRay.Origin = invOrigin.ToVector3D();
                }

                var c = obj.Intersection(intersectRay);
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
                foreach (TransformationMatrix transform in hit.HitObject.Transformations)
                {
                    var normalMatrix = new Vector3DMatrix();
                    var hitpointMatrix = new Vector3DMatrix();

                    normalMatrix.FromVector(hit.Normal);
                    hitpointMatrix.FromPoint(hit.HitPoint);

                    var invNormal = normalMatrix.TransformNormal(transform);
                    var invHitpoint = hitpointMatrix.Transform(transform);

                    hit.Normal = invNormal.ToVector3D();
                    hit.HitPoint = invHitpoint.ToVector3D();
                }
                return hit;
            }

        }



    }
}

