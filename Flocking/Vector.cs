using System;

namespace Nuevo
{
	public class Vector
	{
		public Vector(double a, double b)
		{
			X = a;
			Y = b;
		}

		double _x;
		public double X
		{
			get { return _x; }
			set { _x = value; }
		}

		double _y;
		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}
		public double magnitude()
		{
			return Math.Sqrt(_x * _x + _y * _y);
		}

		public void limit(double max)
		{
			if (magnitude() > max)
			{
				this.normalize();
				this.mult(max);
			}
		}

		public double heading(){
			double angle = Math.Atan2(-this._y, this._x);
			return angle;
		}
		public void add(Vector b)
		{
			this._x += b._x;
			this._y += b._y;
		}

		public void sub(double d)
		{
			this._x -= d;
			this._y -= d;
		}

		public Vector sub(Vector v1, Vector v2)
		{
			Vector v = new Vector(v1._x - v2._x, v1._y - v2._y);
			return v;
		}

		public void sub(Vector v2)
		{
			this._x -= v2.X;
			this._y -= v2.Y;

		}

		public void div(double d)
		{
			this._x /= d;
			this._y /= d;
		}
		public void mult(double d)
		{
			this._x *= d;
			this._y *= d;
		}

		public double escalar(Vector v1)
		{
			double total = this.X * v1.X + this.Y * v1.Y;
			return total;
		}

		public double distance(Vector v)
		{
			double dx = this._x - v.X;
			double dy = this._y - v.Y;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		public void normalize()
		{
			double divisor = this.magnitude();
			if (divisor > 0)
				this.div(divisor);
		}

		public double angulo_entre_vectores(Vector b){
			double scalar = escalar (b);
			double moduloA = magnitude ();
			double moduloB = b.magnitude ();
			double numero = scalar / (moduloA*moduloB);
			return Math.Acos (numero);
		}
	}

}

