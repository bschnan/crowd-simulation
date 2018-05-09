using System;

namespace Nuevo
{
	public class Obstaculo
	{
        public IntPtr ogreRef;
		public int intermitencia;
		public Cairo.Rectangle rectangle;
		public bool enable;

		public Obstaculo (double x, double y, double width, double height, int i)
		{
            ogreRef = IntPtr.Zero;
			intermitencia = i;
			rectangle = new Cairo.Rectangle (x, y, width, height);
			enable = true;
		}
	}
}

