using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Mogre;

namespace WindowsFormsApplication1
{
    public partial class ProjetSMA : Form
    {
        public ProjetSMA()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Simulation win = new Simulation();
                win.Close();
                win.Go();

            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                if (OgreException.IsThrown)
                {
                    MessageBox.Show(OgreException.LastException.FullDescription, "An Ogre exception has occurred!");
                }
                else
                    throw;
            }
        }

        private void ProjetSMA_Load(object sender, EventArgs e)
        {

        }
    }
}
