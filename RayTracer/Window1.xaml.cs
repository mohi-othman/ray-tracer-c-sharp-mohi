using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RayTracer.RayTracer;
using RayTracer.RayTracer.Objects;
using RayTracer.RayTracer.Cameras;
using System.Drawing;
using System.IO;

namespace RayTracer
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //var camera = new OrthographicCamera(1000);
            var camera = new ConicalCamera(new Vector3D(320, 240, -700));

            var sphere = new Sphere(new Vector3D(150, 290, 0), 50);
            sphere.Material = new RayTracer.Materials.LambertianMaterial(new RayTracer.Color(.9, .4, 0));
            //sphere.Material = new RayTracer.Materials.CustomMaterial(new RayTracer.Color(.9, .4, 0), 1, 0, 1);


            var sphere2 = new Sphere(new Vector3D(353, 290, 700), 200);
            //sphere2.Material = new RayTracer.Materials.LambertianMaterial(new RayTracer.Color(0, .95, .4));
            sphere2.Material = new RayTracer.Materials.CustomMaterial(new RayTracer.Color(0, .95, .4), .5, 0, 1);

            var plane = new Plane(200, new Vector3D(0, 0, -1));
            plane.Material = new RayTracer.Materials.CustomMaterial(new RayTracer.Color(0, .8, .2), 0, 0, 1);

            var listObj = new List<SolidObject>();
            listObj.Add(sphere);
            listObj.Add(sphere2);
            listObj.Add(plane);

            var light1 = new RayTracer.Lights.PointLight();
            light1.Location = new Vector3D(10, 240, -700);
            light1.Color = new RayTracer.Color(1, 1, 1);

            var light2 = new RayTracer.Lights.PointLight();
            light2.Location = new Vector3D(220, 100, 500);
            light2.Color = new RayTracer.Color(.2, .2, .2);

            var listLight = new List<LightSource>();
            listLight.Add(light1);
            //listLight.Add(light2);

            var ambientLight = new RayTracer.Color(.1, .1, .1);
            var scene = new RayTracer.Scene(640, 480, camera, ambientLight, listObj, listLight, new RayTracer.Shaders.DiffuseShader());
            var picArray = scene.Render();
            //var picture = picArray.ExportDepthImage();
            var picture = picArray.ExportImage();

            MemoryStream ms = new MemoryStream();
            picture.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            image1.Source = bi;
        }
    }
}
