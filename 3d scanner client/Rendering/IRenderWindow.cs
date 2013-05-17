using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using OpenTK;

namespace _3DScanner.Client.Rendering
{
    interface IRenderWindow
    {
        UIElement GetUIElement();
        void SetPoints(List<Vector3> pointList);
        void SetVertices(List<Vector3> verticeList);
        void SetCamera(Camera camera);
    }
}
