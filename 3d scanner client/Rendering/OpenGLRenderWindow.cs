using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms.Integration;
using OpenTK;

namespace _3DScanner.Client.Rendering
{
    class OpenGLRenderWindow : WindowsFormsHost, IRenderWindow
    {
        private readonly OpenGlEngine _glEngine;

        public OpenGLRenderWindow()
        {
            _glEngine = new OpenGlEngine();
            Child = _glEngine;
        }

        public UIElement GetUIElement()
        {
            return this;
        }

        public void SetPoints(List<Vector3> pointList)
        {
           _glEngine.SetPoints(pointList);
        }

        public void SetVertices(List<Vector3> verticeList)
        {
            _glEngine.SetVertices(verticeList);
        }

        public void SetCamera(Camera camera)
        {
            _glEngine.Camera = camera;
        }

        public Color Color
        {
            get { return _glEngine.Color; }
            set { _glEngine.Color = value; }
        }

        public float PointSize
        {
            get { return _glEngine.PointSize; }
            set { _glEngine.PointSize = value; }
        }
    }
}
