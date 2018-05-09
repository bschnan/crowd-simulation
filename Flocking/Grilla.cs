using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nuevo
{
	public class Grilla
	{
		List<List<Casillero>> matrix = new List<List<Casillero>>();

		int inicioX, inicioY, avanceX, avanceY, sobranteX, sobranteY;

		public Grilla ()
		{
			inicioX = 0;
			inicioY = 0;
			avanceX = ((Constantes.limites.Width)/Constantes.nro_casillas);
			avanceY = ((Constantes.limites.Height)/Constantes.nro_casillas);
			sobranteX = (Constantes.limites.Width) % Constantes.nro_casillas;
			sobranteY = (Constantes.limites.Height) % Constantes.nro_casillas;
			Console.WriteLine ("avanceX: "+avanceX+"; avanceY: "+avanceY);
			init_grilla ();
		}

		private void init_grilla(){

			for(int x=0;x<Constantes.nro_casillas;x++){
				List<Casillero> row = new List<Casillero>();
				for (int y = 0; y < Constantes.nro_casillas; y++) {
					Casillero c = new Casillero ();
					c.visitas = 0;
					c.rectangulos = new List<Obstaculo>();
					c.circulos = new List<Obstaculo>();
					c.lineas = new List<Obstaculo>();
					c.boids = new BoidCollection ();
					c.inicioX = inicioX;
					c.inicioY = inicioY;
					c.finX = inicioX + avanceX;
					c.finY = inicioY + avanceY;
					inicioY += avanceY + 1;
					//Ultimo casillero de Y o X distinto de tamaño
					if (y == Constantes.nro_casillas -1)
						c.finY += sobranteY;
					if (x == Constantes.nro_casillas -1)
						c.finX += sobranteX;

					row.Add (c);
				}
				inicioY = 0;
				inicioX += avanceX + 1;
				matrix.Add (row);
			}
		}

		public Casillero get_casillero(int i, int j){
			return matrix [i] [j];
		}
			

		public void cargar_obstaculos(){
			limpiar_obstaculos ();
			Cairo.Rectangle n;
			foreach (Obstaculo r in Logica.rectangulos) {

				double X0=r.rectangle.X, Y0=Logica.mapeo(r.rectangle.Y), X1= r.rectangle.X+r.rectangle.Width, Y1=Logica.mapeo(r.rectangle.Y+r.rectangle.Height);
				double Xf, Yf;
				if (X0 < X1) {
					X0 -= 10;
					Xf = X1 + 20;
				} else {
					Xf = X0 + 20;
					X0 = X1 - 10;
				}
				if (Y0 < Y1) {
					Y0 -= 10;
					Yf = Y1 + 20;
				} else {
					Yf = Y0 + 20;
					Y0 = Y1 - 10;
				}

				n = new Cairo.Rectangle ((int)X0,(int)Y0,(int)Xf,(int)Yf);
				ubicar_rectangulo_casillero (n, r, 'r');

			}

			foreach (Obstaculo c in Logica.circulos) {
				double X0 = c.rectangle.X - c.rectangle.Width - 10;
				double Y0 = Logica.mapeo(c.rectangle.Y) - c.rectangle.Width -10;
				double X1 = c.rectangle.X + c.rectangle.Width +10;
				double Y1 = Logica.mapeo(c.rectangle.Y) + c.rectangle.Width + 10;;
				n = new Cairo.Rectangle (X0, Y0, X1, Y1);
				ubicar_rectangulo_casillero (n, c, 'c');
			}
			

			foreach (Obstaculo l in Logica.lineas) {
				int lineainiciox, lineafinx, lineainicioy, lineafiny;
				if (l.rectangle.X < l.rectangle.Width) {
					lineainiciox = (int)l.rectangle.X -10;
					lineafinx = (int)l.rectangle.Width +10;
				} else {
					lineainiciox = (int)l.rectangle.Width -10;
					lineafinx = (int)l.rectangle.X + 10;
				}
				if (Logica.mapeo(l.rectangle.Y) < Logica.mapeo(l.rectangle.Height)) {
					lineainicioy = (int)Logica.mapeo(l.rectangle.Y)-10;
					lineafiny = (int)Logica.mapeo(l.rectangle.Height)+10;
				} else {
					lineainicioy = (int)Logica.mapeo(l.rectangle.Height) - 10;
					lineafiny = (int)Logica.mapeo(l.rectangle.Y) + 10;
				}


				n = new Cairo.Rectangle (lineainiciox, lineainicioy, lineafinx, lineafiny);
				ubicar_rectangulo_casillero (n, l, 'l');
			}
				
		}

		public void limpiar_obstaculos(){
			for (int y = 0; y < Constantes.nro_casillas; y++)
				for (int x = 0; x < Constantes.nro_casillas; x++) {
					matrix [x] [y].circulos.Clear ();
					matrix [x] [y].rectangulos.Clear ();
					matrix [x] [y].lineas.Clear ();
				}
		}

		public void limpiar_boids(){
			for (int y = 0; y < Constantes.nro_casillas; y++)
				for (int x = 0; x < Constantes.nro_casillas; x++) {
					matrix [x] [y].boids.Clear ();

				}
		}

		public void ubicar_boid_casillero(Boid b){
			int radio = (int)Constantes.max (Logica.radioAli, Logica.radioCoh, Logica.radioSep);
			int xo = (int)b.Location.X - radio, yo = (int)b.Location.Y - radio, x1 = (int)b.Location.X + radio, y1 = (int)b.Location.Y + radio;
			Point inicio = ubicar_punto_casillero (xo,yo);
			Point fin = ubicar_punto_casillero (x1,y1);
			for (int x = inicio.X; x <= fin.X; x++)
				for (int y = inicio.Y; y <= fin.Y; y++) {
					matrix [x] [y].boids.Add (b);
					matrix [x] [y].visitas++;
					if (matrix [x] [y].visitas > 2000000000)
						resetVisitas ();
				}

		}

		public void ubicar_rectangulo_casillero(Cairo.Rectangle r, Obstaculo original, char tipo){
			Point inicio = ubicar_punto_casillero ((int)r.X,(int)r.Y);
			Point fin = ubicar_punto_casillero ((int)r.Width,(int)r.Height);
			for (int x = inicio.X; x <= fin.X; x++)
				for (int y = inicio.Y; y <= fin.Y; y++) {
					if (tipo == 'r')
						matrix [x] [y].rectangulos.Add (original);
					if (tipo == 'c')
						matrix [x] [y].circulos.Add (original);
					if (tipo == 'l')
						matrix [x] [y].lineas.Add (original);
				}
		}

		public Rectangle ubicacion_boid(Boid b){
			int radio = (int)Constantes.max (Logica.radioAli, Logica.radioCoh, Logica.radioSep);
			int xo = (int)b.Location.X - radio, yo = (int)b.Location.Y - radio, x1 = (int)b.Location.X + radio, y1 = (int)b.Location.Y + radio;
			Point inicio = ubicar_punto_casillero (xo,yo);
			Point fin = ubicar_punto_casillero (x1,y1);
			return new Rectangle (inicio.X, inicio.Y, fin.X, fin.Y);
		}

		private Point ubicar_punto_casillero(int posX, int posY){

			int pX = (int)(posX / avanceX);
			int pY = (int)(posY / avanceY);

			if (pX > Constantes.nro_casillas - 1)
				pX = Constantes.nro_casillas - 1;
			if (pY > Constantes.nro_casillas - 1)
				pY = Constantes.nro_casillas - 1;
			if (pX < 0)
				pX = 0;
			if (pY < 0)
				pY = 0;

			return new Point (pX, pY);

		}

		public void reclasificar(BoidCollection coleccion){
			limpiar_boids ();
			int n = 0;
			foreach (Boid b in coleccion) {
				if(n!=0)
					ubicar_boid_casillero (b);
				n++;
			}

		}

		public void resetVisitas(){
			for (int x = 0; x < Constantes.nro_casillas; x++)
				for (int y = 0; y < Constantes.nro_casillas; y++)
					matrix [x] [y].visitas = 0;
		}

		public int max_visitas(){
			int n = 0;
			for (int x = 0; x < Constantes.nro_casillas; x++)
				for (int y = 0; y < Constantes.nro_casillas; y++)
					if (matrix [x] [y].visitas > n)
						n = matrix [x] [y].visitas;
			return n;
		}

		public int min_visitas(){
			int n = 0;
			for (int x = 0; x < Constantes.nro_casillas; x++)
				for (int y = 0; y < Constantes.nro_casillas; y++)
					if (matrix [x] [y].visitas < n)
						n = matrix [x] [y].visitas;
			return n;
		}
	}
}

