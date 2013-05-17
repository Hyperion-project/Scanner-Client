using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace _3DScanner.Client.Rendering.opengl
{
    class GridRenderer
    {
        //Settings
        private const int size = 10;
        private const int num = 128;
        private Color color = Color.DarkGray;

        //Privates
        private const int half = ((size * num) / 2);
        private int _gridVBO;
        private Vector3[] _grid;

        public GridRenderer()
        {
            //Create vertices
            List<Vector3> gridList = new List<Vector3>();

            for (int i = 0; i <= num; i++)
            {
                gridList.Add(new Vector3(-half, (i * size) - half, 0));
                gridList.Add(new Vector3(half, (i * size) - half, 0));
            }

            for (int i = 0; i <= num; i++)
            {
                gridList.Add(new Vector3((i * size) - half, -half, 0));
                gridList.Add(new Vector3((i * size) - half, half, 0));
            }
            _grid = gridList.ToArray();

            //Store in VBO
            GL.GenBuffers(1, out _gridVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _gridVBO);
            GL.BufferData(BufferTarget.ArrayBuffer,
                          new IntPtr(_grid.Length * Vector3.SizeInBytes),
                          _grid, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Draw()
        {
            GL.Color3(color);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _gridVBO);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.DrawArrays(BeginMode.Lines, 0, _grid.Length);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
