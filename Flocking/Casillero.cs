using System;
using System.Collections.Generic;
using System.Drawing;

namespace Nuevo
{
	public class Casillero
	{	
		int _inicioX,_inicioY, _finX, _finY;

		public int inicioX {
			get { return _inicioX; }
			set { _inicioX = value; }
		}

		public int inicioY {
			get { return _inicioY; }
			set { _inicioY = value; }
		}

		public int finX {
			get { return _finX; }
			set { _finX = value; }
		}

		public int finY {
			get { return _finY; }
			set { _finY = value; }
		}

		public List<Obstaculo> _rectangulos;
		public List<Obstaculo> rectangulos{
			get { return _rectangulos; }
			set { _rectangulos = value; }
		}

		public List<Obstaculo> _circulos;
		public List<Obstaculo> circulos{
			get { return _circulos; }
			set { _circulos = value; }
		}

		public List<Obstaculo> _lineas;
		public List<Obstaculo> lineas{
			get { return _lineas; }
			set { _lineas = value; }
		}

		public BoidCollection _boids;
		public BoidCollection boids{
			get { return _boids; }
			set { _boids = value; }
		}

		public int _visitas;
		public int visitas{
			get { return _visitas;}
			set { _visitas = value;}
		}


		public Casillero ()
		{
		}
			
	}
}
