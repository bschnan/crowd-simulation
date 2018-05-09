using System;

namespace Nuevo
{
	public partial class DialogoPuntoObjetivo : Gtk.Dialog
	{
		Window mainw;

		public DialogoPuntoObjetivo (Window m)
		{
			this.Build ();
			mainw = m;
			this.spinbutton3.SetRange (1,Constantes.max_nivel);
			this.spinbutton3.Value = Constantes.max_nivel;
		}

		protected void OnButtonOkPressed (object sender, EventArgs e)
		{
			Constantes.nivel_actual = (int)this.spinbutton3.Value;
			if (Constantes.max_nivel == this.spinbutton3.Value)
				Constantes.max_nivel++;
			Constantes.punto_objetivo_action = true;
			this.Destroy ();
		}

		protected void OnButtonCancelPressed (object sender, EventArgs e)
		{
			Constantes.punto_objetivo_action = false;
			this.Destroy ();
		}
	}
}

