using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using _3DScanner.Client.Rendering.opengl;

namespace _3DScanner.Client.Rendering
{
    public class OpenGlEngine : GLControl
    {
        //Settings
        private readonly Color _backgroundColor = Color.Black;
        private const bool Vsync = true;

        //Camera
        public Camera Camera = new Camera();
        private readonly Camera _realCamera = new Camera();

        //My privates
        private readonly Stopwatch _frameStopwatch = new Stopwatch();
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer(DispatcherPriority.Render);

        //Renderers
        private readonly ObjectNormalRenderer _objectRenderer;
        private readonly GridRenderer _gridRenderer;
        private readonly PointCloudRenderer _pointCloudRenderer;

        public OpenGlEngine()
        {
            Load += OnLoad;
            Paint += OnPaint;
            Resize += OnResize;

            _dispatcherTimer.Tick += ThreadIdle;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 13);

            Context.VSync = Vsync;
            _realCamera = new Camera { Pan = 40, Yaw = -40, Z = 300, X = 0, Y = 0, Roll = 0 };

            _objectRenderer = new ObjectNormalRenderer();
            _gridRenderer = new GridRenderer();
            _pointCloudRenderer = new PointCloudRenderer();

            _dispatcherTimer.Start();
        }

        private void ThreadIdle(object sender, EventArgs eventArgs)
        {
                UpdateView();
                Render();
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            GL.ClearColor(_backgroundColor);
            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);
            // These things are necessary for anti-aliasing
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.Blend);
            
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            // end
            GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 80.0f, 40.0f, 100.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f,1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
        }

        private void OnResize(object sender, EventArgs eventArgs)
        {
            float aspect = (float) Width/(float) Height;
            // Set up initial modes
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Viewport(0, 0, Width, Height);
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect, 1, 700);
            GL.LoadMatrix(ref matrix);
        }

        private void OnPaint(object sender, PaintEventArgs paintEventArgs)
        {
            UpdateView();
            Render();
        }

        private void UpdateView()
        {
            double milis = ComputeTimeSlice();
            _realCamera.Update(Camera,(float)milis);
        }

        private double ComputeTimeSlice()
        {
            _frameStopwatch.Stop();
            double timeslice = _frameStopwatch.Elapsed.TotalMilliseconds;
            _frameStopwatch.Reset();
            _frameStopwatch.Start();
            return timeslice;
        }


        private void Render()
        {

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            Matrix4 modelview = Matrix4.LookAt(0,0, _realCamera.Z,0, 0, 0, 0, 1, 0);
            GL.LoadMatrix(ref modelview);

            GL.Rotate(_realCamera.Yaw, Vector3.UnitX);
            GL.Rotate(_realCamera.Roll, Vector3.UnitY);
            GL.Rotate(_realCamera.Pan, Vector3.UnitZ);

            //Draw things
            GL.Color3(Color);
            GL.PointSize(PointSize);
            GL.Disable(EnableCap.Lighting);
            _pointCloudRenderer.Draw();
            _gridRenderer.Draw();
            GL.Enable(EnableCap.Lighting);
            _objectRenderer.Draw();

            SwapBuffers();
        }

        private Color _color = Color.LightBlue;
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private float _pointSize = 1;
        public float PointSize
        {
            get { return _pointSize; }
            set { _pointSize = value; }
        }

        public void SetPoints(List<Vector3> pointList)
        {
           _pointCloudRenderer.SetPointcloud(pointList);
        }

        public void SetVertices(List<Vector3> verticeList)
        {
            _objectRenderer.SetVertices(verticeList);
        }
    }
}
