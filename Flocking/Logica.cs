using System;
using System.Drawing;
//using Cairo;
using System.Collections.Generic;


namespace Nuevo
{
	public class Logica
	{
		//Configuracion fija
		private static double fcoefCoh = 0.2; // 0.2 
		private static double fcoefAli = 0.1; // 0.5
		private static double fcoefSep = 1.0; 

		private static double fradioCoh = 30; // 50
		private static double fradioAli = 30; // 50
		private static double fradioSep = 30; // 30

		private static double fmax_speed = 2;
		private static double fmax_force = 0.5;

		private static double ftamanio_boid = 4;

		private static bool frebotar = false;
		private static bool fatravesar = true;

		private static bool fmouse = false;

//...........................................................................
		public static double coefCoh = fcoefCoh;
		public static double coefAli = fcoefAli;
		public static double coefSep = fcoefSep;

		public static double radioCoh = fradioCoh;
		public static double radioAli = fradioAli;
		public static double radioSep = fradioSep;

		public static double max_speed = fmax_speed;
		public static double max_force = fmax_force;

		public static double tamanio_boid = ftamanio_boid;

		public static bool rebotar = frebotar;
		public static bool atravesar = fatravesar;

		public static bool mouse = fmouse;



		public static bool play = false;

		public static int nro_boids = 100;

		public static BoidCollection objects = new BoidCollection ();

		public static List<Point> puntos_inicio = new List<Point> ();

		public static List<Objetivo> puntos_objetivo = new List<Objetivo> ();

		public static List<Obstaculo> rectangulos = new List<Obstaculo>();
		public static List<Obstaculo> lineas = new List<Obstaculo>();
		public static List<Obstaculo> circulos = new List<Obstaculo>();


		public static Grilla grilla;

		private static int cantidad_parcial;
		private static int cantidad_total;
		public static int lapso_temporal;

		public static void crear_boids(){
			grilla.limpiar_boids ();
			grilla.resetVisitas ();
			Random r = new Random ();
			objects.Clear ();
			cantidad_parcial = nro_boids/Constantes.lapsos;
			cantidad_total = 0;
			lapso_temporal = 1;

			Boid b;
			b = new Mouse();
			b.Location = new Vector(1, mapeo(1));
			b.LocationNueva = new Vector(1, mapeo(1));
			b.Velocity = new Vector(0, 0);
			objects.Add(b);

		}
			
		public static void add_boids_iteracion(){
			if (lapso_temporal < Constantes.lapsos) {
				Boid b;
				Random r = new Random ();
				for (int i = 0; i < cantidad_parcial; i++) {
					b = new BoidAutonomo (lapso_temporal, r);
					objects.Add (b);
				}
				lapso_temporal++;
				cantidad_total += cantidad_parcial;
				grilla.reclasificar (objects);

			} else if (lapso_temporal == Constantes.lapsos) {
				Boid b;
				int n = nro_boids - cantidad_total;
				Random r = new Random ();
				for (int i = 0; i < n; i++) {
					b = new BoidAutonomo (lapso_temporal, r);
					objects.Add (b);
				}
				lapso_temporal++;
				cantidad_total += n;
				grilla.reclasificar (objects);
			}
				
		}

		public static double mapeo(double y)
		{	
            return (Constantes.limites.Height - y);
		}
			

		public static void reset(){
			coefCoh = fcoefCoh;
			coefAli = fcoefAli;
			coefSep = fcoefSep;

			radioCoh = fradioCoh;
			radioAli = fradioAli;
			radioSep = fradioSep;

			max_speed = fmax_speed;
			max_force = fmax_force;

			tamanio_boid = ftamanio_boid;

			rebotar = frebotar;
			atravesar = fatravesar;

			mouse = fmouse;

			grilla.resetVisitas ();
		}
			
			
		public static bool avoid_circulo(double newx, double newy,Vector velocity, Cairo.Rectangle circulo,Vector location)
		{

			double dSiguiente = Constantes.distancia_dos_puntos ((int)newx, (int)newy, (int)circulo.X, (int)mapeo ((int)circulo.Y));
			double dActual= Constantes.distancia_dos_puntos ((int)location.X, (int)location.Y, (int)circulo.X, (int)mapeo ((int)circulo.Y));
			double velX, velY;
	
			if ((dActual>=circulo.Width + 10 && dSiguiente <= circulo.Width + 10)) {
				Vector n = new Vector (newx - circulo.X, newy - mapeo(circulo.Y));
				n.normalize ();
				Vector d = new Vector (velocity.X, velocity.Y);

				double escalar = 2 * (d.escalar (n));
				Vector r = new Vector (d.X - (escalar * n.X), d.Y - (escalar * n.Y));
				velX = r.X;
				velY = r.Y;
				newx = location.X + velX;
				newy = location.Y + velY;

				double dNueva = Constantes.distancia_dos_puntos ((int)newx, (int)newy, (int)circulo.X, (int)mapeo ((int)circulo.Y));
				if (dNueva <= circulo.Width + 10) {
					velX = velocity.X * -1;
					velY = velocity.Y * -1;

				}

				velocity.X = velX;
				velocity.Y = velY;

				Constantes.reboto = true;
				return true;
			}

			return false;
		}



		public static bool avoid_linea(double cx, double cy,Vector velocity, Cairo.Rectangle linea,Vector location)
		{
			bool reb=avoid_circulo(cx, cy, velocity, new Cairo.Rectangle(linea.X,linea.Y,2,0), location);
			if (reb)
				return true;
			reb = avoid_circulo(cx, cy, velocity, new Cairo.Rectangle(linea.Width,linea.Height,2,0), location);
			if (reb)
				return true;
			double r = 10;
			double ax = linea.X, ay= mapeo(linea.Y);
			double bx = linea.Width, by = mapeo (linea.Height);
			Vector d = new Vector (bx - ax, by - ay);
			Vector f = new Vector (ax - cx, ay - cy);
			double a = d.escalar( d ) ;
			double b = 2*f.escalar( d ) ;
			double c = f.escalar( f ) - r*r ;

			double discriminant = b*b-4*a*c;
			if( discriminant < 0 )
			{
				return false;
			}
			else
			{

				discriminant = Math.Sqrt( discriminant );
			
				double t1 = (-b - discriminant)/(2*a);
				double t2 = (-b + discriminant)/(2*a);

				if( t1 >= 0 && t1 <= 1 )
				{

					Vector v1 = new Vector (bx - ax, by - ay);
					Vector u2 = new Vector (1, 1);
					double u2u1 = u2.X * v1.X + u2.Y * v1.Y;
					double u1u1 = v1.X * v1.X + v1.Y * v1.Y;
					Vector n = new Vector (u2.X - ((u2u1 / u1u1) * v1.X), u2.Y - ((u2u1 / u1u1) * v1.Y));
					n.normalize ();
					Vector de = new Vector (velocity.X, velocity.Y);
					double escalar = 2 * (de.escalar (n));
					Vector re = new Vector (de.X - (escalar * n.X), de.Y - (escalar * n.Y));
					velocity.X = re.X;
					velocity.Y = re.Y;

					Constantes.reboto = true;
					return true;

				}
					
				if( t2 >= 0 && t2 <= 1 )
				{
					// ExitWound
					Vector v1 = new Vector (bx - ax, by - ay);
					Vector u2 = new Vector (1, 1);
					double u2u1 = u2.X * v1.X + u2.Y * v1.Y;
					double u1u1 = v1.X * v1.X + v1.Y * v1.Y;
					Vector n = new Vector (u2.X - ((u2u1 / u1u1) * v1.X), u2.Y - ((u2u1 / u1u1) * v1.Y));
					n.normalize ();
					Vector de = new Vector (velocity.X, velocity.Y);
					double escalar = 2 * (de.escalar (n));
					Vector re = new Vector (de.X - (escalar * n.X), de.Y - (escalar * n.Y));
					velocity.X = re.X;
					velocity.Y = re.Y;

					Constantes.reboto = true;
					return true ;

				}

				// no intn: FallShort, Past, CompletelyInside
				return false ;
			}


		}


		public static bool avoid_rectangulo(double newx, double newy,Vector velocity,Cairo.Rectangle r,Vector location){
			bool reboto;
			reboto = Logica.avoid_linea (newx, newy, velocity, new Cairo.Rectangle (r.X, r.Y, r.X+r.Width, r.Y),location);
			if (reboto)
				return true;
			reboto = Logica.avoid_linea (newx, newy, velocity, new Cairo.Rectangle(r.X+r.Width,r.Y,r.X+r.Width,r.Y+r.Height),location);
			if (reboto)
				return true;
			reboto = Logica.avoid_linea (newx, newy, velocity, new Cairo.Rectangle(r.X+r.Width,r.Y+r.Height,r.X,r.Y+r.Height),location);
			if (reboto)
				return true;
			reboto = Logica.avoid_linea (newx, newy, velocity, new Cairo.Rectangle(r.X,r.Y+r.Height,r.X,r.Y),location);
			if (reboto)
				return true;
			return false;

		}
			
		public static void reset_trayectorias(){
			for (int i = 1; i < objects.Count; i++)
				((BoidAutonomo)objects[i]).trayectoria.Clear ();
		}

		public static Point calcular_vecinos_estadistica(){
			int n = 0;
			int max = -1;
			int min = Logica.nro_boids + 10;

			foreach (Boid b in Logica.objects) {
				if (n != 0) {
					((BoidAutonomo)b).calcular_vecinos ();
					if (b.vecinos > max)
						max = b.vecinos;
					if (b.vecinos < min)
						min = b.vecinos;
				}
				n++;
			}
			return new Point (min,max);
		}

		public static void clear_puntos_objetivo(){
			foreach (Objetivo o in puntos_objetivo) {
				o.Objetivos ().Clear ();
			}
			puntos_objetivo.Clear ();
		}

		public static void actualizar_posicion_boids(){
			foreach(Boid b in objects){
				b.Location.X = b.LocationNueva.X;
				b.Location.Y = b.LocationNueva.Y;
			}
		}
			
	}
}
