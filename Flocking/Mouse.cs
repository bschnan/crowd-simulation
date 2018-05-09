using System;
using Gtk;

namespace Nuevo
{
	public class Mouse : Boid
	{
		int x,y;

		public Mouse()
		{

		}

		public override void calculateVelocity(BoidCollection s,DrawingArea area)
		{	
			area.GetPointer (out x,out y);
			y = (int)Logica.mapeo (y);
			Velocity.X = x-Location.X;
			Velocity.Y = y-Location.Y;
			LocationNueva.X = x;
			LocationNueva.Y = y;
		}
			
	}
}

