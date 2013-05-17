using System;
using System.Windows.Forms;
using System.Windows.Input;
using _3DScanner.Client.ViewModel;

namespace _3DScanner.Client.Commands
{
    class LoadFileCommand : ICommand
    {
        private object _viewModel;

        public LoadFileCommand(object viewModel)
        {
            _viewModel = viewModel;
        }

        public void Execute(object parameter)
        {
            OpenFileDialog dialog = new OpenFileDialog
                                        {
                                            Filter = "Pointcloud file (*.pcd) |*.pcd| All files (*.*) | *.*",
                                            Multiselect = false
                                        };

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                GlobalViewModel model = (GlobalViewModel) _viewModel;
                model.FileName = dialog.FileName;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
