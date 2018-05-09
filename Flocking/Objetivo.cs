using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nuevo
{
	public class Objetivo
	{

		List<Point> objetivos;

		public Objetivo ()
		{
			objetivos = new List<Point> ();
		}

		public void add_objetivo(int x, int y){
			Point p = new Point (x, y);
			objetivos.Add (p);
		}

		public Point get_element(int i){
			return objetivos [i];
		}

		public bool contains(int x, int y){
			Point p = new Point (x, y);
			return objetivos.Contains (p);
		}

		public Point get_random(Random r){
			double nro = Math.Round((r.NextDouble () * (objetivos.Count - 1)));
//			System.Console.WriteLine (objetivos.Count);
			if(nro<objetivos.Count)
				return objetivos[(int)nro];
			return objetivos [objetivos.Count - 1];

		}

		public Point get_cercano(Vector location){
			double distancia = 10000;
			Point final = objetivos[0]; 
			foreach (Point p in objetivos) {
				double temporal = location.distance (new Vector(p.X,Logica.mapeo(p.Y)));
				if (temporal < distancia) {
					final = p;
					distancia = temporal;
				}
			}
			return final;
		}

		public Point get_menos_concurrido(Vector location){
			int min = Logica.nro_boids+10;
			int n = 0;
			Point final = objetivos [0];
			foreach (Point p in objetivos) {
				n = 0;
				foreach (Boid b in Logica.objects)
					if (b.Location.distance (new Vector (p.X, Logica.mapeo (p.Y))) <= Constantes.radio_objetivos * 2)
						n++;
				if (n < min) {
					final = p;
					min = n;
				} else if (n == min) {
					if (location.distance (new Vector (p.X, Logica.mapeo (p.Y))) < location.distance (new Vector (final.X, Logica.mapeo (final.Y))))
						final = p;
				}
			}
			return final;
		}

		public List<Point> Objetivos(){
			return objetivos;
		}
	}
}

