using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _3DScanner.Client.Rendering.opengl
{
    class PointCloudRenderer
    {
        //Privates
        private int _pointCloudVBO;
        private Vector3[] _pointcloud;

        public PointCloudRenderer()
        {
            _pointcloud = new Vector3[0];
            GL.GenBuffers(1, out _pointCloudVBO);
            UpdatePointCloud();
        }

        public void Draw()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _pointCloudVBO);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.DrawArrays(BeginMode.Points, 0, _pointcloud.Length);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void SetPointcloud(List<Vector3> pointcloud)
        {
            _pointcloud = pointcloud.ToArray();
            UpdatePointCloud();
        }

        private void UpdatePointCloud()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _pointCloudVBO);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          new IntPtr(_pointcloud.Length * Vector3.SizeInBytes),
                          _pointcloud, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
