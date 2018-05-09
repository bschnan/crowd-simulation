using System;
using System.IO;

namespace Nuevo
{
	public partial class DialogoGrafico : Gtk.Dialog
	{
		public DialogoGrafico ()
		{
			this.Build ();
			if (Constantes.tiempos_boids.Count > 0) {
				BarChart b = new BarChart ();
				this.labelGrafica.Text = "";
				b.load ();
				image1.Pixbuf = new Gdk.Pixbuf ("..\\chart.png");
				this.Maximize ();
			} else
				this.labelGrafica.Text = "No hay datos para mostrar";
			SaveAlineacionAsCSV ("direccion.csv");
			SaveColorMapAsCSV ("colormap.csv");
			SaveLocationAsCSV ("Location.csv");
		}


		protected void OnButtonOkPressed (object sender, EventArgs e)
		{
			if (Constantes.tiempos_boids.Count > 0) {
				File.Delete ("..\\chart.png");
			}
			this.Destroy ();
		}

		public void SaveColorMapAsCSV(string fileName)
		{
			using (StreamWriter file = new StreamWriter(fileName))
			{
				for (int i = 0; i < Constantes.nro_casillas; i++){
					for (int j = 0; j < Constantes.nro_casillas; j++) {
						file.Write (Logica.grilla.get_casillero (j, i).visitas+";");
					}
					file.Write (Environment.NewLine);

				}



			}
		}

		public void SaveAlineacionAsCSV(string fileName)
		{
			Vector n = new Vector (0,0);
			using (StreamWriter file = new StreamWriter(fileName))
			{
				foreach (Boid b in Logica.objects) {
					n.X = b.Velocity.X;
					n.Y = b.Velocity.Y;
					n.normalize ();
					file.WriteLine (n.X + ";" + n.Y);
				}

			}
		}

		public void SaveLocationAsCSV(string fileName)
		{

			using (StreamWriter file = new StreamWriter(fileName))
			{
				foreach (Boid b in Logica.objects) {
					file.WriteLine (b.Location.X + ";" + b.Location.Y);
				}

			}
		}
			
	}
}

