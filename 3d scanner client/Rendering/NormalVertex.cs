using System.Runtime.InteropServices;
using OpenTK;

namespace _3DScanner.Client.Rendering
{
    [StructLayout(LayoutKind.Sequential)]
    struct NormalVertex
    {
        public NormalVertex(Vector3 normal,Vector3 position)
        {
            Normal = normal;
            Position = position;
        }
        public Vector3 Normal;
        public Vector3 Position;
    }
}