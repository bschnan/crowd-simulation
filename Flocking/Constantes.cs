using System;
using Cairo;
using System.Collections.Generic;


namespace Nuevo
{
    public class Constantes
    {
        public static ImageSurface viewSurface;
        //		public static ImageSurface escenarioSurface;
        public static bool escenarioCargado = false;

        public static System.Drawing.Rectangle limites;

        public static double sx = 1, sy = 1;

        public static uint milisegundos_iteracion = 60;

        public static bool simulacion = false;

        public static bool escenario = false;

        public static int nro_casillas = 30;

        public static int radio_vecinos = 50;

        public static bool grilla = true;

        public static bool reboto = true;

        public static int tiempo = 0;

        public static int lapsos = 100;

        public static int iteraciones_lapso = 30;

        public static int max_nivel = 1;

        public static int nivel_actual = 1;

        public static int radio_objetivos = 20;

        public static bool objetivoCercano = false;

        public static bool objetivoLibre = false;

        public static bool objetivoAzar = false;

        public static bool simulacionContinua = true;

        public static bool ogre3d = false;

        public static bool sincronizar = false;


        //Para mostrar las estadisticas

        public static int min_tiempo = 300000000;

        public static int max_tiempo = 0;

        public static double tiempo_acum = 0;

        public static int nro_trayectorias = 0;

        public static List<int> tiempos_boids = new List<int>();

        public static TimeSpan r_acum = new TimeSpan(0);

        public static TimeSpan a_acum = new TimeSpan(0);

        public static int iteraciones_total = 0;


        public static int distancia(int xo, int x1)
        {
            if (xo < x1)
                return x1 - xo;
            return xo - x1;
        }

        public static double distancia_dos_puntos(int xo, int yo, int x1, int y1)
        {
            double cuadrado1 = Math.Pow(x1 - xo, 2);
            double cuadrado2 = Math.Pow(y1 - yo, 2);
            return Math.Sqrt(cuadrado1 + cuadrado2);
        }

        public static double max(double r1, double r2, double r3)
        {
            if (r1 <= r2 && r3 <= r2)
                return r2;
            if (r1 <= r3 && r2 <= r3)
                return r3;
            if (r2 <= r1 && r3 <= r1)
                return r1;
            return 0;
        }

        public static double max(double r1, double r2)
        {
            if (r1 <= r2)
                return r2;
            else
                return r1;
        }

        public static void addObstacles(List<Obstaculo> origen, List<Obstaculo> destino)
        {
            foreach (Obstaculo b in origen)
                if (!contiene(destino, b))
                    destino.Add(b);
        }

        public static void resetEstadisticas()
        {
            min_tiempo = 300000000;
            max_tiempo = 0;
            tiempo_acum = 0;
            nro_trayectorias = 0;
            r_acum = new TimeSpan(0);
            a_acum = new TimeSpan(0);
        }

        public static bool sin_movimiento_estadistica()
        {
            if (min_tiempo == 300000000 && max_tiempo == 0 && tiempo_acum == 0 && nro_trayectorias == 0)
                return true;
            return false;
        }

        public static bool contiene(List<Obstaculo> l, Obstaculo o)
        {
            foreach (Obstaculo obst in l)
            {
                if (obst.rectangle.X == o.rectangle.X && obst.rectangle.Y == o.rectangle.Y &&
                    obst.rectangle.Width == o.rectangle.Width && obst.rectangle.Height == o.rectangle.Height)
                    return true;
            }
            return false;
        }

        public static double mapearOgre(double dato, double coordenada)
        {
            double invertido = coordenada - dato;
            return invertido - (coordenada / 2);
        }

        public static double mapearOgreObs(double dato, double coordenada)
        {
            return dato - (coordenada / 2);
        }

        public static double calculateRotationOgre(Vector v)
        {

            double radianes = Math.Atan2(v.Y, v.X);
            double grados = (radianes * 180) / Math.PI;
            return -grados;
        }
    }
}