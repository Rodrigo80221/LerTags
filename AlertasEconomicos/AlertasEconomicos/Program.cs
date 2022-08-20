using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace AlertasEconomicos
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmAlertas());

            
        }
    }
}
