using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nuevo
{
    public class ogreSceneWrap
    {
            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string dllToLoad);

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);


            [DllImport("kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr hModule);

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void initTerrainInterface();

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void setCamera(float x, float y, float z, float vx, float vy, float vz);

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern IntPtr addBox(string name, float x, float y, float z, float sx, float sy, float sz, float orientation);

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern IntPtr addFromFile(string name, string filename, float x, float y, float z, float sx, float sy, float sz, float orientation, string animation);

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void setColor(IntPtr entity, float r, float g, float b);

            //[DllImport("C:/OgreSDK/bin/Release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void runUI();

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void closeAPP();

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void removeEntities();

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void createGround();

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void moveNode(IntPtr entity, float x, float y, float z);

            //[DllImport("C:/OgreSDK/bin/release/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            //public static extern void rotateNode(IntPtr entity, float rot);


//----------------------------------------------------------------------------------------------------------------------------------------------


            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void initTerrainInterface();

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void setCamera(float x, float y, float z, float vx, float vy, float vz);

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr addBox(string name, float x, float y, float z, float sx, float sy, float sz, float orientation);

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr addFromFile(string name, string filename, float x, float y, float z, float sx, float sy, float sz, float orientation, string animation);

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void setColor(IntPtr entity, float r, float g, float b);

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void runUI();

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void closeAPP();

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void removeEntities();

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void createGround();

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void moveNode(IntPtr entity, float x, float y, float z);

            [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
            public static extern void rotateNode(IntPtr entity, float rot);
        

        public static void initOgre()
        {
            
           ogreSceneWrap.runUI();
        }

        public static IntPtr addWall(string name, float x, float y, float z, float sx, float sy, float sz, float orientation )
        {
            return  addBox(name, x,y, z, sx, sy,sz, orientation);
        }

        public static IntPtr addBoid(string name, float x, float y, float z, float sx, float sy, float sz, float orientation)
        {
            return addFromFile(name, "MAN01.mesh", x, y, z, sx, sy, sz, orientation, "guywalk");
        }

        public static IntPtr addObstacle(string name,int obstacleType, float x, float y, float z, float sx, float sy, float sz, float orientation)
        {
            return addFromFile(name, "column.mesh", x, y, z, sx, sy, sz, orientation,"");
        }
    }
}
