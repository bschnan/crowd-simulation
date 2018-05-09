using System;
using Gtk;
using System.Drawing;
using System.IO;


namespace Nuevo
{
	public partial class Window : Gtk.Window
	{

		//public System.Drawing.Rectangle limites = new Rectangle ();

		int loop=0;



		public Window () :	base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		public Gtk.ScrolledWindow scrollw
		{
			get { return this.scrolledwindow1; }
			set { this.scrolledwindow1 = value; }
		}

		public Gtk.MenuBar menub
		{
			get { return this.menubar1; }
			set { this.menubar1 = value; }
		}

		public Gtk.Action estadistica {
			get { return this.EstadsticaAction; }
			set { this.EstadsticaAction = value; }
		}

		public Gtk.Action grafica {
			get { return this.GrficaAction; }
			set { this.GrficaAction = value; }
		}

		public Gtk.Toolbar tb1
		{
			get { return this.toolbar1; }
			set { this.toolbar1 = value; }
		}

		public Gtk.Toolbar tb2
		{
			get { return this.toolbar2; }
			set { this.toolbar2 = value; }
		}

		public Gtk.ToggleAction contorno
		{
			get { return this.contornoButton; }
			set { this.contornoButton = value; }
		}

		public Gtk.ToggleAction calor
		{
			get { return this.calorButton; }
			set { this.calorButton = value; }
		}

		public Gtk.ToggleAction grilla
		{
			get { return this.grillaButton; }
			set { this.grillaButton = value; }
		}

		public Gtk.ToggleAction trayectoria
		{
			get { return this.trayectoriasAction; }
			set { this.trayectoriasAction = value; }
		}

		public Gtk.RadioAction circulo
		{
			get { return this.circuloButton; }
			set { this.circuloButton = value; }
		}

		public Gtk.RadioAction linea
		{
			get { return this.lineaButton; }
			set { this.lineaButton = value; }
		}

		public Gtk.RadioAction rectangulo
		{
			get { return this.rectanguloButton; }
			set { this.rectanguloButton = value; }
		}

		public Gtk.RadioAction puntoInicio
		{
			get { return this.puntoInicioAction; }
			set { this.puntoInicioAction = value; }
		}

		public Gtk.RadioAction puntoObjetivo
		{
			get { return this.puntoObjetivoAction; }
			set { this.puntoObjetivoAction = value; }
		}

		public Gtk.SpinButton nivel
		{
			get { return this.spinbuttonNivel; }
			set { this.spinbuttonNivel = value; }
		}

		public Gtk.SpinButton intermitencia
		{
			get { return this.spinbuttonIntermitencia; }
			set { this.spinbuttonIntermitencia = value; }
		}

		public Gtk.Label etiquetaNivel
		{
			get { return this.labelNivel; }
			set { this.labelNivel = value; }
		}

		public Gtk.Label etiquetaIntermitencia
		{
			get { return this.labelIntermitencia; }
			set { this.labelIntermitencia = value; }
		}

		public Gtk.Label etiquetaIteraciones
		{
			get { return this.labelIteraciones; }
			set { this.labelIteraciones = value; }
		}

		public Gtk.Label render
		{
			get { return this.labelRender; }
			set { this.labelRender = value; }
		}

		public Gtk.Label algorithm
		{
			get { return this.labelAlgorithm; }
			set { this.labelAlgorithm = value; }
		}

		protected void OnNuevaSimulacinActionActivated (object sender, EventArgs e)
		{
			DialogoSimulacion s = new DialogoSimulacion (Constantes.limites,this);
			s.Show ();
		}


		protected void OnPropiedadesActionActivated (object sender, EventArgs e)
		{
			DialogoPropiedades v = new DialogoPropiedades ();
			v.Show ();
		}


		protected void OnParmetrosActionActivated (object sender, EventArgs e)
		{
			DialogoParametros p = new DialogoParametros ();
			p.Show ();
		}


		protected void OnMtricasActionActivated (object sender, EventArgs e)
		{	
			DialogoMetricas d = new DialogoMetricas ();
			d.Show ();
		}


		protected void OnGrficaActionActivated (object sender, EventArgs e)
		{
			DialogoGrafico g = new DialogoGrafico ();
			g.Show ();
		}

		protected void OnPausaButtonActivated (object sender, EventArgs e)
		{
			Logica.play = false;
		}


		protected void OnPlayButtonActivated (object sender, EventArgs e)
		{
			Logica.play = true;
		}


		protected void OnResetButtonActivated (object sender, EventArgs e)
		{
			Logica.reset();
		}


		protected void OnZoomOutButtonActivated (object sender, EventArgs e)
		{
			
				Constantes.sx -= 0.1;
				Constantes.sy -= 0.1;
				loop--;
			
		}


		protected void OnZoomInButtonActivated (object sender, EventArgs e)
		{
			Constantes.sx += 0.1;
			Constantes.sy += 0.1;
			loop++;
		}

		protected void zoom_reset(){
			Constantes.sx = 1;
			Constantes.sy = 1;
		}


		protected void OnNuevoEscenarioActionActivated (object sender, EventArgs e)
		{
			limpiarPantalla ();
			Constantes.escenarioCargado = false;
//			Constantes.escenarioSurface.Dispose ();
			activarBotonesNuevoEscenario ();
		}

		protected void OnGuardarEscenarioActionActivated (object sender, EventArgs e)
		{

			FileChooserDialog Fcd = new FileChooserDialog ("Guardar escenario", this, FileChooserAction.Save,"Guardar",ResponseType.Accept,"Cancelar",ResponseType.Cancel);

			Fcd.SetCurrentFolder ("..\\");

			if (Fcd.Run() == (int)ResponseType.Accept) {
				String[] contenido = crear_archivo_SVG ();
				System.IO.StreamWriter file = new System.IO.StreamWriter(Fcd.Filename+".svg");
				file.WriteLine(contenido[0]);
				file.Close();
				System.IO.StreamWriter fileConf = new System.IO.StreamWriter(Fcd.Filename+".conf");
				fileConf.WriteLine(contenido[1]);
				fileConf.Close();
				limpiarPantalla ();
				desactivarTodo ();

			} 
				
			Fcd.Destroy();
		}

		private String[] crear_archivo_SVG(){

			string lines= "";
			string linesConfig= "";
			string encabezado = "<svg width=\"" + Constantes.limites.Width + "\" height=\"" + Constantes.limites.Height + "\">";
			string cierre = "\n</svg>";
			foreach (Obstaculo r in Logica.rectangulos) {
				lines += "\n<polygon points=\"" + r.rectangle.X + "," + r.rectangle.Y + "," + (r.rectangle.X + r.rectangle.Width) + "," + r.rectangle.Y +"," + (r.rectangle.X + r.rectangle.Width) + "," + (r.rectangle.Y+r.rectangle.Height) + "," + r.rectangle.X + "," + (r.rectangle.Y+r.rectangle.Height) + "\" style=\"fill:none;stroke:red;stroke-width:2\" />";
				linesConfig += r.intermitencia.ToString () + "\n";
			}
			foreach (Obstaculo c in Logica.circulos) {
				lines += "\n<circle cx=\"" + c.rectangle.X + "\" cy=\"" + c.rectangle.Y + "\" r=\"" + c.rectangle.Width + "\" stroke=\"blue\" stroke-width=\"2\" fill=\"none\" />";
				linesConfig += c.intermitencia.ToString () + "\n";
			}
			foreach (Obstaculo l in Logica.lineas) {
				lines += "\n<line x1=\""+l.rectangle.X+"\" y1=\""+l.rectangle.Y+"\" x2=\""+l.rectangle.Width+"\" y2=\""+l.rectangle.Height+"\" style=\"stroke:green;stroke-width:2\" />";
				linesConfig += l.intermitencia.ToString () + "\n";
			}

			lines += "\n<polyline points=\"";
			foreach (System.Drawing.Point p in Logica.puntos_inicio) {
				lines += " "+p.X+","+p.Y;
			}
			lines += "\" style=\"fill:none\" />";

			foreach (Objetivo o in Logica.puntos_objetivo) {
				lines += "\n<polyline points=\"";
				foreach (System.Drawing.Point p in o.Objetivos()) {
					lines += " "+p.X+","+p.Y;
				}
				lines += "\" style=\"fill:none\" />";
			}

			String[] s = new string[2];
			s[0] = encabezado + lines + cierre;
			s [1] = linesConfig;
			return s;

		}
        int n = 0;
        protected void OnExportarAVTKActionActivated(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            string lines = "# vtk DataFile Version 3.0" + "\n" + "vtk output" + "\n" + "ASCII" + "\n" + "DATASET POLYDATA" + "\n";
            lines += "POINTS " + (Logica.objects.Count-1).ToString() + " float" + "\n";

            for (int i = 1; i < Logica.objects.Count; i++)
                lines += Logica.objects[i].Location.X.ToString() + " " + Logica.objects[i].Location.Y.ToString() + " 0" + "\n";

            lines += "VERTICES " + (Logica.objects.Count-1).ToString() +" "+((Logica.objects.Count-1) * 2).ToString() + "\n";
            for (int i = 0; i < Logica.objects.Count-1; i++)
                lines += "1 " + i+"\n";

            lines += "POINT_DATA " + (Logica.objects.Count-1).ToString() + "\n";
            
            lines+= "SCALARS ejeX float"+"\n";
            lines+= "LOOKUP_TABLE default"+"\n";
            for (int i = 1; i < Logica.objects.Count; i++)
                lines += Logica.objects[i].Location.X.ToString()+"\n";

            lines += "SCALARS ejeY float" + "\n";
            lines += "LOOKUP_TABLE default" + "\n";
            for (int i = 1; i < Logica.objects.Count; i++)
                lines += Logica.objects[i].Location.Y.ToString() + "\n";

            using (StreamWriter file = new StreamWriter("boids"+n+".vtk")) 
            {
                file.Write(lines);
            }

            n++;


        }

		protected void OnEscenarioEnBlancoActionActivated (object sender, EventArgs e)
		{
			limpiarPantalla();
			activarBotonesSimulacion();
			Constantes.escenarioCargado = false;

		}

		private void activarBotonesSimulacion(){
			SimulacinAction.Sensitive=true;
			ConfiguracinAction.Sensitive=true;
			Constantes.simulacion=true;

			GuardarEscenarioAction.Sensitive=false;
			tb1.Sensitive=false;
			tb2.Sensitive=false;
			spinbuttonNivel.Sensitive = false;
			labelNivel.Sensitive = false;
			spinbuttonIntermitencia.Sensitive = false;
			labelIntermitencia.Sensitive = false;
			Constantes.escenario=false;

			grilla.Active = false;
			calor.Active = false;

			Logica.play = false;

		}

		private void activarBotonesNuevoEscenario(){
			GuardarEscenarioAction.Sensitive=true;
			Constantes.escenario=true;
			tb2.Sensitive=true;
			spinbuttonNivel.Sensitive = true;
			labelNivel.Sensitive = true;
			spinbuttonIntermitencia.Sensitive = true;
			labelIntermitencia.Sensitive = true;


			tb1.Sensitive=false;
			SimulacinAction.Sensitive=false;
			ConfiguracinAction.Sensitive=false;
			EstadsticaAction.Sensitive = false;
			Constantes.simulacion=false;

			grilla.Active = false;
			calor.Active = false;
		

		}

		private void desactivarTodo(){
			GuardarEscenarioAction.Sensitive=false;
			SimulacinAction.Sensitive=false;
			ConfiguracinAction.Sensitive=false;
			EstadsticaAction.Sensitive = false;
			tb1.Sensitive=false;
			tb2.Sensitive=false;
			spinbuttonNivel.Sensitive = false;
			labelNivel.Sensitive = false;
			spinbuttonIntermitencia.Sensitive = false;
			labelIntermitencia.Sensitive = false;

			Constantes.simulacion=false;
			Constantes.escenario=false;
			Constantes.escenarioCargado = false;
		}

		private void limpiarPantalla(){
			if (Constantes.viewSurface != null) {
				Constantes.viewSurface.Dispose ();
				Constantes.viewSurface = new Cairo.ImageSurface (Cairo.Format.ARGB32,Constantes.limites.Width,Constantes.limites.Height);
			}
			Logica.objects.Clear ();
			Logica.circulos.Clear ();
			Logica.rectangulos.Clear ();
			Logica.lineas.Clear ();
			Logica.puntos_inicio.Clear ();
			Logica.clear_puntos_objetivo ();
			Logica.grilla.limpiar_obstaculos ();
			Logica.grilla.limpiar_boids ();
			this.spinbuttonIntermitencia.Value = 0;
			Constantes.max_nivel = 1;
			Constantes.nivel_actual = 1;
			this.spinbuttonNivel.SetRange (1, 1);
			zoom_reset ();
		}
	


		protected void OnEscenarioDesdeArchivoActionActivated (object sender, EventArgs e)
		{
			FileChooserDialog Fcd = new FileChooserDialog ("Abrir escenario", this, FileChooserAction.Open,"Abrir",ResponseType.Accept,"Cancelar",ResponseType.Cancel);

			Fcd.SetCurrentFolder ("..\\");

			Fcd.Filter = new FileFilter();
			Fcd.Filter.AddPattern("*.svg");
			string tempname = GetEmptyPath ();

			if (Fcd.Run() == (int)ResponseType.Accept) {

				limpiarPantalla ();
				activarBotonesSimulacion ();
				String fileconfig = Fcd.Filename.Split ('.')[0] + ".conf";
				leer_archivo (Fcd.Filename, fileconfig);

//				Gdk.Pixbuf img=Rsvg.Pixbuf.FromFile (Fcd.Filename);
				Constantes.escenarioCargado = true;
//				img.Save (tempname, "png");
//				Constantes.escenarioSurface.Dispose ();
//				Constantes.escenarioSurface = new Cairo.ImageSurface (tempname);
//				File.Delete (tempname);
				GuardarEscenarioAction.Sensitive = true;
				Logica.grilla.cargar_obstaculos ();

			} 

			Fcd.Destroy();
		}

		private string GetEmptyPath ()
		{
			string bpath = "temp", path = "temp";
			for (int i = 0; File.Exists (path); i++)
				path = bpath + i.ToString ();
			return path;
		}

		private void leer_archivo(string path, string pathConfig){
			string line;
			string lineConfig;
			bool existe = false;
			System.IO.StreamReader fileConfig = null;
			System.IO.StreamReader file = new System.IO.StreamReader(path);
			if (File.Exists (pathConfig)) {
				fileConfig = new System.IO.StreamReader (pathConfig);
				existe = true;
			}
			Logica.rectangulos.Clear ();
			Logica.circulos.Clear ();
			Logica.lineas.Clear ();
			Logica.puntos_inicio.Clear ();
			Logica.puntos_objetivo.Clear ();
			int nivel = 0;

			while((line = file.ReadLine()) != null)
			{
				line.Trim ();
				if (line.StartsWith ("<polygon")) {

					string numero = "";
					int n = 0, i = 0;

					int[] rect = new int[8];
					char[] b = new char[line.Length];

					StringReader sr = new StringReader (line);
					sr.Read (b, 0, line.Length);

					while (n < line.Length && i < 8) {
						if (char.IsDigit(b [n]))
							numero += b [n];
						else if (numero != "") {
							rect [i] = Convert.ToInt32 (numero);
							i++;
							numero = "";
						}
						n++;
					}
					sr.Close ();
					int x0 = rect [0], y0 = rect [1], x1 = rect [2]-x0, y1 = rect [5]-y0;
					if (existe) {
						lineConfig = fileConfig.ReadLine ();
						Logica.rectangulos.Add (new Obstaculo (x0, y0, x1, y1, Convert.ToInt32 (lineConfig)));
					}
					else
						Logica.rectangulos.Add (new Obstaculo (x0, y0, x1, y1, 0));


				} else if (line.StartsWith ("<circle")) {

					string numero = "";
					int n = 0, i = 0;

					int[] circ = new int[3];
					char[] b = new char[line.Length];

					StringReader sr = new StringReader (line);
					sr.Read (b, 0, line.Length);

					while (n < line.Length && i < 3) {
						if (char.IsDigit(b [n]))
							numero += b [n];
						else if (numero != "") {
							circ [i] = Convert.ToInt32 (numero);
							i++;
							numero = "";
						}
						n++;
					}
					sr.Close ();
					int x0 = circ [0], y0 = circ[1], r=circ[2];
					if (existe) {
						lineConfig = fileConfig.ReadLine ();
						Logica.circulos.Add (new Obstaculo (x0, y0, r, 0, Convert.ToInt32 (lineConfig)));
					}
					else
						Logica.circulos.Add (new Obstaculo (x0, y0, r, 0, 0));

				} else if (line.StartsWith ("<line")) {

					string numero = "";
					int n = 0, i = 0;

					int[] lin = new int[4];
					char[] b = new char[line.Length];

					StringReader sr = new StringReader (line);
					sr.Read (b, 0, line.Length);

					while (n < line.Length && i < 4) {
						if (b [n] == 'x' || b [n] == 'y')
							n++;
                        else if (char.IsDigit(b[n]))
							numero += b [n];
						else if (numero != "") {
							lin [i] = Convert.ToInt32 (numero);					
							i++;
							numero = "";
						}
						n++;
					}
					sr.Close ();
					int x0 = lin [0], y0 = lin[1], x1=lin[2], y1=lin[3];
					if (existe) {
						lineConfig = fileConfig.ReadLine ();
						Logica.lineas.Add (new Obstaculo (x0, y0, x1, y1, Convert.ToInt32 (lineConfig)));
					}
					else
						Logica.lineas.Add (new Obstaculo (x0, y0, x1, y1, 0));
				}
				else if (line.StartsWith ("<polyline")) {
					if (nivel == 0) {
						string numero = "";
						int n = 0;
						int xo = 0, yo;
						int[] rect = new int[8];
						char[] b = new char[line.Length];
						char ultimo;
						StringReader sr = new StringReader (line);
						sr.Read (b, 0, line.Length);
						ultimo = b [0];
						while (n < line.Length) {
                            if (char.IsDigit(b[n]))
                            {
								numero += b [n];
								//System.Console.WriteLine ("0");
                            }
                            else if (b[n] == ',' && char.IsDigit(ultimo))
                            {
								xo = Convert.ToInt32 (numero);
								//System.Console.WriteLine ("1");
								numero = "";
                            }
                            else if (b[n] == ' ' && char.IsDigit(ultimo))
                            {
								yo = Convert.ToInt32 (numero);
//															System.Console.WriteLine ("2");
								Logica.puntos_inicio.Add (new System.Drawing.Point (xo, yo));
								numero = "";
                            }
                            else if (b[n] == '"' && char.IsDigit(ultimo))
                            {
								yo = Convert.ToInt32 (numero);
//															System.Console.WriteLine ("3");
								Logica.puntos_inicio.Add (new System.Drawing.Point (xo, yo));
								numero = "";
								n = line.Length - 1;
							}

							ultimo = b [n];
							n++;
						}
						nivel = 1;
						sr.Close ();
					} else {
						Objetivo o = new Objetivo ();
						string numero = "";
						int n = 0;
						int xo = 0, yo;
						int[] rect = new int[8];
						char[] b = new char[line.Length];
						char ultimo;
						StringReader sr = new StringReader (line);
						sr.Read (b, 0, line.Length);
						ultimo = b [0];
						while (n < line.Length) {
                            if (char.IsDigit(b[n]))
                            {
								numero += b [n];
								//							System.Console.WriteLine ("0");
                            }
                            else if (b[n] == ',' && char.IsDigit(ultimo))
                            {
								xo = Convert.ToInt32 (numero);
								//							System.Console.WriteLine ("1");
								numero = "";
                            }
                            else if (b[n] == ' ' && char.IsDigit(ultimo))
                            {
								yo = Convert.ToInt32 (numero);
								//							System.Console.WriteLine ("2");
								o.add_objetivo (xo,yo);
								numero = "";
                            }
                            else if (b[n] == '"' && char.IsDigit(ultimo))
                            {
								yo = Convert.ToInt32 (numero);
								//							System.Console.WriteLine ("3");
								o.add_objetivo (xo,yo);
								numero = "";
								n = line.Length - 1;
							}

							ultimo = b [n];
							n++;
						}
						nivel++;
						Logica.puntos_objetivo.Add (o);
						sr.Close ();
					}

				} 
			}

			file.Close();
			if(fileConfig!=null)
				fileConfig.Close ();
//			int j=0;
//			foreach (Objetivo o in Logica.puntos_objetivo) {
//				System.Console.WriteLine (j+" - "+o.Objetivos().Count);
//				j++;
//			}

		}
			

		protected void OnIniciarOgre3DActionActivated (object sender, EventArgs e)
		{
            Form2 form = new Form2();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true; /* run your code here */
                form.ShowDialog();
            }).Start();
            
		
		}


	}


}



