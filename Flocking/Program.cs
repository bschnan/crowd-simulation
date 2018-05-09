using Gtk;
using GLib;
using Cairo;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Nuevo {

	class SharpApp : Window {

		private bool timer = true;
		private DrawingArea darea;

		private Window ventana;

		int xo, yo, xt, yt;
		bool pressed = false;

		BoidCollection coleccion = new BoidCollection ();
		List<Obstaculo> rectangulos = new List<Obstaculo>();
		List<Obstaculo> lineas = new List<Obstaculo>();
		List<Obstaculo> circulos = new List<Obstaculo>();
		System.Drawing.Rectangle u= new System.Drawing.Rectangle();

		Matrix r = new Matrix ();
		Matrix t = new Matrix ();

		double Ax = 0;
		double Ay = - Logica.tamanio_boid * 2;
		double Bx = - Logica.tamanio_boid;
		double By = Logica.tamanio_boid * 2;
		double Cx = Logica.tamanio_boid;
		double Cy = Logica.tamanio_boid * 2;

		double rot;

		Logica logica=new Logica();

		DateTime r1;
		DateTime r2;

		DateTime a1;
		DateTime a2;


		public SharpApp() : base()
		{
			Console.WriteLine ("ROJO: cercano");
			Console.WriteLine ("VERDE: menos concurrido");
			Console.WriteLine ("AZUL: al azar");
			ventana = this;
			ventana.Maximize ();
			SetPosition (WindowPosition.Center);
			DeleteEvent += delegate {
				Application.Quit ();
			};
			GLib.Timeout.Add (Constantes.milisegundos_iteracion, new GLib.TimeoutHandler (OnTimer));
			darea = new DrawingArea ();
			darea.ExposeEvent += OnExpose;
			int displayX, displayY;
			ventana.Title = "Flocking 3.0";

			ventana.menub.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (240, 240, 240));
			ventana.tb1.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (240, 240, 240));
			ventana.tb2.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (240, 240, 240));
			//ventana.tb3.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (240, 240, 240));
			darea.ModifyBg (Gtk.StateType.Normal, new Gdk.Color (255, 255, 255));

            //ventana.GetSize(out displayX, out displayY);
            //Constantes.limites = new System.Drawing.Rectangle(0, 0, displayX, displayY);

            Constantes.limites = new System.Drawing.Rectangle(0, 0, 1500, 1500);

			Logica.grilla = new Grilla ();

            //Constantes.viewSurface = new ImageSurface (Format.ARGB32, displayX, displayY);

            Constantes.viewSurface = new ImageSurface(Format.ARGB32, 1500, 1500);

//			Constantes.escenarioSurface = new ImageSurface (Format.ARGB32, displayX, displayY);



			ventana.scrollw.AddWithViewport (darea);
			ventana.ShowAll();

		}

		private void reset(Cairo.Context cr){
			Ax = 0;
			Ay = - Logica.tamanio_boid * 2;
			Bx = - Logica.tamanio_boid;
			By = Logica.tamanio_boid * 2;
			Cx = Logica.tamanio_boid;
			Cy = Logica.tamanio_boid * 2;
			r.InitIdentity ();
			t.InitIdentity ();
		}


		private void triangulo_contorno(Cairo.Context cr,Vector location, double rot){

			reset (cr);

			cr.Save();

			r.Rotate (rot);
			t.Translate (location.X,Logica.mapeo(location.Y));

			r.TransformPoint (ref Ax, ref Ay);
			t.TransformPoint (ref Ax, ref Ay);

			r.TransformPoint (ref Bx, ref By);
			t.TransformPoint (ref Bx, ref By);

			r.TransformPoint (ref Cx, ref Cy);
			t.TransformPoint (ref Cx, ref Cy);

//			System.Diagnostics.Debug.WriteLine (Ax+", "+Ay);

			cr.MoveTo (Ax, Ay);
			cr.LineTo (Bx, By);
			cr.LineTo (Cx, Cy);

			cr.LineWidth = 2;
			cr.SetSourceRGBA (0, 0, 0, 0.5);

			cr.ClosePath ();
			cr.FillPreserve ();
			cr.Stroke ();

			cr.Restore ();

			cr.Save ();
			cr.Arc(Ax, Ay, Logica.radioSep, 0, 2*Math.PI);
			cr.SetSourceRGBA (1, 0, 0, 0.2);
			cr.Stroke ();
			cr.Restore ();

		}

		private void triangulo(Cairo.Context cr,Boid b, double rot){

			reset (cr);

			cr.Save();

			r.Rotate (rot);
			t.Translate (b.Location.X,Logica.mapeo(b.Location.Y));

			r.TransformPoint (ref Ax, ref Ay);
			t.TransformPoint (ref Ax, ref Ay);

			r.TransformPoint (ref Bx, ref By);
			t.TransformPoint (ref Bx, ref By);

			r.TransformPoint (ref Cx, ref Cy);
			t.TransformPoint (ref Cx, ref Cy);

			//			System.Diagnostics.Debug.WriteLine (Ax+", "+Ay);

			cr.MoveTo (Ax, Ay);
			cr.LineTo (Bx, By);
			cr.LineTo (Cx, Cy);

			cr.LineWidth = 2;

			if(b.criterio == 0)
				cr.SetSourceRGBA (0.5, 0, 0, 0.5); //cercano
			else if(b.criterio == 1)
				cr.SetSourceRGBA (0, 0.5, 0, 0.5); //menos concurrido
			else
				cr.SetSourceRGBA (0, 0, 0.5, 0.5); //random

			cr.ClosePath ();
			cr.FillPreserve ();
			cr.Stroke ();

			cr.Restore ();


		}

		void OnExpose(object sender, ExposeEventArgs args)
		{

			DrawingArea area = (DrawingArea)sender;
//			area.AddEvents ((int) Gdk.EventMask.ButtonPressMask);
//			area.ButtonPressEvent += delegate(object o, ButtonPressEventArgs arg) {
//				int i,j;
//				area.GetPointer(out i,out j);
//				System.Console.WriteLine(i+" , "+j);
//	
//			};
			if (Constantes.simulacion || Constantes.escenario) {
			

				area.SetSizeRequest ((int)(Constantes.limites.Width * Constantes.sx), (int)(Constantes.limites.Height * Constantes.sy));
					
				Cairo.Context cr = Gdk.CairoHelper.Create (area.GdkWindow);
				cr.Scale (Constantes.sx,Constantes.sy);

				if (ventana.calor.Active && ! ventana.trayectoria.Active)
					dibujar_grilla_estadistica (area, cr);

				if (ventana.trayectoria.Active && ! ventana.calor.Active)
					dibujar_trayectorias (area, cr);

				if (ventana.trayectoria.Active && ventana.calor.Active)
					dibujar_trayectorias (area, cr);

				if (ventana.grilla.Active && !ventana.calor.Active && !ventana.trayectoria.Active)
					dibujar_grilla (area, cr);

				if(Constantes.escenarioCargado)
					escenarioCargado (cr);

				if (Constantes.simulacion && ! ventana.calor.Active && !ventana.trayectoria.Active) {
                    Constantes.sincronizar = true;
					if (Constantes.simulacionContinua)
						play_simulacion_continua2 (area, cr); //para simulacion continua poner play_simulacion_grilla (area, cr); y cambiar en BoidAutonomo el metodo update_obejtivo()
					else
						play_simulacion_finita2 (area, cr);
                    Constantes.sincronizar = false;
				}
				if (Constantes.escenario)
					scene_design (area, cr);


				cr.GetTarget().Dispose ();
				((IDisposable)cr).Dispose ();

			}
		}


		private void dibujar_grilla_estadistica(DrawingArea area, Cairo.Context cr){
			double[] color;
			double min = Logica.grilla.min_visitas ();
			double max = Logica.grilla.max_visitas ();
			for (int x = 0; x < Constantes.nro_casillas; x++)
				for (int y = 0; y < Constantes.nro_casillas; y++) {
					int x0 = Logica.grilla.get_casillero (x, y).inicioX;
					int y0 = Logica.grilla.get_casillero (x, y).inicioY;
					int xf = Logica.grilla.get_casillero (x, y).finX - Logica.grilla.get_casillero (x, y).inicioX;
					int yf = Logica.grilla.get_casillero (x, y).finY - Logica.grilla.get_casillero (x, y).inicioY;
					cr.Save ();

//					referencia_mapa (cr);

					cr.Save ();

					color = calcular_mapa_calor (Logica.grilla.get_casillero (x, y).visitas,min,max);
					cr.SetSourceRGBA (color[0], color[1], color[2],0.5);
					cr.Rectangle (x0, Logica.mapeo(y0)-yf, xf, yf);
					cr.Fill ();


//					cr.SetSourceRGB (0,0,0);
//					cr.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
//					cr.SetFontSize (12);
//					TextExtents t = cr.TextExtents (Logica.grilla.get_casillero (x, y).visitas.ToString ());
//					cr.MoveTo (x0+10, Logica.mapeo(y0)-10);
//					cr.ShowText (Logica.grilla.get_casillero (x, y).visitas.ToString ());

					cr.Restore();

				}
		}

		private void referencia_mapa(Cairo.Context cr){

			cr.Save ();
			cr.SetSourceRGB(0, 0, 0);
			cr.LineWidth = 1;
			cr.Rectangle(2, 2, 250, 100);
			cr.StrokePreserve();
			cr.SetSourceRGB(1, 1, 1);
			cr.Fill();
			cr.Restore ();

			double[] color;
			for (int i = 27; i < 227; i++) {
				color = calcular_mapa_calor (i, 27, 227);
				cr.Save ();
				cr.SetSourceRGBA (color[0], color[1], color[2], 1);
				cr.Rectangle (i, 27, 1, 50);
				cr.Fill ();
				cr.Restore ();
			}

			cr.Save ();
			cr.SetSourceRGB (0,0,0);
			cr.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
			cr.SetFontSize (10);
			cr.MoveTo (7, 92);
			cr.ShowText ("Menos visitado");
			cr.MoveTo (192, 92);
			cr.ShowText ("Mas visitado");
			cr.MoveTo (12, 12);
			cr.ShowText ("Nro. de visitas acumuladas de boids por casillero ");
			cr.MoveTo (12, 24);
			cr.ShowText ("por unidad de tiempo");
			cr.Restore ();

		}

		private double[] calcular_mapa_calor(double value, double min, double max){
			double[] color = new double[3];
			double radio=0;
			if(max!=min)
				radio = 2 * (value - min) / (max - min);
			color[2] = Constantes.max(0, (1 - radio));
			color[0] = Constantes.max (0, (radio - 1));
			color[1] = 1 - color[2] - color[0];
			return color;
		}


		private void dibujar_grilla(DrawingArea area, Cairo.Context cr){
			for (int x = 0; x < Constantes.nro_casillas; x++){
				int x0 = Logica.grilla.get_casillero (x, 0).inicioX;
				cr.Save ();
				cr.LineWidth = 0.5;
				cr.SetSourceRGBA (0, 0, 0, 0.5);
				cr.MoveTo (x0, 0);
				cr.LineTo (x0, Constantes.limites.Height);
				cr.Stroke ();
				cr.Restore();
			}
			for (int y = 0; y < Constantes.nro_casillas; y++){
				int y0 = Logica.grilla.get_casillero (0, y).inicioY;
				cr.Save ();
				cr.LineWidth = 0.5;
				cr.SetSourceRGBA (0, 0, 0, 0.5);
				cr.MoveTo (0, y0);
				cr.LineTo (Constantes.limites.Width,y0);
				cr.Stroke ();
				cr.Restore();
			}
		}

		private void dibujar_trayectorias(DrawingArea area, Cairo.Context cr){
			System.Drawing.Point punto;
			for (int i=1; i<Logica.objects.Count; i++) {
				if ( ((BoidAutonomo)Logica.objects[i]).trayectoria.Count > 0) {
					punto =  ((BoidAutonomo)Logica.objects[i]).trayectoria [0];
					foreach (System.Drawing.Point p in ((BoidAutonomo)Logica.objects[i]).trayectoria) {
						cr.Save ();
						cr.LineWidth = 0.5;
						cr.SetSourceRGBA (0, 0, 0, 0.5);
						cr.MoveTo (punto.X, Logica.mapeo (punto.Y));
						cr.LineTo (p.X, Logica.mapeo (p.Y));
						cr.Stroke ();
						cr.Restore ();
						punto = p;
					}
				}
			}
		}

		private void play_simulacion_continua(DrawingArea area, Cairo.Context cr){
			int n = 0;
			TimeSpan temp = new TimeSpan(0);
			if (Logica.play) {
				if (Constantes.iteraciones_total>=2000000000)
					Constantes.iteraciones_total = 0;
				Constantes.iteraciones_total++;
				a1 = DateTime.Now;
				if (Constantes.iteraciones_lapso != 0 && Constantes.tiempo % Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();
				else if (Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();

				Constantes.tiempo++;
				if (Constantes.tiempo > 2000000000) {
					Constantes.tiempo = 0;
					Logica.reset_trayectorias ();
				}

			}
			//PARA DETENER LA SIMULACION EN LA ITERACION 10.000
			if (Constantes.tiempo == 10000)
				Logica.play = false;

			foreach (Boid s in Logica.objects) {

				u=Logica.grilla.ubicacion_boid (s);
				coleccion.Clear ();
				rectangulos.Clear ();
				lineas.Clear ();
				circulos.Clear ();
				for (int x = u.X; x <= u.Width; x++)
					for (int y = u.Y; y <= u.Height; y++) {
						coleccion.Add (Logica.objects [0]);
						coleccion.AddBoids (Logica.grilla.get_casillero (x, y).boids);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).circulos, circulos);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).lineas, lineas);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).rectangulos, rectangulos);
					}
//				TimeSpan total = new TimeSpan (r2.Ticks - r1.Ticks);
//				System.Console.WriteLine (total);
				if (Logica.play) {
				
					if (n == 0 && Logica.mouse) {
						s.calculateVelocity (Logica.objects, area);
					} else if (n != 0) {
						rot = s.calculateRotation ();
						r1 = DateTime.Now;
						if (ventana.contorno.Active)
							triangulo_contorno (cr, s.Location, rot);
						else
							triangulo (cr, s, rot);
						r2 = DateTime.Now;
						temp += new TimeSpan(r2.Ticks - r1.Ticks);
						Constantes.r_acum += new TimeSpan(r2.Ticks - r1.Ticks);

						s.calculateVelocity (coleccion, circulos, rectangulos, lineas);


					}


				} else {
					if (n != 0) {
						rot = s.calculateRotation ();
						if (ventana.contorno.Active)
							triangulo_contorno (cr, s.Location, rot);
						else
							triangulo (cr, s, rot);
					}
							
					}
				n++;
			}
			
			Logica.actualizar_posicion_boids ();
			
			Logica.grilla.reclasificar (Logica.objects);
			
			if (Logica.play) {
				a2 = DateTime.Now;
				Constantes.a_acum += new TimeSpan(a2.Ticks - a1.Ticks) - temp;
				ventana.render.Text ="Tiempo de rendering: "+Constantes.r_acum.ToString()+"    ";
				ventana.algorithm.Text ="Tiempo de algoritmo: "+Constantes.a_acum.ToString();
				ventana.etiquetaIteraciones.Text ="   Cantidad de iteraciones: "+Constantes.iteraciones_total.ToString();

			}
		
		}


		private void play_simulacion_finita(DrawingArea area, Cairo.Context cr){
			int n = 0;
			TimeSpan temp = new TimeSpan(0);
			if (Logica.play) {
				if (Constantes.iteraciones_total>=2000000000)
					Constantes.iteraciones_total = 0;
				Constantes.iteraciones_total++;
				a1 = DateTime.Now;
				if (Constantes.iteraciones_lapso != 0 && Constantes.tiempo % Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();
				else if (Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();

				//PARA DETENER LA SIMULACION EN UNA ITERACION DETERMINADA!!
				if (Constantes.tiempo == 10000)
					Logica.play = false;
				//-----------------------------------------------------------

				Constantes.tiempo++;
				if (Constantes.tiempo > 2000000000) {
					Constantes.tiempo = 0;
					Logica.reset_trayectorias ();
				}

			}

			for (int i=0;i<Logica.objects.Count;i++) {

				u=Logica.grilla.ubicacion_boid (Logica.objects[i]);
				coleccion.Clear ();
				rectangulos.Clear ();
				lineas.Clear ();
				circulos.Clear ();
				for (int x = u.X; x <= u.Width; x++)
					for (int y = u.Y; y <= u.Height; y++) {
						coleccion.Add (Logica.objects [0]);
						coleccion.AddBoids (Logica.grilla.get_casillero (x, y).boids);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).circulos, circulos);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).lineas, lineas);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).rectangulos, rectangulos);
					}
				//				TimeSpan total = new TimeSpan (r2.Ticks - r1.Ticks);
				//				System.Console.WriteLine (total);
				if (Logica.play) {

					if (n == 0 && Logica.mouse) {
						Logica.objects[i].calculateVelocity (Logica.objects, area);
					} else if (n != 0) {
						rot = Logica.objects[i].calculateRotation ();
						r1 = DateTime.Now;
						if (ventana.contorno.Active)
							triangulo_contorno (cr, Logica.objects[i].Location, rot);
						else
							triangulo (cr, Logica.objects[i], rot);
						r2 = DateTime.Now;
						temp += new TimeSpan(r2.Ticks - r1.Ticks);
						Constantes.r_acum += new TimeSpan(r2.Ticks - r1.Ticks);

						Logica.objects[i].calculateVelocity (coleccion, circulos, rectangulos, lineas);

						if (Logica.objects[i].llego) {
							Logica.objects.RemoveAt (i);
							i--;

						}
					}


				} else {
					if (n != 0) {
						rot = Logica.objects[i].calculateRotation ();
						if (ventana.contorno.Active)
							triangulo_contorno (cr, Logica.objects[i].Location, rot);
						else
							triangulo (cr, Logica.objects[i], rot);
					}

				}
				n++;
			}

			Logica.actualizar_posicion_boids ();

			Logica.grilla.reclasificar (Logica.objects);

			if (Logica.play) {
				a2 = DateTime.Now;
				Constantes.a_acum += new TimeSpan(a2.Ticks - a1.Ticks) - temp;
				ventana.render.Text ="Tiempo de rendering: "+Constantes.r_acum.ToString()+"    ";
				ventana.algorithm.Text ="Tiempo de algoritmo: "+Constantes.a_acum.ToString();
				ventana.etiquetaIteraciones.Text ="   Cantidad de iteraciones: "+Constantes.iteraciones_total.ToString();

			}

		}



		private void play_simulacion_continua2(DrawingArea area, Cairo.Context cr){
			int n = 0;
			TimeSpan temp = new TimeSpan(0);
			if (Logica.play) {
				if (Constantes.iteraciones_total>=2000000000)
					Constantes.iteraciones_total = 0;
				Constantes.iteraciones_total++;
				a1 = DateTime.Now;
				if (Constantes.iteraciones_lapso != 0 && Constantes.tiempo % Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();
				else if (Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();

				Constantes.tiempo++;
				if (Constantes.tiempo > 2000000000) {
					Constantes.tiempo = 0;
					Logica.reset_trayectorias ();
				}

			}
			//PARA DETENER LA SIMULACION EN LA ITERACION 10.000
			if (Constantes.tiempo == 10000)
				Logica.play = false;

			foreach (Boid s in Logica.objects) {

				u=Logica.grilla.ubicacion_boid (s);
				coleccion.Clear ();
				rectangulos.Clear ();
				lineas.Clear ();
				circulos.Clear ();
				for (int x = u.X; x <= u.Width; x++)
					for (int y = u.Y; y <= u.Height; y++) {
						coleccion.Add (Logica.objects [0]);
						coleccion.AddBoids (Logica.grilla.get_casillero (x, y).boids);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).circulos, circulos);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).lineas, lineas);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).rectangulos, rectangulos);
					}
				//				TimeSpan total = new TimeSpan (r2.Ticks - r1.Ticks);
				//				System.Console.WriteLine (total);
				if (Logica.play) {

					if (n == 0 && Logica.mouse) {
						s.calculateVelocity (Logica.objects, area);
					} else if (n != 0) {
						s.calculateVelocity (coleccion, circulos, rectangulos, lineas);

					}


				} 
				n++;
			}

			r1 = DateTime.Now;
			n = 0;
			foreach (Boid s in Logica.objects) {
				if (Logica.play) {
					if (n != 0) {
						rot = s.calculateRotation ();
						if (ventana.contorno.Active)
							triangulo_contorno (cr, s.Location, rot);
						else
							triangulo (cr, s, rot);
					}
				} else {
					if (n != 0) {
						rot = s.calculateRotation ();
						if (ventana.contorno.Active)
							triangulo_contorno (cr, s.Location, rot);
						else
							triangulo (cr, s, rot);
					}
				}
				n++;
			}

			r2 = DateTime.Now;
			temp += new TimeSpan(r2.Ticks - r1.Ticks);
			Constantes.r_acum += new TimeSpan(r2.Ticks - r1.Ticks);

			Logica.actualizar_posicion_boids ();

			Logica.grilla.reclasificar (Logica.objects);

			if (Logica.play) {
				a2 = DateTime.Now;
				Constantes.a_acum += new TimeSpan(a2.Ticks - a1.Ticks) - temp;
				ventana.render.Text ="Tiempo de rendering: "+Constantes.r_acum.ToString()+"    ";
				ventana.algorithm.Text ="Tiempo de algoritmo: "+Constantes.a_acum.ToString();
				ventana.etiquetaIteraciones.Text ="   Cantidad de iteraciones: "+Constantes.iteraciones_total.ToString();

			}

		}

		private void play_simulacion_finita2(DrawingArea area, Cairo.Context cr){
            int n = 0;
			TimeSpan temp = new TimeSpan(0);
			if (Logica.play) {
				if (Constantes.iteraciones_total>=2000000000)
					Constantes.iteraciones_total = 0;
				Constantes.iteraciones_total++;
				a1 = DateTime.Now;
				if (Constantes.iteraciones_lapso != 0 && Constantes.tiempo % Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();
				else if (Constantes.iteraciones_lapso == 0)
					Logica.add_boids_iteracion ();

				//PARA DETENER LA SIMULACION EN UNA ITERACION DETERMINADA!!
				if (Constantes.tiempo == 10000)
					Logica.play = false;
				//-----------------------------------------------------------

				Constantes.tiempo++;
				if (Constantes.tiempo > 2000000000) {
					Constantes.tiempo = 0;
					Logica.reset_trayectorias ();
				}

			}

			for (int i=0;i<Logica.objects.Count;i++) {

				u=Logica.grilla.ubicacion_boid (Logica.objects[i]);
				coleccion.Clear ();
				rectangulos.Clear ();
				lineas.Clear ();
				circulos.Clear ();
				for (int x = u.X; x <= u.Width; x++)
					for (int y = u.Y; y <= u.Height; y++) {
						coleccion.Add (Logica.objects [0]);
						coleccion.AddBoids (Logica.grilla.get_casillero (x, y).boids);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).circulos, circulos);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).lineas, lineas);
						Constantes.addObstacles (Logica.grilla.get_casillero (x, y).rectangulos, rectangulos);
					}
				//				TimeSpan total = new TimeSpan (r2.Ticks - r1.Ticks);
				//				System.Console.WriteLine (total);
				if (Logica.play) {

					if (n == 0 && Logica.mouse) {
						Logica.objects[i].calculateVelocity (Logica.objects, area);
					} else if (n != 0) {
						Logica.objects[i].calculateVelocity (coleccion, circulos, rectangulos, lineas);

						if (Logica.objects[i].llego) {
							Logica.objects.RemoveAt (i);
							i--;

						}
					}

				} 

			n++;
			}
			r1 = DateTime.Now;
			n = 0;
			foreach (Boid s in Logica.objects) {
				
					if (n != 0) {
						rot = s.calculateRotation ();
                        if (ventana.contorno.Active)
                            triangulo_contorno(cr, s.Location, rot);
                        else
                            triangulo(cr, s, rot);

					}
				n++;
			}

			r2 = DateTime.Now;
			temp += new TimeSpan(r2.Ticks - r1.Ticks);
			Constantes.r_acum += new TimeSpan(r2.Ticks - r1.Ticks);

			Logica.actualizar_posicion_boids ();

			Logica.grilla.reclasificar (Logica.objects);
            

			if (Logica.play) {
				a2 = DateTime.Now;
				Constantes.a_acum += new TimeSpan(a2.Ticks - a1.Ticks) - temp;
				ventana.render.Text ="Tiempo de rendering: "+Constantes.r_acum.ToString()+"    ";
				ventana.algorithm.Text ="Tiempo de algoritmo: "+Constantes.a_acum.ToString();
				ventana.etiquetaIteraciones.Text ="   Cantidad de iteraciones: "+Constantes.iteraciones_total.ToString();

			}

		}



		private void scene_design(DrawingArea area, Cairo.Context cr){

			area.AddEvents ((int) Gdk.EventMask.ButtonPressMask);
			area.ButtonPressEvent += delegate(object o, ButtonPressEventArgs arg) {
				area.GetPointer(out xo, out yo);
				pressed = true;
			};

			area.AddEvents ((int)Gdk.EventMask.ButtonReleaseMask);
			area.ButtonReleaseEvent += delegate(object o, ButtonReleaseEventArgs args) {
				pressed = false;
				UpdateViewSurface();
			};


			if (pressed) {
				area.GetPointer (out xt, out yt);
				if(ventana.circulo.Active){
					cr.LineWidth = 2;
					cr.SetSourceRGBA (0, 0, 1, 0.5);
					cr.Arc (xo, yo, Constantes.distancia(xt,xo),0,Math.PI*2);
					cr.Stroke ();
				}
				else if(ventana.linea.Active){
					cr.LineWidth = 2;
					cr.SetSourceRGBA (0, 1, 0, 0.5);
					cr.MoveTo (xo, yo);
					cr.LineTo (xt, yt);
					cr.Stroke ();

				}
				else if (ventana.rectangulo.Active){
					cr.LineWidth = 2;
					cr.SetSourceRGBA (1, 0, 0, 0.5);
					cr.Rectangle (xo, yo, xt - xo, yt - yo);
					cr.Stroke ();
				}

			}

			cr.SetSourceSurface(Constantes.viewSurface, 0, 0); 
			cr.Paint();

		}

		private void UpdateViewSurface() 
		{	
			if (Constantes.escenario) {
				//whenever we want 
				//draw onto our surface in memory 
				using (Context cr = new Context (Constantes.viewSurface)) { 
					if (ventana.circulo.Active) {
						if (!Constantes.contiene(Logica.circulos,new Obstaculo(xo, yo, Constantes.distancia (xt, xo), 0, (int)ventana.intermitencia.Value)) && Constantes.distancia (xt, xo) != 0)
							Logica.circulos.Add (new Obstaculo (xo, yo, Constantes.distancia (xt, xo), 0, (int)ventana.intermitencia.Value));
						cr.LineWidth = 2;
						cr.SetSourceRGBA (0, 0, 1, 1);
						cr.Arc (xo, yo, Constantes.distancia (xt, xo), 0, Math.PI * 2);
						cr.Stroke ();
					} else if (ventana.linea.Active) {
						if (!Constantes.contiene (Logica.lineas, new Obstaculo (xo, yo, xt, yt, (int)ventana.intermitencia.Value)) && Constantes.distancia_dos_puntos (xo, yo, xt, yt) != 0)
							Logica.lineas.Add (new Obstaculo (xo, yo, xt, yt, (int)ventana.intermitencia.Value));
						cr.LineWidth = 2;
						cr.SetSourceRGBA (0, 1, 0, 1);
						cr.MoveTo (xo, yo);
						cr.LineTo (xt, yt);
						cr.Stroke ();

					} else if (ventana.rectangulo.Active) {
						if (!Constantes.contiene (Logica.rectangulos, new Obstaculo (xo, yo, xt - xo, yt - yo, (int)ventana.intermitencia.Value)) && (Constantes.distancia (xt, xo) != 0 || Constantes.distancia (yt, yo) != 0))
							Logica.rectangulos.Add (new Obstaculo (xo, yo, xt - xo, yt - yo, (int)ventana.intermitencia.Value));
						cr.Rectangle (xo, yo, xt - xo, yt - yo);
						cr.LineWidth = 2;
						cr.SetSourceRGBA (1, 0, 0, 1);
						cr.Stroke ();

					} else if (ventana.puntoInicio.Active) {
						if (!Logica.puntos_inicio.Contains (new System.Drawing.Point (xo, yo)))
							Logica.puntos_inicio.Add (new System.Drawing.Point (xo, yo));
						cr.SetSourceRGBA (1, 0, 1, 0.8);
						cr.Arc (xo, yo, 10, 0, 2 * Math.PI);
						cr.Fill ();
						cr.Stroke ();

					} else if (ventana.puntoObjetivo.Active) {
						int n = (int)ventana.nivel.Value;
						if (ventana.nivel.Value == Constantes.max_nivel) {
							Logica.puntos_objetivo.Add (new Objetivo ());
							Constantes.max_nivel++;
							ventana.nivel.SetRange (1, Constantes.max_nivel);
						}
						if (!Logica.puntos_objetivo [(int)ventana.nivel.Value - 1].contains (xo, yo))
							Logica.puntos_objetivo [(int)ventana.nivel.Value - 1].add_objetivo (xo, yo);
						cr.SetSourceRGBA (1, 0, 0, 0.6);
						cr.Arc (xo, yo, Constantes.radio_objetivos, 0, 2 * Math.PI);
						cr.Fill ();
						cr.Stroke ();
						cr.SetSourceRGB (0,0,0);
						cr.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
						cr.SetFontSize (12);
						TextExtents t = cr.TextExtents (n.ToString());
						cr.MoveTo (xo-4, yo+4);
						cr.ShowText (n.ToString());

					}


					cr.Dispose ();
				}
			}
		}

		private void escenarioCargado(Context cr){
//			cr.SetSourceSurface(Constantes.escenarioSurface, 0, 0); 
//			cr.Paint();
			foreach (Obstaculo o in Logica.circulos) {
				if (o.intermitencia != 0 && (Constantes.tiempo % o.intermitencia) == 0)
					o.enable = !o.enable;
				if (o.enable || !Logica.play || !Constantes.simulacion) {
					cr.Save ();
					cr.LineWidth = 2;
					cr.SetSourceRGBA (0, 0, 1, 1);
					cr.Arc (o.rectangle.X, o.rectangle.Y, o.rectangle.Width, o.rectangle.Height, Math.PI * 2);
					cr.Stroke ();
					cr.Restore ();
				}
			}
			foreach (Obstaculo o in Logica.rectangulos) {
				if (o.intermitencia != 0 && (Constantes.tiempo % o.intermitencia) == 0)
					o.enable = !o.enable;
				if (o.enable || !Logica.play || !Constantes.simulacion) {
					cr.Save ();
					cr.Rectangle (o.rectangle.X, o.rectangle.Y, o.rectangle.Width, o.rectangle.Height);
					cr.LineWidth = 2;
					cr.SetSourceRGBA (1, 0, 0, 1);
					cr.Stroke ();
					cr.Restore ();
				}
			}
			foreach (Obstaculo o in Logica.lineas) {
				if (o.intermitencia != 0 && (Constantes.tiempo % o.intermitencia) == 0)
					o.enable = !o.enable;
				if (o.enable || !Logica.play || !Constantes.simulacion) {
					cr.Save ();
					cr.LineWidth = 2;
					cr.SetSourceRGBA (0, 1, 0, 1);
					cr.MoveTo (o.rectangle.X, o.rectangle.Y);
					cr.LineTo (o.rectangle.Width, o.rectangle.Height);
					cr.Stroke ();
					cr.Restore ();
				}
			}


			foreach (System.Drawing.Point p in Logica.puntos_inicio) {
				cr.Save ();
				cr.SetSourceRGBA (1, 0, 1, 0.8);
				cr.Arc (p.X, p.Y, 10, 0, 2 * Math.PI);
				cr.Fill ();
				cr.Stroke ();
				cr.Restore ();
			}
			int n = 1;
			foreach (Objetivo o in Logica.puntos_objetivo) {
				foreach (System.Drawing.Point p in o.Objetivos()) {
					cr.Save ();
					cr.SetSourceRGBA (1, 0, 0, 0.6);
					cr.Arc (p.X, p.Y, Constantes.radio_objetivos, 0, 2 * Math.PI);
					cr.Fill ();
					cr.Stroke ();
					cr.SetSourceRGB (0,0,0);
					cr.SelectFontFace ("Arial", FontSlant.Normal, FontWeight.Normal);
					cr.SetFontSize (12);
					TextExtents t = cr.TextExtents (n.ToString());
					cr.MoveTo (p.X-4, p.Y+4);
					cr.ShowText (n.ToString());
					cr.Restore ();
				}
				n++;
			}
		}

		bool OnTimer() 
		{ 
			if (!timer) 
				return false;
			darea.QueueDraw();
			return true;
		} 



		public static void Main()
		{
			Application.Init();
			new SharpApp();
			Application.Run();
		}
	}
}