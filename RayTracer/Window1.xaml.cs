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
using RayTracer.RayTracer.Primitives;
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
        View pic;
        bool color = true;
        RayTracer.Scene scene = null;

        public Window1()
        {
            InitializeComponent();

            label1.Content = "";
            var t = DateTime.Now;

            Pic2();

            string error = "";
            try
            {
                pic = scene.Render();
                OutPutPic();
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


            label1.Content = (DateTime.Now - t).Milliseconds.ToString();
        }

        private void OutPutPic()
        {
            Bitmap picture = null;

            if (color)
                picture = pic.ExportImage();
            else
                picture = pic.ExportDepthImage();

            MemoryStream ms = new MemoryStream();
            picture.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = ms;
            bi.EndInit();

            image1.Source = bi;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            color = !color;
            OutPutPic();
        }

        private void Pic0()
        {
            var camera = new SimplePerspectiveCamera(new Vector3D(0, 0, -5), new Vector3D(0, 0, 1).Normalize(), new Vector3D(0, 1, 0).Normalize(), 5);

            var plane = new Plane(4.4, new Vector3D(0, 1, 0));
            plane.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, 0);
            plane.Material.DiffuseColor = new RayTracer.Color(.4, .3, .3);

            var sphere1 = new Sphere(new Vector3D(1, -0.8, 3), 2.5);
            sphere1.Material = new RayTracer.Materials.CustomMaterial(1, .6, .2, .8);
            sphere1.Material.DiffuseColor = new RayTracer.Color(.7, .7, .7);


            var sphere2 = new Sphere(new Vector3D(-5.5f, -0.5, 7), 2);
            sphere2.Material = new RayTracer.Materials.CustomMaterial(1, 1, .1, .8);
            sphere2.Material.DiffuseColor = new RayTracer.Color(.7, .7, 1);


            var listObj = new List<Primitive>();
            listObj.Add(sphere1);
            listObj.Add(sphere2);
            listObj.Add(plane);


            var light1 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light1.Location = new Vector3D(0, 5, 5);
            light1.Color = new RayTracer.Color(.4, .4, .4);


            var light2 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light2.Location = new Vector3D(2, 5, 1);
            light2.Color = new RayTracer.Color(.6, .6, .8);


            var listLight = new List<Light>();
            //listLight.Add(light1);
            //listLight.Add(light2);

            var light3 = new RayTracer.Lights.DirectionalLight(new Vector3D(0, -1, 1), new Vector3D(.5, .5, .5));
            light3.Location = new Vector3D(0, 0, 2);
            light3.Color = new RayTracer.Color(1f, 0.6f, 0.8f);
            listLight.Add(light3);

            var ambientLight = new RayTracer.Color(0, 0, 0);
            scene = new RayTracer.Scene(800, 600, 8.0/800.0 , camera, ambientLight, listObj, listLight, new RayTracer.Shaders.PhongIllumination(), new RayTracer.Color(0, 0, 0));
        }

        private void Pic1()
        {
            //var camera = new OrthographicCamera(new Vector3D(0, 0, -5), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0));
            //var camera = new SimplePerspectiveCamera(new Vector3D(0, 0, -5), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 10);
            var camera = new PerspectiveCamera(new Vector3D(0, 0, -15), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), .6);

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
                                                                    0);

            var sphere2 = new Sphere(new Vector3D(-5.5, -5, 7), 6);

            sphere2.Material = new RayTracer.Materials.PhongMaterial(new RayTracer.Color(.7, .7, 1),
                                                                    new RayTracer.Color(1, 1, 1),
                                                                    20,
                                                                    new RayTracer.Color(.1, .1, 0),
                                                                    new RayTracer.Color(.1, .1, 0),
                                                                    .1);

            var triangle = new Triangle(new Vector3D(-11, 12, 18),
                                        new Vector3D(3, 12, 22),
                                        new Vector3D(-4, -2, 18));
            triangle.Material = new RayTracer.Materials.CustomMaterial(1, 0, .8, .2);
            triangle.Material.DiffuseColor = new RayTracer.Color(.8, 0, 0);
            triangle.Material.Exponent = 20;
            triangle.Material.SpecularColor = new RayTracer.Color(.8, 0, 0);


            var listObj = new List<Primitive>();
            listObj.Add(sphere1);
            listObj.Add(sphere2);
            listObj.Add(plane);
            listObj.Add(triangle);

            var light1 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light1.Location = new Vector3D(0, 5, 5);
            light1.Color = new RayTracer.Color(.6, .6, .6);


            var light2 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light2.Location = new Vector3D(2, 5, 1);
            light2.Color = new RayTracer.Color(.7, .7, .9);


            var listLight = new List<Light>();
            listLight.Add(light1);
            listLight.Add(light2);

            var ambientLight = new RayTracer.Color(.16, .16, .16);
            scene = new RayTracer.Scene(800, 600, .05, camera, ambientLight, listObj, listLight, new RayTracer.Shaders.PhongIllumination(), new RayTracer.Color(0, 0, 0));
        }

        private void Pic2()
        {
            var objects = new List<Primitive>();

            // ground plane
            var groundPlane = new Plane(4.4, new Vector3D(0, 1, 0));
            groundPlane.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, .8);
            groundPlane.Material.DiffuseColor = new RayTracer.Color(.4, .3, .3);
            groundPlane.Material.RefractionCoeff = 0;
            groundPlane.Material.RefractionIndex = 0;
            objects.Add(groundPlane);

            // big sphere
            var bigSphere = new Sphere(new Vector3D(2, 0.8f, 3), 2.5);
            bigSphere.Material = new RayTracer.Materials.CustomMaterial(1, .2, .2, .8);
            bigSphere.Material.RefractionCoeff = 0.8;
            bigSphere.Material.RefractionIndex = 1.3;
            bigSphere.Material.DiffuseColor = new RayTracer.Color(0.7f, 0.7f, 1.0f);
            objects.Add(bigSphere);

            // small sphere
            var smallSphere = new Sphere(new Vector3D(-5.5f, -0.5, 7), 2);
            smallSphere.Material = new RayTracer.Materials.CustomMaterial(1, .5, .1, .8);
            smallSphere.Material.RefractionCoeff = 0;
            smallSphere.Material.RefractionIndex = 1.3;
            smallSphere.Material.DiffuseColor = new RayTracer.Color(.7, .7, 1);
            //objects.Add(smallSphere);

            // extra sphere
            var extraSphere = new Sphere(new Vector3D(-1.5f, -3.8f, 1), 1.5f);
            extraSphere.Material = new RayTracer.Materials.CustomMaterial(1, 0, .2, .8);
            extraSphere.Material.RefractionCoeff = 0.8;
            extraSphere.Material.DiffuseColor = new RayTracer.Color(1.0f, 0.4f, 0.4f);
            //objects.Add(extraSphere);

            // back plane
            var backPlane = new Plane(12, new Vector3D(0.4f, 0, -1));
            backPlane.Material = new RayTracer.Materials.CustomMaterial(1, 0, .6, 0);
            backPlane.Material.RefractionCoeff = 0;
            backPlane.Material.RefractionIndex = 0;
            backPlane.Material.DiffuseColor = new RayTracer.Color(0.5f, 0.3f, 0.5f);
            objects.Add(backPlane);

            // ceiling plane
            var cPlane = new Plane(7.4f, new Vector3D(0, -1, 0));
            cPlane.Material = new RayTracer.Materials.CustomMaterial(1, 0, .5, 0);
            cPlane.Material.RefractionCoeff = 0;
            cPlane.Material.DiffuseColor = new RayTracer.Color(0.4f, 0.7f, 0.7f);
            //objects.Add(cPlane);

            // grid            
            for (int x = 0; x < 8; x++) for (int y = 0; y < 7; y++)
                {
                    var s = new Sphere(new Vector3D(-4.5f + x * 1.5f, -4.3f + y * 1.5f, 10), 0.3f);
                    s.Material = new RayTracer.Materials.CustomMaterial(1, 0, .6, .6);
                    s.Material.RefractionCoeff = 0;
                    s.Material.DiffuseColor = new RayTracer.Color(0.3f, 1.0f, 0.4f);
                    objects.Add(s);
                }

            var lights = new List<Light>();

            // light source 1
            var light1 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light1.Location = new Vector3D(0, 5, 5);
            light1.Color = new RayTracer.Color(0.4f, 0.4f, 0.4f);
            //lights.Add(light1);

            // light source 2
            var light2 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light2.Location = new Vector3D(-3, 5, 1);
            light2.Color = new RayTracer.Color(0.6f, 0.6f, 0.8f);
            //lights.Add(light2);

            // light source 3
            var light3 = new RayTracer.Lights.DirectionalLight(new Vector3D(.2, -.1, .5), new Vector3D(.5, .5, .5));
            light3.Location = new Vector3D(0, 0, 2);
            light3.Color = new RayTracer.Color(1f, 0.6f, 0.8f);
            lights.Add(light3);

            var ambientLight = new RayTracer.Color(.0, .0, .0);
            var camera = new SimplePerspectiveCamera(new Vector3D(0, 0, -5), new Vector3D(.2, 0, 1).Normalize(), new Vector3D(0, 1, 0).Normalize(), 5);

            var f = new RayTracer.Primitives.Mesh.FaceList();

            f.Vertices.Add(new Vector3D(1.0, 1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, 1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, 1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, 1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, -1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, -1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, -1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, -1.0, 1.0));

            for (int i = 0; i < f.Vertices.Count; i++)
            {
                f.Vertices[i] += new Vector3D(-1.5, 0, 0);
            }

            f.Faces.Add(new int[] { 3, 2, 1 });
            f.Faces.Add(new int[] { 1, 2, 5 });
            f.Faces.Add(new int[] { 2, 6, 5 });
            f.Faces.Add(new int[] { 4, 5, 6 });
            f.Faces.Add(new int[] { 7, 4, 6 });
            f.Faces.Add(new int[] { 2, 3, 7 });
            f.Faces.Add(new int[] { 7, 6, 2 });
            f.Faces.Add(new int[] { 1, 5, 4 });
            f.Faces.Add(new int[] { 4, 7, 3 });
            f.Faces.Add(new int[] { 4, 3, 0 });
            f.Faces.Add(new int[] { 0, 1, 4 });
            f.Faces.Add(new int[] { 3, 1, 0 });

            var mesh = new TriangleMesh(f);
            mesh.Material = new RayTracer.Materials.CustomMaterial(1, 1, .1, .8);
            mesh.Material.DiffuseColor = new RayTracer.Color(.5, .3, 0);
            mesh.Material.RefractionCoeff = 0;
            mesh.Material.RefractionIndex = 0;

            //objects.Add(mesh);

            scene = new RayTracer.Scene(800, 600, .01, camera, ambientLight, objects, lights, new RayTracer.Shaders.PhongIllumination(), new RayTracer.Color(0, 0, 0));
        }

        private void Pic3()
        {

            //var camera = new SimplePerspectiveCamera(new Vector3D(3, 0, 5), new Vector3D(0, 0, -1).Normalize(), new Vector3D(0, 1, 0).Normalize(), 5);
            var camera = new SimplePerspectiveCamera(new Vector3D(3, 0, -5), new Vector3D(0, 0, 1).Normalize(), new Vector3D(0, 1, 0).Normalize(), 5);
            //var camera = new PerspectiveCamera(new Vector3D(0, 0, -5), new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 2);
            //var camera = new OrthographicCamera(new Vector3D(0, 0, 0), new Vector3D(-1, 0, 1), new Vector3D(0, 1, .2));

            var plane = new Plane(4.4, new Vector3D(0, 1, 0));
            plane.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, 0);
            plane.Material.DiffuseColor = new RayTracer.Color(.4, .3, .3);

            var sphere1 = new Sphere(new Vector3D(0, 0, 0), 1.5);
            sphere1.Material = new RayTracer.Materials.CustomMaterial(1, .6, .2, .8);
            sphere1.Material.DiffuseColor = new RayTracer.Color(.7, .7, .7);

            var sphere2 = new Sphere(new Vector3D(-5.5f, -0.5, 7), 2);
            sphere2.Material = new RayTracer.Materials.CustomMaterial(1, 1, .1, .8);
            sphere2.Material.DiffuseColor = new RayTracer.Color(.2, .7, 1);

            var listObj = new List<Primitive>();
            //listObj.Add(sphere1);
            //listObj.Add(sphere2);
            listObj.Add(plane);


            var light1 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light1.Location = new Vector3D(0, 5, 5);
            light1.Color = new RayTracer.Color(.4, .4, .4);


            var light2 = new RayTracer.Lights.PointLight(new Vector3D(.5, .5, .5));
            light2.Location = new Vector3D(2, 5, 1);
            light2.Color = new RayTracer.Color(.6, .6, .8);


            var light3 = new RayTracer.Lights.DirectionalLight(new Vector3D(0, 0, 1), new Vector3D(.5, .5, .5));
            light3.Location = new Vector3D(0, 0, 2);
            light3.Color = new RayTracer.Color(1f, 0.6f, 0.8f);

            var listLight = new List<Light>();
            listLight.Add(light1);
            //listLight.Add(light2);
            listLight.Add(light3);

            var f = new RayTracer.Primitives.Mesh.FaceList();

            f.Vertices.Add(new Vector3D(1.0, 1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, 1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, 1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, 1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, -1.0, 1.0));
            f.Vertices.Add(new Vector3D(1.0, -1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, -1.0, -1.0));
            f.Vertices.Add(new Vector3D(-1.0, -1.0, 1.0));

            f.Faces.Add(new int[] { 3, 2, 1 });
            f.Faces.Add(new int[] { 1, 2, 5 });
            f.Faces.Add(new int[] { 2, 6, 5 });
            f.Faces.Add(new int[] { 4, 5, 6 });
            f.Faces.Add(new int[] { 7, 4, 6 });
            f.Faces.Add(new int[] { 2, 3, 7 });
            f.Faces.Add(new int[] { 7, 6, 2 });
            f.Faces.Add(new int[] { 1, 5, 4 });
            f.Faces.Add(new int[] { 4, 7, 3 });
            f.Faces.Add(new int[] { 4, 3, 0 });
            f.Faces.Add(new int[] { 0, 1, 4 });
            f.Faces.Add(new int[] { 3, 1, 0 });

            var mesh = new TriangleMesh(f);
            mesh.Material = new RayTracer.Materials.CustomMaterial(1, 1, .1, .8);
            mesh.Material.DiffuseColor = new RayTracer.Color(.5, .3, 0);
            mesh.Material.RefractionCoeff = 0;
            mesh.Material.RefractionIndex = 0;

            //listObj.Add(mesh);
            var ambientLight = new RayTracer.Color(.2, .2, .2);
            scene = new RayTracer.Scene(800, 600, .05, camera, ambientLight, listObj, listLight, new RayTracer.Shaders.PhongIllumination(), new RayTracer.Color(0, 0, 0));
        }

        private void Pic4()
        {
            var camera = new SimplePerspectiveCamera(new Vector3D(0, 0, 8), new Vector3D(0, 0, -1).Normalize(), new Vector3D(0, 1, 0).Normalize(), 5);

            var plane = new Plane(-2, new Vector3D(0, 1, 0));
            plane.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, .8);
            plane.Material.DiffuseColor = new RayTracer.Color(.10, .35, .1);
            plane.Material.SpecularColor = new RayTracer.Color(.45, .55, .45);
            plane.Material.Exponent = 1;
            plane.Material.RefractionIndex = 0;
            
            var sphere1 = new Sphere(new Vector3D(0, 0, 0), 1.0);
            sphere1.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, .8);
            sphere1.Material.DiffuseColor = new RayTracer.Color(0, 0, .2);
            sphere1.Material.SpecularColor = new RayTracer.Color(.7, .7, .7);
            sphere1.Material.Exponent = 64;
            sphere1.Material.RefractionIndex = 1.5;
            sphere1.Material.TransparentColor = new RayTracer.Color(1, 1, 1);

            var sphere2 = new Sphere(new Vector3D(1, 0, -2), 1);
            sphere2.Material = new RayTracer.Materials.CustomMaterial(1, 0, 1, .8);
            sphere2.Material.DiffuseColor = new RayTracer.Color(0.2, 0.2, .2);
            sphere2.Material.SpecularColor = new RayTracer.Color(.5, .5, .5);
            sphere2.Material.Exponent = 64;
            sphere2.Material.ReflectiveColor = new RayTracer.Color(.5, .5, .5);
            
            
            var listObj = new List<Primitive>();
            listObj.Add(sphere1);
            listObj.Add(sphere2);
            listObj.Add(plane);


            var light1 = new RayTracer.Lights.PointLight(new Vector3D(1, .5, .5));
            light1.Location = new Vector3D(0, 10, 2);
            light1.Color = new RayTracer.Color(1, 1, 1);

                        
            var listLight = new List<Light>();
            listLight.Add(light1);
            
            var ambientLight = new RayTracer.Color(0, 0, 0);
            scene = new RayTracer.Scene(1000, 1000, 0.0040, camera, ambientLight, listObj, listLight, new RayTracer.Shaders.PhongShader(), new RayTracer.Color(0.1, 0.1, 0.1));
        }
    }
}
