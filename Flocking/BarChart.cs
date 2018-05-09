using System;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Nuevo
{
	public class BarChart
	{
		Chart chart1 = new Chart();

		public BarChart ()
		{
			InitializeComponent ();
		}


		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
//			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
//			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			// 
			// chart1
			// 
			chartArea1.Name = "ChartArea1";
			chartArea1.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

			chartArea1.AxisX.Title = "Nro. de iteraciones";
			chartArea1.AxisY.Title = "Nro. de boids";
			this.chart1.ChartAreas.Add(chartArea1);
//			legend1.Name = "Legend1";
//			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(76, 34);
			this.chart1.Name = "chart1";
//			series1.ChartArea = "ChartArea1";
//			series1.Legend = "Legend1";
//			series1.Name = "Series1";
//			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(1200, 600);
			this.chart1.TabIndex = 0;
			this.chart1.Text = "chart1";


		}
			

		public void load()
		{

			Constantes.tiempos_boids.Sort ();
			int n = Constantes.tiempos_boids.Count - 1;
			int lapso;
			if ((Constantes.tiempos_boids [n] - Constantes.tiempos_boids [0]) == 0)
				lapso = 0;
			else
				lapso = (int)((Constantes.tiempos_boids [n] - Constantes.tiempos_boids [0])/10);
			int[] barras = new int[10];
			int[] cantidad = new int[10];
			int anterior = Constantes.tiempos_boids [0];
			for (int i = 0; i < 10; i++) {
				barras [i] = anterior + lapso;
				anterior = barras [i];
				cantidad [i] = 0;
			}
			int j = 0;
			for (int i = 0; i < 10; i++) {
				while (j <= n && Constantes.tiempos_boids[j]<=barras[i]) {
					cantidad[i]++;
					j++;
				}
			}

			// Set palette.
			this.chart1.Palette = ChartColorPalette.Berry;

			// Set title.
			this.chart1.Titles.Add("Iteraciones para completar objetivo");
			// Add series.
			this.chart1.Series.Add ("boids");

			for (int i = 0; i < 10; i++)
			{
				if(cantidad[i]!=0)
					this.chart1.Series["boids"].Points.AddXY((barras[i]-lapso).ToString()+" - "+barras[i].ToString(),cantidad[i]);

			}

			this.chart1.SaveImage ("..\\chart.png", ChartImageFormat.Png);

			SaveArrayAsCSV (Constantes.tiempos_boids, "archivoCVS.csv");


		}

		public void SaveArrayAsCSV(System.Collections.Generic.List<int> listaTiempos, string fileName)
		{
			using (StreamWriter file = new StreamWriter(fileName))
			{
				for (int i=0;i< listaTiempos.Count;i++)
				{
					file.Write(listaTiempos[i]);
					file.Write (Environment.NewLine);
				}

			}
		}


	}


}

