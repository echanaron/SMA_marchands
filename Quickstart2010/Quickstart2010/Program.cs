using System;
using System.Windows.Forms;
using MogreFramework;
using Mogre;

namespace MogreTutorial
{
    #region class Program
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                MoveDemo win = new MoveDemo();
                win.Go();
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                if (OgreException.IsThrown)
                    MessageBox.Show(OgreException.LastException.FullDescription, "An Ogre exception has occurred!");
                else
                    throw;
            }
        }
    }
    #endregion

}