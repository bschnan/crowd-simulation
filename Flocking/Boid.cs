using System;
using System.Drawing;
using System.Collections.Generic;
using Gtk;


namespace Nuevo
{ 
	public abstract class Boid
	{
        //OGRE PROPERTIES------------
        public IntPtr ogreRef;
        //-----------------------------


        Double _rotacion;
        public Double rotacion
        {
            get { return _rotacion; }
            set { _rotacion = value; }
        }

		Vector _location;
		public Vector Location
		{
			get { return _location; }
			set { _location = value; }
		}
		Vector _locationNueva;

		public Vector LocationNueva
		{
			get { return _locationNueva; }
			set { _locationNueva = value; }
		}

		Vector _velocity;
		public Vector Velocity
		{
			get { return _velocity; }
			set { _velocity = value; }
		}

		Vector _acceleration;
		public Vector Acceleration
		{
			get { return _acceleration; }
			set { _acceleration = value; }
		}

		public int _vecinos;
		public int vecinos{
			get{ return _vecinos; }
			set{ _vecinos = value; }
		}


		int _objetivo;
		public int objetivo
		{
			get { return _objetivo; }
			set { _objetivo = value; }
		}

		Point _mousePosition;
		public Point MousePosition
		{
			get { return _mousePosition; }
			set { _mousePosition = value; }
		}

		bool _llego;
		public bool llego
		{
			get { return _llego; }
			set { _llego = value; }
		}

		int _criterio;
		public int criterio{
			get { return _criterio; }
			set { _criterio = value; }
		}


		public double calculateRotation()
		{

			double escalar = Velocity.Y;
			double modulo = Velocity.magnitude();
			double valor = 1;
			if (modulo!=0)
				valor = escalar / modulo;
			double radianes = Math.Acos(valor);
			//double grados = (radianes * 180) / Math.PI;
			if(Velocity.X<0)
				return -radianes;
			return radianes;

		}

        public double calculateRotationOgre()
        {
            Vector v = new Vector(-this.Velocity.X, -this.Velocity.Y);
            v.normalize();

            double radianes = Math.Atan2(v.Y, v.X);

            //if (radianes < 0)
            //    radianes += 2 * Math.PI;
            double grados = (radianes * 180) / Math.PI;
            //return grados + 270;
            return grados;
        }

		public virtual void calculateVelocity(BoidCollection sc,DrawingArea area)
		{

		}

		public virtual void calculateVelocity(BoidCollection boids, List<Obstaculo> c, List<Obstaculo> r, List<Obstaculo> l)
		{

		}



//		protected void SetupTransform(Graphics g)
//		{
//			_state = g.Save();//guarda el estado de las matrices de transf y rotacion para despues de dibujar el objeto quede todo como estaba (vacio)
//			Matrix mx = new Matrix();
//			double grados=calculateRotation();
//			mx.Rotate((float)(grados), MatrixOrder.Append); //en grados
//			mx.Translate((float)(this.Location.X), (float)(Form1.mapeo(this.Location.Y)), MatrixOrder.Append);
//			g.Transform = mx;
//		}
//
//		protected void RestoreTransform(Graphics g)
//		{
//			g.Restore(_state); //vuelve todas las matrices como estaban para poder dibujar el proximo objeto
//		}
//
//		public void Draw(Graphics g)
//		{
//			SetupTransform(g);
//			RenderObject(g);
//			RestoreTransform(g);
//		}
//
//		public virtual void RenderObject(Graphics g)
//		{
//		}

	}
}

