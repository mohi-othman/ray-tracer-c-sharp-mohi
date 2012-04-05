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
            var camera = new ConicalCamera(new Vector3D(0, 0, -5));

            var plane = new Plane(4.4, new Vector3D(0, 1, 0));
            plane.Material = new RayTracer.Materials.PhongMaterial(new RayTracer.Color(.4, .3, .3),
                                                                    new RayTracer.Color(1, 1, 1),
                                                                    20,
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    0);


            var sphere1 = new Sphere(new Vector3D(1, -0.8, 3), 2.5);
            sphere1.Material = new RayTracer.Materials.PhongMaterial(new RayTracer.Color(.7, .7, .7),
                                                                    new RayTracer.Color(1, 1, 1),
                                                                    20,
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    .5);

            var sphere2 = new Sphere(new Vector3D(-5.5, -0.5, 7), 2);
            sphere2.Material = new RayTracer.Materials.PhongMaterial(new RayTracer.Color(.7, .7, 1),
                                                                    new RayTracer.Color(1, 1, 1),
                                                                    20,
                                                                    new RayTracer.Color(.1, .1, 0),
                                                                    new RayTracer.Color(.1, .1, 0),
                                                                    0);

            var triangle = new Triangle(new Vector3D(-11, 12, 18),
                                        new Vector3D(3, 12, 22),                                        
                                        new Vector3D(-4, -2, 18));
            triangle.Material = new RayTracer.Materials.PhongMaterial(new RayTracer.Color(.7, .3, .3),
                                                                    new RayTracer.Color(1, 1, 1),
                                                                    20,
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    new RayTracer.Color(.1, 0, 0),
                                                                    0);

            var listObj = new List<Primitive>();
            listObj.Add(sphere1);
            listObj.Add(sphere2);
            listObj.Add(plane);
            listObj.Add(triangle);

            var light1 = new RayTracer.Lights.PointLight();
            light1.Location = new Vector3D(0, 5, 5);
            light1.Color = new RayTracer.Color(.6, .6, .6);
            

            var light2 = new RayTracer.Lights.PointLight();
            light2.Location = new Vector3D(2, 5, 1);
            light2.Color = new RayTracer.Color(.7, .7, .9);
            

            var listLight = new List<LightSource>();
            listLight.Add(light1);
            listLight.Add(light2);

            var ambientLight = new RayTracer.Color(.16, .16, .16);
            var scene = new RayTracer.Scene(800, 600, camera, ambientLight, listObj, listLight, new RayTracer.Shaders.PhongShader(), new RayTracer.Color(0, 0, 0));
            View pic;
            Bitmap picture = null;
            string error = "";
            try
            {
                pic = scene.Render();
                picture = pic.ExportImage();
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            if (error != "")
            {
                MessageBox.Show(error);
                return;
            }

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
