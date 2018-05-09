using System;

namespace Nuevo
{
	public partial class DialogoParametros : Gtk.Dialog
	{
		public DialogoParametros ()
		{
			this.Build ();
			this.pesoAlineamiento.Value = Logica.coefAli;
			this.pesoSeparacion.Value = Logica.coefSep;
			this.pesoCohesion.Value = Logica.coefCoh;

			this.radioAlineamiento.Value = Logica.radioAli;
			this.radioSeparacion.Value = Logica.radioSep;
			this.radioCohesion.Value = Logica.radioCoh;

			this.spinMaxForce.Value = Logica.max_force;
			this.spinMaxSpeed.Value = Logica.max_speed;

			this.spinTamanio.Value = Logica.tamanio_boid;

			this.spinbuttonRadioObjetivos.Value = Constantes.radio_objetivos;
		}

		protected void OnButtonOkPressed (object sender, EventArgs e)
		{
			Logica.coefAli = this.pesoAlineamiento.Value;
			Logica.coefSep = this.pesoSeparacion.Value;
			Logica.coefCoh = this.pesoCohesion.Value;

			Logica.radioAli = this.radioAlineamiento.Value;
			Logica.radioSep = this.radioSeparacion.Value;
			Logica.radioCoh = this.radioCohesion.Value;

			Logica.max_force = this.spinMaxForce.Value;
			Logica.max_speed = this.spinMaxSpeed.Value;

			Logica.tamanio_boid = (int)this.spinTamanio.Value;

			Constantes.radio_objetivos = (int)this.spinbuttonRadioObjetivos.Value;

			this.Destroy ();
		}

		protected void OnButtonCancelPressed (object sender, EventArgs e)
		{

			this.Destroy ();
		}
			
	}
}

