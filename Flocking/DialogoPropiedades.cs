using System;

namespace Nuevo
{
	public partial class DialogoPropiedades : Gtk.Dialog
	{

		public DialogoPropiedades ()
		{
			this.Build ();
			this.radiobutton1.Active = Logica.atravesar;
			this.radiobutton2.Active = Logica.rebotar;
			this.checkbutton1.Active = Logica.mouse;
			this.proximoObjetivo.Active = Constantes.objetivoCercano;
			this.objetivoLibre.Active = Constantes.objetivoLibre;
			this.criterioAzar.Active = Constantes.objetivoAzar;
			this.simulacionContinuaButton.Active = Constantes.simulacionContinua;

		}

		protected void OnButtonOkPressed (object sender, EventArgs e)
		{
			Logica.atravesar = this.radiobutton1.Active;
			Logica.rebotar = this.radiobutton2.Active;
			Logica.mouse = this.checkbutton1.Active;
			Constantes.objetivoCercano = this.proximoObjetivo.Active;
			Constantes.objetivoLibre = this.objetivoLibre.Active;
			Constantes.objetivoAzar = this.criterioAzar.Active;
			Constantes.simulacionContinua = this.simulacionContinuaButton.Active;


			this.Destroy ();
		}

		protected void OnButtonCancelPressed (object sender, EventArgs e)
		{

			this.Destroy ();
		}

		protected void OnDeleteEvent (object sender, EventArgs a)
		{

		}

		protected void OnClose (object sender, EventArgs e){


		}
	}
}

