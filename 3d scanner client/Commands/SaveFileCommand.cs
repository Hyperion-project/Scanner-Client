using System;
using System.Windows.Forms;
using System.Windows.Input;
using _3DScanner.Client.ViewModel;

namespace _3DScanner.Client.Commands
{
    public class SaveFileCommand : ICommand
    {
        /*private object _viewModel;

        public SaveFileCommand(object viewModel)
        {
            _viewModel = viewModel;
        }*/

        public void Execute(object parameter)
        {
            System.IO.Stream fs;
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Pointcloud file (*.pcd) |*.pcd| All files (*.*) | *.*",
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if((fs = dialog.OpenFile()) != null)
                {
                    fs.Close();
                }
            }
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}