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
            
            var sphere = new Sphere(new Vector3D(233, 290, 0), 100);
            sphere.Material = new RayTracer.Materials.LambertianMaterial();
            sphere.Color = new RayTracer.Color(200, 50, 0);

            var sphere2 = new Sphere(new Vector3D(353, 350, 200), 90);
            sphere2.Material = new RayTracer.Materials.LambertianMaterial();
            sphere2.Color = new RayTracer.Color(0, 250, 75);

            var listObj = new List<SolidObject>();
            listObj.Add(sphere);
            listObj.Add(sphere2);

            var light = new RayTracer.Lights.PointLight();
            light.Location = new Vector3D(320, 240, -700);
            light.Color = new RayTracer.Color(255, 255, 255);

            var listLight = new List<LightSource>();
            listLight.Add(light);

            var scene = new RayTracer.Scene(640, 480, camera, listObj, listLight);
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
