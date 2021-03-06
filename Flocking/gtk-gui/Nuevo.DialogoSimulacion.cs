
// This file has been generated by the GUI designer. Do not modify.
namespace Nuevo
{
	public partial class DialogoSimulacion
	{
		private global::Gtk.HBox hbox1;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.SpinButton spinbutton1;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.SpinButton spinbuttonLapsos;
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.SpinButton spinbuttonIteraciones;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Nuevo.DialogoSimulacion
			this.Name = "Nuevo.DialogoSimulacion";
			this.Title = global::Mono.Unix.Catalog.GetString ("Nueva simulación");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(1));
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Resizable = false;
			// Internal child Nuevo.DialogoSimulacion.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Homogeneous = true;
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Numero de boids");
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w2.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.spinbutton1 = new global::Gtk.SpinButton (1D, 1000D, 1D);
			this.spinbutton1.CanFocus = true;
			this.spinbutton1.Name = "spinbutton1";
			this.spinbutton1.Adjustment.PageIncrement = 10D;
			this.spinbutton1.ClimbRate = 1D;
			this.spinbutton1.Numeric = true;
			this.hbox1.Add (this.spinbutton1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.spinbutton1]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			w1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Homogeneous = true;
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Cantidad de lapsos");
			this.hbox2.Add (this.label2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label2]));
			w5.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.spinbuttonLapsos = new global::Gtk.SpinButton (1D, 50000D, 1D);
			this.spinbuttonLapsos.CanFocus = true;
			this.spinbuttonLapsos.Name = "spinbuttonLapsos";
			this.spinbuttonLapsos.Adjustment.PageIncrement = 10D;
			this.spinbuttonLapsos.ClimbRate = 1D;
			this.spinbuttonLapsos.Numeric = true;
			this.spinbuttonLapsos.Value = 1D;
			this.hbox2.Add (this.spinbuttonLapsos);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.spinbuttonLapsos]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			w1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Homogeneous = true;
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Iteraciones entre lapsos");
			this.hbox3.Add (this.label3);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label3]));
			w8.Position = 0;
			// Container child hbox3.Gtk.Box+BoxChild
			this.spinbuttonIteraciones = new global::Gtk.SpinButton (0D, 200D, 1D);
			this.spinbuttonIteraciones.CanFocus = true;
			this.spinbuttonIteraciones.Name = "spinbuttonIteraciones";
			this.spinbuttonIteraciones.Adjustment.PageIncrement = 10D;
			this.spinbuttonIteraciones.ClimbRate = 1D;
			this.spinbuttonIteraciones.Numeric = true;
			this.hbox3.Add (this.spinbuttonIteraciones);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.spinbuttonIteraciones]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			w1.Add (this.hbox3);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox3]));
			w10.Position = 2;
			w10.Expand = false;
			w10.Fill = false;
			// Internal child Nuevo.DialogoSimulacion.ActionArea
			global::Gtk.HButtonBox w11 = this.ActionArea;
			w11.Name = "dialog1_ActionArea";
			w11.Spacing = 10;
			w11.BorderWidth = ((uint)(5));
			w11.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Cancelar");
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w12 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w11 [this.buttonCancel]));
			w12.Expand = false;
			w12.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = global::Mono.Unix.Catalog.GetString ("Aceptar");
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w13 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w11 [this.buttonOk]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 256;
			this.DefaultHeight = 198;
			this.Show ();
			this.buttonCancel.Pressed += new global::System.EventHandler (this.OnButtonCancelPressed);
			this.buttonOk.Pressed += new global::System.EventHandler (this.OnButtonOkPressed);
		}
	}
}
