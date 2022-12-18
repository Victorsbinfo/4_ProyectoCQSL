using ProyectoCSQL.Principal;
using System;
using System.Windows.Forms;

namespace ProyectoCSQL
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmUsuario());
        }
    }
}
