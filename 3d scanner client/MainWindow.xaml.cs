using Microsoft.Windows.Controls.Ribbon;
using _3DScanner.Client.Rendering;
using _3DScanner.Client.GUI;
using System.Windows.Forms;
using System;
using _3DScanner.Client.ViewModel;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Input;
using System.Collections.Generic;


namespace _3DScanner.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {

       // OpenGLRenderWindow renderWindow;
        Camera c = new Camera();
        private Dictionary<Key, bool> _keys = new Dictionary<Key, bool>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GlobalViewModel();
            Interface iss = new Interface();
            
            border1.Child = ((GlobalViewModel) DataContext).RenderWindow.GetUIElement();


            // Set een default camera view

            //renderWindow.SetCamera(new Camera{Pan = 0.05f});
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { X = 10 });

           
            //verticeList.Add(new Vector3(0, 0, 0));          //0
            //verticeList.Add(new Vector3(100, 0, 0));        //1
            //verticeList.Add(new Vector3(0, 100, 0));        //2
            //verticeList.Add(new Vector3(0, 0, 100));        //3
            //verticeList.Add(new Vector3(0, 100, 100));      //4
            //verticeList.Add(new Vector3(100, 100, 0));      //5
            //verticeList.Add(new Vector3(100, 0, 100));      //6
            //verticeList.Add(new Vector3(100, 100, 100));    //7
            //verticeList.Add(new Vector3(0, 0, 0));          //0

            //int[] temp = {0, 2, 5, 0, 1, 5, //bottom
            //             0,3,4,0,2,4, //left
            //             0,3,6,0,1,6,//front
            //             7,5,2,7,4,2, //back
            //             7,6,3,7,4,3, //top
            //             7,5,1,7,6,1 //right
            //             }; 
            //var indiceList = new List<int>(temp);
            //renderWindow.SetIndices(indiceList);

            //renderWindow.SetVertices(verticeList);
        }


        private void AboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Replicator create by Team 7");
        }
        
        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { Z = -0.05f });
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { Z = +0.05f });
        }

        private void PanRightButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Console.WriteLine("Right down");
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { Yaw = -0.05f });
        }

        private void PanRightButton_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Console.WriteLine("Right up");
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { Yaw = 0.0f });

        }

        private void PanRightButton_Click(object sender, EventArgs e)
        {

            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(new Camera { Pan = 0.05f });

        }

        private bool getKey(Key key)
        {
            bool val;
            if (_keys.TryGetValue(key, out val))
                return val;
            return false;
        }


        private void RibbonWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!getKey(e.Key))
            {
                switch (e.Key)
                {
                    case System.Windows.Input.Key.Right:
                        c.Pan += 0.1f;
                        break;
                    case System.Windows.Input.Key.Left:
                        c.Pan -= 0.1f;
                        break;
                    case System.Windows.Input.Key.Up:
                        c.Yaw += 0.1f;
                        break;
                    case System.Windows.Input.Key.Down:
                        c.Yaw -= 0.1f;
                        break;
                    case System.Windows.Input.Key.Space:
                        c.Z -= 0.1f;
                        break;
                    case System.Windows.Input.Key.Back:
                        c.Z += 0.1f;
                        break;
                }
                ((GlobalViewModel)DataContext).RenderWindow.SetCamera(c);
                _keys[e.Key] = true;
            }
        }

        private void RibbonWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Right:
                    c.Pan -= 0.1f;
                    break;
                case System.Windows.Input.Key.Left:
                    c.Pan += 0.1f;
                    break;
                case System.Windows.Input.Key.Up:
                    c.Yaw -= 0.1f;
                    break;
                case System.Windows.Input.Key.Down:
                    c.Yaw += 0.1f;
                    break;
                case System.Windows.Input.Key.Space:
                    c.Z += 0.1f;
                    break;
                case System.Windows.Input.Key.Back:
                    c.Z -= 0.1f;
                    break;
            }
            ((GlobalViewModel)DataContext).RenderWindow.SetCamera(c);
            _keys[e.Key] = false;
        } 
    }
}
