using System;
using System.Drawing;

namespace Nuevo
{
	public partial class DialogoSimulacion : Gtk.Dialog
	{
		Rectangle lim;
		Window mainw;

		public DialogoSimulacion (Rectangle l,Window m)
		{
			lim = l;
			mainw = m;
			this.Build ();
			this.spinbutton1.Value = Logica.nro_boids;
			this.spinbuttonLapsos.Value = Constantes.lapsos;
			this.spinbuttonIteraciones.Value = Constantes.iteraciones_lapso;

		}

		protected void OnButtonOkPressed (object sender, EventArgs e)
		{

			Constantes.escenario = false;
			Constantes.simulacion = true;

			mainw.tb1.Sensitive = true;
			mainw.tb2.Sensitive = false;
			mainw.nivel.Sensitive = false;
			mainw.etiquetaNivel.Sensitive = false;
			mainw.intermitencia.Sensitive = false;
			mainw.etiquetaIntermitencia.Sensitive = false;
			mainw.calor.Active = false;
			mainw.trayectoria.Active = false;

			Logica.nro_boids = (int)this.spinbutton1.Value;
			Constantes.lapsos = (int)this.spinbuttonLapsos.Value;
			Constantes.iteraciones_lapso = (int)this.spinbuttonIteraciones.Value;
			Constantes.tiempos_boids.Clear ();
			Constantes.tiempo = 0; //Comienzo a contar las iteraciones de nuevo
			Constantes.iteraciones_total = 0;

			Constantes.resetEstadisticas ();

			Logica.crear_boids ();
			Logica.play = true;
			mainw.tb1.Sensitive = true;
			mainw.grilla.Active = false;
			mainw.calor.Active = false;
			mainw.estadistica.Sensitive = true;
			this.Destroy ();
		}

		protected void OnButtonCancelPressed (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

