using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NSOEndingTreeMaker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var appDomain = AppDomain.CurrentDomain;
            if (string.IsNullOrEmpty(Properties.Settings.Default.GameDirectory))
            {
                CheckGamePath();
            }
            appDomain.AssemblyResolve += OnAssemblyResolve;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EndingTreeForm());
        }

        static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            string requestedAssemblyName = args.Name.Split(',')[0];
            string gamePath;
            try
            {
                if (requestedAssemblyName == "Assembly-CSharp")
                {
                    gamePath = GameLocation.InitializeValidGamePath();
                    return Assembly.LoadFrom(gamePath + @"\Windose_Data\Managed\" + requestedAssemblyName + ".dll");
                }

                return Assembly.LoadFrom(Path.Combine(Directory.GetCurrentDirectory(), "Assemblies", requestedAssemblyName + ".dll"));
            }
            catch
            {
                return null;
            }
        }
        static void CheckGamePath()
        {
            DialogResult msg;
            string gamePath = GameLocation.InitializeValidGamePath();
            if ((gamePath == null && string.IsNullOrEmpty(Properties.Settings.Default.GameDirectory)) || !Directory.Exists(gamePath))
            {
                do
                {
                    msg = MessageBox.Show("To continue, please open the folder containing the game executable. (NEEDY GIRL OVERDOSE)", "", MessageBoxButtons.OKCancel);
                    if (msg == DialogResult.Cancel)
                        Environment.Exit(0);
                } while ((gamePath = OpenGamePath()) == null);
                Properties.Settings.Default.GameDirectory = gamePath;
                Properties.Settings.Default.Save();
            }
        }
        static string OpenGamePath()
        {
            var openNsoStream = new FolderBrowserDialog();
            openNsoStream.RootFolder = Environment.SpecialFolder.MyComputer;
            if (openNsoStream.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(openNsoStream.SelectedPath + @"\Windose_Data\Managed\Assembly-CSharp.dll"))
                {
                    return null;
                }
                return openNsoStream.SelectedPath;
            }
            return null;
        }
    }
    
}
