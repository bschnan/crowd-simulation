using System;

namespace Nuevo
{
	public partial class DialogoMetricas : Gtk.Dialog
	{
		public DialogoMetricas ()
		{
			this.Build ();
			if (!Constantes.sin_movimiento_estadistica ()) {
				this.max_tiempo.Text = Constantes.max_tiempo.ToString ()+" it x 60 ms";
				this.min_tiempo.Text = Constantes.min_tiempo.ToString ()+" it x 60 ms";
				this.tiempo_prom.Text = Math.Round((Constantes.tiempo_acum / Constantes.nro_trayectorias),2).ToString ()+" it x "+ Constantes.milisegundos_iteracion+" ms";
				this.labelMiliseg.Text = "(it: iteración)";
			} else {
				this.max_tiempo.Text = "No existen registros";
				this.min_tiempo.Text = "No existen registros";
				this.tiempo_prom.Text = "No existen registros";
				this.labelMiliseg.Text = "";
			}

			this.min_vecinos.Text = Logica.calcular_vecinos_estadistica ().X.ToString ();
			this.max_vecinos.Text = Logica.calcular_vecinos_estadistica ().Y.ToString ();

		}

		protected void OnButton794Pressed (object sender, EventArgs e)
		{
			this.Destroy ();
		}
	}
}

