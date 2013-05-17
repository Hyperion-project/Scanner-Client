using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3DScanner.Client.GUI
{
    class Interface
    {
        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zoom in");
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zoom out");
        }

        private void PanLeftButton_Click2(object sender, EventArgs e)
        {
            MessageBox.Show("Panning left");
        }

        private void PanRightButton_Click2(object sender, EventArgs e)
        {
            MessageBox.Show("Panning right");
        }

        private void PanUpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Panning up");
        }

        private void PanDownButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Panning down");
        }

    }
}
