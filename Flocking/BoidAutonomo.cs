using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;

namespace Nuevo
{
	public class BoidAutonomo:Boid
	{
		int tiempo_inicio;
		Point intermedio;
		Random random;
		public List<Point> trayectoria;
		int nro_iteracion;
//		Vector locacion_temp;
//		bool mover = true;
//

		public BoidAutonomo(int lapso, Random r)
		{
            ogreRef = IntPtr.Zero;
            rotacion = 0;
			llego = false;
			nro_iteracion = 0;
			this.objetivo = 0;
			random = r;
			tiempo_inicio = Constantes.tiempo;
			seleccionarCriterio (r);
			trayectoria = new List<Point> ();
			this.vecinos = 0;
			this.Acceleration = new Vector(0, 0);
			double angle = r.NextDouble()*2*Math.PI;
			this.Velocity = new Vector(Math.Cos(angle), Math.Sin(angle));
            this.Velocity.mult(4);
//			this.Location = new Vector(Constantes.limites.Width/2, Constantes.limites.Height/2);
//			this.Velocity = new Vector(0, 1);
			if (Logica.puntos_inicio.Count > 0) {
				Point p = Logica.puntos_inicio.ElementAt ((int)((lapso - 1) % Logica.puntos_inicio.Count)); //el punto de inicio es numero de lapso MOD cantidad ptos inicio
				this.Location = new Vector (p.X, Logica.mapeo (p.Y));
			} else
                this.Location = new Vector(Constantes.limites.Width / 2, Constantes.limites.Height / 2);
                //this.Location = new Vector (r.NextDouble () * Constantes.limites.Width, r.NextDouble () * Constantes.limites.Height);

//			locacion_temp = new Vector (Location.X,Location.Y);

			LocationNueva = new Vector (Location.X,Location.Y);

			if (Logica.puntos_objetivo.Count > 0) { //Comprobar si elige el objetivo mas corto

					if (criterio == 0)
						intermedio = Logica.puntos_objetivo [objetivo].get_cercano (Location);
					else if (criterio == 1)
						intermedio = Logica.puntos_objetivo [objetivo].get_menos_concurrido (Location);
					else
						intermedio = Logica.puntos_objetivo [objetivo].get_random (random);
			}

		}

		//-------------------------------FLOCKING PROPERTIES-------------------------------------------------------------------------

		public override void calculateVelocity(BoidCollection boids, List<Obstaculo> c, List<Obstaculo> r, List<Obstaculo> l)
		{	

//			if (Constantes.frenado)
//				calculateVelocityFrenado (boids, c, r, l);
//			else
				calculateVelocityNormal (boids, c, r, l);

		}

		private void calculateVelocityNormal(BoidCollection boids, List<Obstaculo> c, List<Obstaculo> r, List<Obstaculo> l)
		{
			update_objetivo ();
			flock (boids);
			update (boids, c, r, l,1);
		}

//		private void calculateVelocityFrenado(BoidCollection boids, List<Obstaculo> c, List<Obstaculo> r, List<Obstaculo> l)
//		{
//			if (nro_iteracion == 50) {
//
//				if (this.locacion_temp.distance (Location) > Constantes.zona_confort) {
//					mover = true;
//					locacion_temp.X = Location.X;
//					locacion_temp.Y = Location.Y;
//				} else {
//					mover = false;
//
//				}
//			}
//			if (nro_iteracion == 100) {
//				nro_iteracion = 0;
//				mover = true;
//			}
//
//			if (mover) {
//				update_objetivo ();
//				flock (boids);
//				update (boids, c, r, l, 1);
//
//			} else {
//				if (nro_iteracion % 2 == 0) {
//					update_objetivo ();
//					flock (boids);
//					update (boids, c, r, l, 0.85);
//				}
//			}
//			nro_iteracion++;
//		}

		private void flock(BoidCollection boids) //Resultante de las 3 propiedades = Acceleration
		{
			Vector sep = separate(boids);
			Vector ali = align(boids);
			Vector coh = cohesion(boids);

			sep.mult(Logica.coefSep);
			ali.mult(Logica.coefAli);
			coh.mult(Logica.coefCoh);

			Acceleration.add(sep);
			Acceleration.add(ali);
			Acceleration.add(coh);

			if (Logica.play && Constantes.tiempo%10 == 0) {
				trayectoria.Add (new Point ((int)Location.X, (int)Location.Y));

			}

		}



		private void update(BoidCollection boids, List<Obstaculo> C, List<Obstaculo> R, List<Obstaculo> L, double magnitud) //Revisa los limites y calcula los rebotes
		{

			Velocity.add(Acceleration); //Update velocity
			Velocity.limit(Logica.max_speed);

			double newx = this.Location.X + this.Velocity.X;
			double newy = this.Location.Y + this.Velocity.Y;
			double velOrigX = this.Velocity.X;
			double velOrigY = this.Velocity.Y;

			Constantes.reboto = false;
			for (int i = 0; i < C.Count () && !Constantes.reboto; i++)
				if(C.ElementAt(i).enable)
					Logica.avoid_circulo (newx, newy, this.Velocity, C.ElementAt (i).rectangle, this.Location);
			for (int i = 0; i < R.Count () && !Constantes.reboto; i++)
				if(R.ElementAt(i).enable)
					Logica.avoid_rectangulo (newx, newy, this.Velocity, R.ElementAt (i).rectangle, this.Location);
			for (int i = 0; i < L.Count () && !Constantes.reboto; i++)
				if(L.ElementAt(i).enable)
					Logica.avoid_linea (newx, newy, this.Velocity, L.ElementAt (i).rectangle, this.Location);



			//rebota o atraviesa los bordes de la pantalla
			if (Logica.rebotar)
			{
				if (newx > Constantes.limites.Right - 10 || newx < Constantes.limites.Left + 10)
					this.Velocity.X = -1 * this.Velocity.X;

				if (newy > Constantes.limites.Bottom - 40 || newy < Constantes.limites.Top + 20)
					this.Velocity.Y = -1 * (this.Velocity.Y);
			}

			else if (Logica.atravesar)
			{
				if (newx < 0) 
					LocationNueva.X = Constantes.limites.Width;
				if (newy < 0) 
					LocationNueva.Y = Constantes.limites.Height;
				if (newx > Constantes.limites.Width) 
					LocationNueva.X = 0;
				if (newy > Constantes.limites.Height) 
					LocationNueva.Y = 0;
			}

			Velocity.X *= magnitud;
			Velocity.Y *= magnitud;
			LocationNueva.add(Velocity);

			//Reset acceleration
			Acceleration.X = 0;
			Acceleration.Y = 0;
		}

		private void update_objetivo(){
			if (Logica.puntos_objetivo.Count > 0 && objetivo < Logica.puntos_objetivo.Count) {
				double distancia = Location.distance (new Vector (intermedio.X, Logica.mapeo (intermedio.Y)));
				if (distancia <= Constantes.radio_objetivos) { //ver cuando cambiar el objetivo
					objetivo++;
					if (objetivo < Logica.puntos_objetivo.Count && Logica.puntos_objetivo [objetivo].Objetivos().Count>0) {

						if (criterio == 0)
							intermedio = Logica.puntos_objetivo [objetivo].get_cercano (Location);
						else if (criterio == 1)
							intermedio = Logica.puntos_objetivo [objetivo].get_menos_concurrido (Location);
						else
							intermedio = Logica.puntos_objetivo [objetivo].get_random (random);
					}

					else {

						objetivo = 0;
						actualizarMetricas ();

						if (Constantes.simulacionContinua) {
							ubicar_boid_punto_inicio ();
							trayectoria.Clear ();

						} else {
							llego = true;
						}

						if (criterio == 0)
							intermedio = Logica.puntos_objetivo [objetivo].get_cercano (LocationNueva);
						else if (criterio == 1)
                            intermedio = Logica.puntos_objetivo[objetivo].get_menos_concurrido(LocationNueva);
						else
							intermedio = Logica.puntos_objetivo [objetivo].get_random (random);

					}

				}

			}
		}

		private Vector seek(Vector target)
		{
			Vector desired = target.sub(target, this.Location);
			desired.normalize();
			desired.mult(Logica.max_speed);
			Vector steer=desired.sub(desired,this.Velocity);
			steer.limit(Logica.max_force);
			return steer;
		}

		private Vector separate(BoidCollection boids) // promedio de las distancias del boid con sus vecinos
		{
			Vector sum = new Vector(0,0);
			int neighbors=0;
			vecinos = 0;
			int n=0;
			foreach (Boid b in boids)
			{
				if (n != 0) {
					double d = Location.distance (b.Location);
					if (d > 0 && d < Logica.radioSep) {
						Vector diff = Location.sub (Location, b.Location);
						diff.normalize ();
						diff.div (d);
						sum.add (diff);
						neighbors++;
						vecinos++;
					}

				}
				n++;
			}
				

			if (neighbors > 0)
				sum.div(neighbors);

			if (sum.magnitude() > 0)
			{
				sum.normalize();
				sum.mult(Logica.max_speed);
				sum.sub(sum, this.Velocity);
				sum.limit(Logica.max_force);
			}

			return sum;

		}

		private Vector align(BoidCollection boids) //promedio de las velocidades de los vecinos
		{
			Vector sum = new Vector(0, 0);
			int neighbors = 0;
			int n = 0;
			if ( Logica.puntos_objetivo.Count > 0 && objetivo < Logica.puntos_objetivo.Count ) {

				Vector v;
				if (intermedio.X > Location.X) 
					v = new Vector (intermedio.X - Location.X, Logica.mapeo(intermedio.Y) - Location.Y);
				else
					v = new Vector (Location.X - intermedio.X, Location.Y - Logica.mapeo(intermedio.Y));
				v.normalize ();
				v.mult (0.5);
				sum.add (v);
				neighbors++;
			}

			else {

				foreach (Boid b in boids)
				{
					double d = Location.distance(b.Location);
					if ((Logica.mouse && n == 0) || (n != 0 && d > 0 && d < Logica.radioAli))
					{
						sum.add(b.Velocity);
						neighbors++;
					}
					n++;
				}
			
			}

			if (neighbors > 0)
			{
				sum.div(neighbors);
				sum.normalize();
				sum.mult(Logica.max_speed);
				Vector steer = sum.sub(sum, this.Velocity);
				steer.limit(Logica.max_force);
				return steer;
			}
			else
				return new Vector(0,0);

		}


		private Vector cohesion(BoidCollection boids) //promedio de las posiciones de los vecinos dentro del radio
		{

			Vector sum = new Vector(0, 0);
			int neighbors = 0;
			int n = 0;
			if ( Logica.puntos_objetivo.Count > 0 && objetivo < Logica.puntos_objetivo.Count ) {
				Vector nuevo = new Vector (intermedio.X, Logica.mapeo (intermedio.Y));
				sum.add(nuevo);
				neighbors++;
			}

			else {
				foreach (Boid b in boids) {
					double d = Location.distance (b.Location);
					if ((Logica.mouse && n == 0) || (n != 0 && d > 0 && d < Logica.radioCoh)) {
						sum.add (b.Location);
						neighbors++;

					}
					n++;
				}
			}

			if (neighbors > 0)
			{
				sum.div(neighbors);
				return seek(sum);
			}

			return sum;
		}


		public void calcular_vecinos(){
			int n = 0;
			vecinos = 0;
			foreach (Boid b in Logica.objects)
			{
				double d = Location.distance(b.Location);
				if ((Logica.mouse && n == 0) || (n!=0 && d > 0 && d < Constantes.radio_vecinos))
				{
					vecinos++;
				}
				n++;
			}
		}

		void detener_boid ()
		{
			this.Velocity.X = 0;
			this.Velocity.Y = 0;

		}

		private void ubicar_boid_punto_inicio(){
			Random r = new Random ();
			if (Logica.puntos_inicio.Count > 0) {
				double nro = Math.Round(r.NextDouble () * (Logica.puntos_inicio.Count - 1));
				Point p = Logica.puntos_inicio[(int)nro]; //el punto de inicio es numero de lapso MOD cantidad ptos inicio
				this.LocationNueva = new Vector (p.X, Logica.mapeo (p.Y));
			}
			else
				this.LocationNueva = new Vector(Constantes.limites.Width/2, Constantes.limites.Height/2);
		}

		private void actualizarMetricas ()
		{	
			if (Constantes.tiempo_acum > 2000000000)
				Constantes.resetEstadisticas ();
			if (Constantes.tiempos_boids.Count > 10000)
				Constantes.tiempos_boids.Clear ();
			int tiempo = Constantes.tiempo - this.tiempo_inicio;
			Constantes.tiempos_boids.Add (tiempo);
			Constantes.tiempo_acum += tiempo;
			Constantes.nro_trayectorias++;
			if (tiempo < Constantes.min_tiempo)
				Constantes.min_tiempo = tiempo;
			if (tiempo > Constantes.max_tiempo)
				Constantes.max_tiempo = tiempo;
			this.tiempo_inicio = Constantes.tiempo;

		}

		private void seleccionarCriterio(Random r){
		
			if (Constantes.objetivoCercano && Constantes.objetivoAzar && Constantes.objetivoLibre) {
				criterio = r.Next (0, 3);
			} else if (Constantes.objetivoCercano && Constantes.objetivoLibre) {
				if (r.NextDouble () < 0.5)
					criterio = 0;
				else
					criterio = 1;
			} else if (Constantes.objetivoCercano && Constantes.objetivoAzar) {
				if (r.NextDouble () < 0.5)
					criterio = 0;
				else
					criterio = 2;
			} else if (Constantes.objetivoAzar && Constantes.objetivoLibre) {
				if (r.NextDouble () < 0.5)
					criterio = 1;
				else
					criterio = 2;
			} else if (Constantes.objetivoCercano)
				criterio = 0;
			 else if (Constantes.objetivoLibre)
				criterio = 1;
			 else if (Constantes.objetivoAzar)
				criterio = 2;
		}
	}

}

