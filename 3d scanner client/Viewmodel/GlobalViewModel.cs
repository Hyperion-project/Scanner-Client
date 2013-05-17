using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Input;
using OpenTK;
using PointCloud;
using PointCloud.io;
using _3DScanner.Client.Commands;
using _3DScanner.Client.Rendering;

namespace _3DScanner.Client.ViewModel
{
    class GlobalViewModel
    {
        private OpenGLRenderWindow _renderWindow = new OpenGLRenderWindow();
        private string _fileName = "";
        private PointCloud<PointXYZ> _cloud; 

        public GlobalViewModel()
        {
            LoadFileCommand = new LoadFileCommand(this);
            SaveFileCommand = new SaveFileCommand();
        }

        public ICommand LoadFileCommand { get; set; }
        public ICommand SaveFileCommand { get; set; }

        public PointCloud<PointT> PointCloud { get; set; }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                loadFile();
            }
        }

        public float PointSize
        {
            get { return _renderWindow.PointSize; }
            set { _renderWindow.PointSize = value; }
        }
        
        public Color DrawingColor
        {
            get { return _renderWindow.Color; }
            set { _renderWindow.Color = value; }
        }

        public OpenGLRenderWindow RenderWindow
        {
            get { return _renderWindow; }
            set { _renderWindow = value; }
        }

        private void loadFile()
        {
            _cloud = PCDReader<PointXYZ>.LoadPCDFile(FileName);

            this.RenderWindow.SetPoints(_cloud.Points.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList());
        }
    }
}
