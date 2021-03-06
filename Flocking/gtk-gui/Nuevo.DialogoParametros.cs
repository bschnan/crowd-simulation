
// This file has been generated by the GUI designer. Do not modify.
namespace Nuevo
{
	public partial class DialogoParametros
	{
		private global::Gtk.VBox vbox6;
		
		private global::Gtk.HBox hbox8;
		
		private global::Gtk.Label label9;
		
		private global::Gtk.Label label10;
		
		private global::Gtk.Label label11;
		
		private global::Gtk.HBox hbox2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.SpinButton pesoCohesion;
		
		private global::Gtk.SpinButton radioCohesion;
		
		private global::Gtk.HBox hbox3;
		
		private global::Gtk.Label label4;
		
		private global::Gtk.SpinButton pesoAlineamiento;
		
		private global::Gtk.SpinButton radioAlineamiento;
		
		private global::Gtk.VBox vbox7;
		
		private global::Gtk.HBox hbox4;
		
		private global::Gtk.Label label5;
		
		private global::Gtk.SpinButton pesoSeparacion;
		
		private global::Gtk.SpinButton radioSeparacion;
		
		private global::Gtk.HBox hbox6;
		
		private global::Gtk.VBox vbox8;
		
		private global::Gtk.Label label6;
		
		private global::Gtk.SpinButton spinMaxForce;
		
		private global::Gtk.VBox vbox9;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.SpinButton spinMaxSpeed;
		
		private global::Gtk.VBox vbox10;
		
		private global::Gtk.Label label8;
		
		private global::Gtk.SpinButton spinTamanio;
		
		private global::Gtk.VBox vbox2;
		
		private global::Gtk.Label label12;
		
		private global::Gtk.SpinButton spinbuttonRadioObjetivos;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Nuevo.DialogoParametros
			this.CanFocus = true;
			this.Name = "Nuevo.DialogoParametros";
			this.Title = global::Mono.Unix.Catalog.GetString ("Parámetros");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Resizable = false;
			// Internal child Nuevo.DialogoParametros.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox ();
			this.hbox8.Name = "hbox8";
			this.hbox8.Homogeneous = true;
			// Container child hbox8.Gtk.Box+BoxChild
			this.label9 = new global::Gtk.Label ();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString (" ");
			this.hbox8.Add (this.label9);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox8 [this.label9]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.label10 = new global::Gtk.Label ();
			this.label10.Name = "label10";
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString ("Peso");
			this.hbox8.Add (this.label10);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox8 [this.label10]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString ("Radio");
			this.hbox8.Add (this.label11);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox8 [this.label11]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox6.Add (this.hbox8);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hbox8]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			w5.Padding = ((uint)(15));
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Homogeneous = true;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Cohesion");
			this.hbox2.Add (this.label3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label3]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.pesoCohesion = new global::Gtk.SpinButton (0D, 1D, 0.01D);
			this.pesoCohesion.CanFocus = true;
			this.pesoCohesion.Name = "pesoCohesion";
			this.pesoCohesion.Adjustment.PageIncrement = 10D;
			this.pesoCohesion.ClimbRate = 1D;
			this.pesoCohesion.Numeric = true;
			this.hbox2.Add (this.pesoCohesion);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.pesoCohesion]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.radioCohesion = new global::Gtk.SpinButton (0D, 200D, 1D);
			this.radioCohesion.CanFocus = true;
			this.radioCohesion.Name = "radioCohesion";
			this.radioCohesion.Adjustment.PageIncrement = 10D;
			this.radioCohesion.ClimbRate = 1D;
			this.radioCohesion.Numeric = true;
			this.hbox2.Add (this.radioCohesion);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.radioCohesion]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox6.Add (this.hbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hbox2]));
			w9.Position = 1;
			w9.Fill = false;
			w9.Padding = ((uint)(5));
			// Container child vbox6.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Homogeneous = true;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Alineamiento");
			this.hbox3.Add (this.label4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label4]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.pesoAlineamiento = new global::Gtk.SpinButton (0D, 1D, 0.01D);
			this.pesoAlineamiento.CanFocus = true;
			this.pesoAlineamiento.Name = "pesoAlineamiento";
			this.pesoAlineamiento.Adjustment.PageIncrement = 10D;
			this.pesoAlineamiento.ClimbRate = 1D;
			this.pesoAlineamiento.Numeric = true;
			this.hbox3.Add (this.pesoAlineamiento);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.pesoAlineamiento]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.radioAlineamiento = new global::Gtk.SpinButton (0D, 200D, 1D);
			this.radioAlineamiento.CanFocus = true;
			this.radioAlineamiento.Name = "radioAlineamiento";
			this.radioAlineamiento.Adjustment.PageIncrement = 10D;
			this.radioAlineamiento.ClimbRate = 1D;
			this.radioAlineamiento.Numeric = true;
			this.hbox3.Add (this.radioAlineamiento);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.radioAlineamiento]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			this.vbox6.Add (this.hbox3);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.hbox3]));
			w13.Position = 2;
			w13.Expand = false;
			w13.Fill = false;
			w13.Padding = ((uint)(5));
			// Container child vbox6.Gtk.Box+BoxChild
			this.vbox7 = new global::Gtk.VBox ();
			this.vbox7.Name = "vbox7";
			this.vbox7.Spacing = 6;
			// Container child vbox7.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Homogeneous = true;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Separacion");
			this.hbox4.Add (this.label5);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label5]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.pesoSeparacion = new global::Gtk.SpinButton (0D, 1D, 0.01D);
			this.pesoSeparacion.CanFocus = true;
			this.pesoSeparacion.Name = "pesoSeparacion";
			this.pesoSeparacion.Adjustment.PageIncrement = 10D;
			this.pesoSeparacion.ClimbRate = 1D;
			this.pesoSeparacion.Numeric = true;
			this.hbox4.Add (this.pesoSeparacion);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.pesoSeparacion]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.radioSeparacion = new global::Gtk.SpinButton (0D, 200D, 1D);
			this.radioSeparacion.CanFocus = true;
			this.radioSeparacion.Name = "radioSeparacion";
			this.radioSeparacion.Adjustment.PageIncrement = 10D;
			this.radioSeparacion.ClimbRate = 1D;
			this.radioSeparacion.Numeric = true;
			this.hbox4.Add (this.radioSeparacion);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.radioSeparacion]));
			w16.Position = 2;
			w16.Expand = false;
			w16.Fill = false;
			this.vbox7.Add (this.hbox4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox7 [this.hbox4]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			w17.Padding = ((uint)(5));
			// Container child vbox7.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Homogeneous = true;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox8 = new global::Gtk.VBox ();
			this.vbox8.Name = "vbox8";
			this.vbox8.Spacing = 6;
			// Container child vbox8.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label ();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Max Force");
			this.vbox8.Add (this.label6);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox8 [this.label6]));
			w18.Position = 0;
			w18.Fill = false;
			// Container child vbox8.Gtk.Box+BoxChild
			this.spinMaxForce = new global::Gtk.SpinButton (0D, 5D, 0.01D);
			this.spinMaxForce.CanFocus = true;
			this.spinMaxForce.Name = "spinMaxForce";
			this.spinMaxForce.Adjustment.PageIncrement = 10D;
			this.spinMaxForce.ClimbRate = 1D;
			this.spinMaxForce.Numeric = true;
			this.vbox8.Add (this.spinMaxForce);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox8 [this.spinMaxForce]));
			w19.Position = 1;
			w19.Expand = false;
			w19.Fill = false;
			this.hbox6.Add (this.vbox8);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox8]));
			w20.Position = 0;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox9 = new global::Gtk.VBox ();
			this.vbox9.Name = "vbox9";
			this.vbox9.Spacing = 6;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Max Speed");
			this.vbox9.Add (this.label7);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox9 [this.label7]));
			w21.Position = 0;
			w21.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.spinMaxSpeed = new global::Gtk.SpinButton (0D, 10D, 1D);
			this.spinMaxSpeed.CanFocus = true;
			this.spinMaxSpeed.Name = "spinMaxSpeed";
			this.spinMaxSpeed.Adjustment.PageIncrement = 10D;
			this.spinMaxSpeed.ClimbRate = 1D;
			this.spinMaxSpeed.Numeric = true;
			this.vbox9.Add (this.spinMaxSpeed);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox9 [this.spinMaxSpeed]));
			w22.Position = 1;
			w22.Expand = false;
			w22.Fill = false;
			this.hbox6.Add (this.vbox9);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox9]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox10 = new global::Gtk.VBox ();
			this.vbox10.Name = "vbox10";
			this.vbox10.Spacing = 6;
			// Container child vbox10.Gtk.Box+BoxChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Tamaño del boid");
			this.vbox10.Add (this.label8);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox10 [this.label8]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child vbox10.Gtk.Box+BoxChild
			this.spinTamanio = new global::Gtk.SpinButton (0D, 15D, 0.1D);
			this.spinTamanio.CanFocus = true;
			this.spinTamanio.Name = "spinTamanio";
			this.spinTamanio.Adjustment.PageIncrement = 10D;
			this.spinTamanio.ClimbRate = 1D;
			this.spinTamanio.Numeric = true;
			this.vbox10.Add (this.spinTamanio);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.vbox10 [this.spinTamanio]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			this.hbox6.Add (this.vbox10);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox10]));
			w26.Position = 2;
			w26.Expand = false;
			w26.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label12 = new global::Gtk.Label ();
			this.label12.Name = "label12";
			this.label12.LabelProp = global::Mono.Unix.Catalog.GetString ("Radio de los objetivos");
			this.vbox2.Add (this.label12);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label12]));
			w27.Position = 0;
			w27.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.spinbuttonRadioObjetivos = new global::Gtk.SpinButton (5D, 100D, 1D);
			this.spinbuttonRadioObjetivos.CanFocus = true;
			this.spinbuttonRadioObjetivos.Name = "spinbuttonRadioObjetivos";
			this.spinbuttonRadioObjetivos.Adjustment.PageIncrement = 10D;
			this.spinbuttonRadioObjetivos.ClimbRate = 1D;
			this.spinbuttonRadioObjetivos.Numeric = true;
			this.spinbuttonRadioObjetivos.Value = 5D;
			this.vbox2.Add (this.spinbuttonRadioObjetivos);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.spinbuttonRadioObjetivos]));
			w28.Position = 1;
			w28.Expand = false;
			w28.Fill = false;
			this.hbox6.Add (this.vbox2);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox2]));
			w29.PackType = ((global::Gtk.PackType)(1));
			w29.Position = 3;
			w29.Expand = false;
			w29.Fill = false;
			this.vbox7.Add (this.hbox6);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vbox7 [this.hbox6]));
			w30.Position = 1;
			w30.Expand = false;
			w30.Fill = false;
			w30.Padding = ((uint)(40));
			this.vbox6.Add (this.vbox7);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.vbox7]));
			w31.Position = 3;
			w31.Expand = false;
			w31.Fill = false;
			w1.Add (this.vbox6);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(w1 [this.vbox6]));
			w32.Position = 0;
			w32.Expand = false;
			w32.Fill = false;
			// Internal child Nuevo.DialogoParametros.ActionArea
			global::Gtk.HButtonBox w33 = this.ActionArea;
			w33.Name = "dialog1_ActionArea";
			w33.Spacing = 10;
			w33.BorderWidth = ((uint)(5));
			w33.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Cancelar");
			this.AddActionWidget (this.buttonCancel, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w34 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w33 [this.buttonCancel]));
			w34.Expand = false;
			w34.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = global::Mono.Unix.Catalog.GetString ("Aceptar");
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w35 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w33 [this.buttonOk]));
			w35.Position = 1;
			w35.Expand = false;
			w35.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 489;
			this.DefaultHeight = 504;
			this.Show ();
			this.buttonCancel.Pressed += new global::System.EventHandler (this.OnButtonCancelPressed);
			this.buttonOk.Pressed += new global::System.EventHandler (this.OnButtonOkPressed);
		}
	}
}
