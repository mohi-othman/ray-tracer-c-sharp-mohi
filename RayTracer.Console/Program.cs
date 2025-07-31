using RayTracer.Console;
using SixLabors.ImageSharp;
using System.Diagnostics;

var sw = new Stopwatch();

var scenesGen = new ScenesGenerator();

sw.Start();

var image = scenesGen
                .GetScene2()
                .ParallelRender()
                .ExportImage();

var imageName = $"raytrace{DateTime.Now.ToString("yyyyMMddhhmmss")}.jpg";

image.SaveAsJpeg($"C:\\Projects\\{imageName}");

sw.Stop();

Console.WriteLine($"Successfully rendered image {imageName}. Time elapsed: {sw.Elapsed.ToString()}");