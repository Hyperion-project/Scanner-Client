using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _3DScanner.Client.Rendering.opengl
{
    class ObjectNormalRenderer
    {
        private int _normalVerticeVBO;
        private Vector3[] _vertices;
        private NormalVertex[] _normalVertices;

        public ObjectNormalRenderer()
        {
            GL.GenBuffers(1,out _normalVerticeVBO);
            _normalVertices = new NormalVertex[0];
        }

        public void Draw()
        {
            GL.Color3(Color.CornflowerBlue);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _normalVerticeVBO);
            GL.InterleavedArrays(InterleavedArrayFormat.N3fV3f, 0, IntPtr.Zero);
            GL.DrawArrays(BeginMode.Triangles, 0,_normalVertices.Length);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetVertices(List<Vector3> vertices)
        {
            _vertices = vertices.ToArray();
            List<NormalVertex> normalVertices = new List<NormalVertex>();
            for (int i = 0; i < _vertices.Length; i=i+3)
            {
                Vector3 a, b,normal;
                a = _vertices[i] - _vertices[i + 1];
                b = _vertices[i + 1] - _vertices[i + 2];
                normal.X = (a.Y * b.Z) - (a.Z * b.Y);
                normal.Y = (a.Z * b.X) - (a.X * b.Z);
                normal.Z = (a.X * b.Y) - (a.Y * b.X);
                normal = normalize(normal);
                normalVertices.Add(new NormalVertex(normal, vertices[i]));
                normalVertices.Add(new NormalVertex(normal, vertices[i+1]));
                normalVertices.Add(new NormalVertex(normal, vertices[i+2]));
            }
            _normalVertices = normalVertices.ToArray();
            UpdateVBO();
        }

        private void UpdateVBO()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _normalVerticeVBO);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          new IntPtr(_normalVertices.Length * Vector3.SizeInBytes*2),
                          _normalVertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private Vector3 normalize(Vector3 input)
        {
            float len = input.Length;
            if (len == 0.0f)
                len = 1.0f;
            input /= len;
            return input;
        }
    }
}
