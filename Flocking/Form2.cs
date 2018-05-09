using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Nuevo
{
    public partial class Form2 : Form
    {
   

        public Form2()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            ogreSceneWrap.initOgre();

        }

        int index = 1;

        private void button2_Click(object sender, EventArgs e)
        {

        }
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
            //ogreSceneWrap.setCamera(trackBar1.Value - trackBar1.Maximum / 2, 100, 0, 0, 0.0f, 0.0f);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //foreach (Obstaculo r in Logica.rectangulos)
            //{
            //    System.Console.WriteLine("Rectangulo 1");
            //    IntPtr boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgre((float)(Logica.mapeo(r.rectangle.Y)), Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(r.rectangle.X, Constantes.limites.Width)), 0.5f, 1.0f, 0.5f, 0);
            //    r.ogreRef = boidRef;
            //    index++;
            //}

            foreach (Obstaculo r in Logica.rectangulos)
            {
                float y = (float)((r.rectangle.Y) + (r.rectangle.Height / 2));
                float x = (float)((r.rectangle.X) + (r.rectangle.Width / 2));

                IntPtr boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgreObs(y, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(r.rectangle.X, Constantes.limites.Width)), (float)r.rectangle.Height / 100, 1.0f, 0.1f, 0);
                r.ogreRef = boidRef;
                index++;

                boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgreObs(r.rectangle.Y, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(x, Constantes.limites.Width)), 0.1f, 1.0f, (float)r.rectangle.Width / 100, 0);
                r.ogreRef = boidRef;
                index++;

                float yh = (float)((r.rectangle.Y) + (r.rectangle.Height));
                float xw = (float)((r.rectangle.X) + (r.rectangle.Width));

                boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgreObs(yh, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(x, Constantes.limites.Width)), 0.1f, 1.0f, (float)r.rectangle.Width / 100, 0);
                r.ogreRef = boidRef;
                index++;

                boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgreObs(y, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(xw, Constantes.limites.Width)), (float)r.rectangle.Height / 100, 1.0f, 0.1f, 0);
                r.ogreRef = boidRef;
                index++;
            }

            foreach (Obstaculo c in Logica.circulos)
            {
                IntPtr boidRef = ogreSceneWrap.addObstacle("obs" + Convert.ToString(index), 1, (float)(Constantes.mapearOgreObs(c.rectangle.Y, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(c.rectangle.X, Constantes.limites.Width)), (float)c.rectangle.Width / 30, 0.3f, (float)c.rectangle.Width / 30, 0);
                c.ogreRef = boidRef;
                index++;
            }

            foreach (Obstaculo l in Logica.lineas)
            {
                float line_size = (float)Constantes.distancia_dos_puntos((int)l.rectangle.X, (int)l.rectangle.Y, (int)l.rectangle.Width, (int)l.rectangle.Height);
                float pendiente = (float)((l.rectangle.Height - l.rectangle.Y) / (l.rectangle.Width - l.rectangle.X));
                
                float width = (float)Math.Abs(l.rectangle.Width - l.rectangle.X);
                float height = (float)Math.Abs(l.rectangle.Height - l.rectangle.Y);

                float x;
                float y;

                if (l.rectangle.X <= l.rectangle.Width)
                {
                    x = (float)((l.rectangle.X) + (width / 2));
                    y = (float)((l.rectangle.Y) + (height / 2));
                    if (pendiente < 0)
                        y = (float)((l.rectangle.Y) - (height / 2));
                }
                else
                {
                    x = (float)((l.rectangle.Width) + (width / 2));
                    y = (float)((l.rectangle.Height) + (height / 2));
                    if (pendiente < 0)
                        y = (float)((l.rectangle.Height) - (height / 2));
                }
                

                Vector v = new Vector(l.rectangle.Width-l.rectangle.X,l.rectangle.Height-l.rectangle.Y);
                v.normalize();
                
                IntPtr boidRef = ogreSceneWrap.addWall("wall" + Convert.ToString(index), (float)(Constantes.mapearOgreObs(y, Constantes.limites.Height)), 0, (float)(Constantes.mapearOgre(x, Constantes.limites.Width)), 0.1f, 1.0f, line_size/100, (float)Constantes.calculateRotationOgre(v));
                l.ogreRef = boidRef;
                index++;
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int n = 0;
            foreach (Boid b in Logica.objects)
            {
                if (n != 0)
                {
                    IntPtr boidRef = ogreSceneWrap.addBoid("boid" + Convert.ToString(index), (float)(Constantes.mapearOgre(b.Location.Y, Constantes.limites.Height)), 50, (float)(Constantes.mapearOgre(b.Location.X, Constantes.limites.Width)), 50.0f, 50.0f, 50.0f, 0);
                    b.ogreRef = boidRef;
                    System.Console.WriteLine("OgreRef " + n + ": " + boidRef);
                    index++;
                }
                n++;
            }
            Constantes.ogre3d = true;
            
            

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //ogreSceneWrap.removeEntities();
            //Logica.objects.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ogreSceneWrap.setCamera(1000, 500, 0, 0, 0.0f, 0.0f);
            ogreSceneWrap.createGround();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (Constantes.ogre3d && !Constantes.sincronizar)
            {
                int n = 0;
                foreach (Boid b1 in Logica.objects)
                {

                    if (n != 0 && b1.ogreRef!= IntPtr.Zero)
                    {
                        float g = (float)(b1.calculateRotationOgre());
   
                        ogreSceneWrap.rotateNode(b1.ogreRef, (float)(-b1.rotacion));

                        ogreSceneWrap.rotateNode(b1.ogreRef, g);

                        ogreSceneWrap.moveNode(b1.ogreRef, (float)(Constantes.mapearOgre(b1.Location.Y, Constantes.limites.Height)), 50, (float)(Constantes.mapearOgre(b1.Location.X, Constantes.limites.Width)));

                        b1.rotacion = g;


                    }
                    n++;
                }
            }

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.ogre3d = false;
            ogreSceneWrap.closeAPP();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Constantes.ogre3d = false;
            ogreSceneWrap.closeAPP();        
        }

    }
}
