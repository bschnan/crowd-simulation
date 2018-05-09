using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Nuevo
{
    public class AABB
    {
        public float minX, minY, minZ, maxX, maxY, maxZ;
    }

    public  class OgreEntity
    {
        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setEntityScale(IntPtr e, float sc);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void setEntityPos(IntPtr e, float x, float y, float z);

        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityPosX(IntPtr e);

        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityPosY(IntPtr e);

        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityPosZ(IntPtr e);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMinX(IntPtr e);

        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMinY(IntPtr e);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMinZ(IntPtr e);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMaxX(IntPtr e);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMaxY(IntPtr e);


        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float getEntityMaxZ(IntPtr e);

        [DllImport("C:/OgreSDK/bin/debug/simpleDLL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr addMeshFromFile(string dllToLoad);

        public IntPtr meshPointer;

        protected  bool m_visible;

        public bool visible
        {
            get { return m_visible; }
            set { m_visible = value; }
        }

        public string name;

        virtual public void update() { }

        string m_filename;
        public string fileName
        {
            get { return m_filename; }
            set
            {
                m_filename = value.Replace(@"\", @"/");
                
                this.meshPointer = addMeshFromFile(m_filename);
            }
        }


        protected  float _posX, _posY, _posZ, _sC = 1.0f;

        public float scale
        {
            get { return _sC; }
            set
            {
                _sC = value;
                setEntityScale(meshPointer, _sC);
                this.update();
            }
        }


        public float posX
        {
            get { return _posX; }
            set
            {
                _posX = value;
                setEntityPos(meshPointer, _posX, _posY, _posZ);
            }
        }

        public float posY
        {
            get { return _posY; }
            set
            {
                _posY = value;
                setEntityPos(meshPointer, _posX, _posY, _posZ);
            }
        }

        public float posZ
        {
            get { return _posZ; }
            set
            {
                _posZ = value;
                setEntityPos(meshPointer, _posX, _posY, _posZ);
            }
        }

        protected AABB aabb = new AABB();

        protected void computeAABB()
        {
            aabb.minX = getEntityMinX(meshPointer);
            aabb.minY = getEntityMinY(meshPointer);
            aabb.minZ = getEntityMinZ(meshPointer);

            aabb.maxX = getEntityMaxX(meshPointer);
            aabb.maxY = getEntityMaxY(meshPointer);
            aabb.maxZ = getEntityMaxZ(meshPointer);

        }

        public AABB boundingBox
        {
            get
            {
                computeAABB();
                return aabb;
            }
            set
            {
                aabb = value;
            }
        }

    }
}
