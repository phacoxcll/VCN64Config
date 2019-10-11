using System;
using System.Windows.Forms;

namespace VCN64Config
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length == 2 && args[0] == "validate")
            {
                if (Validator.Evaluate(args[1]))
                    return 0;
                else
                    return 1;
            }
            else 
            {
                FreeConsole();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormEditor());
                return 0;
            }
        }
    }
}
