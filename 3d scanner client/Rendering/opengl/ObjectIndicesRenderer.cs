using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _3DScanner.Client.Rendering.opengl
{
    class ObjectIndicesRenderer
    {
        //Vertices
        private int _verticesVBO;
        private Vector3[] _vertices;

        //Indices
        private int _indicesVBO;
        private int[] _indices;

        public void Draw()
        {
            GL.Color3(Color.CornflowerBlue);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _verticesVBO);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indicesVBO);
            GL.DrawElements(BeginMode.Triangles, _indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.DisableClientState(ArrayCap.IndexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetVertices(List<Vector3> pointList)
        {
            _vertices = pointList.ToArray();
            UpdateVerticesVBO();
        }

        public void SetIndices(List<int> indices)
        {
            _indices = indices.ToArray();
            UpdateIndicesVBO();
        }


        private void CreateVerticesVBO()
        {
            GL.GenBuffers(1, out _verticesVBO);
        }

        private void CreateIndicesVBO()
        {
            GL.GenBuffers(1, out _indicesVBO);
        }

        private void UpdateVerticesVBO()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _verticesVBO);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          new IntPtr(_vertices.Length * Vector3.SizeInBytes),
                          _vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        private void UpdateIndicesVBO()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indicesVBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer,
                          new IntPtr(_indices.Length * sizeof(int)),
                          _indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public ObjectIndicesRenderer()
        {
            CreateIndicesVBO();
            CreateVerticesVBO();
            
        }
    }
}
