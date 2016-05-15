using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework; 
namespace ModuleEditor
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 

            using (EditorForms.EditorForm editor = new EditorForms.EditorForm())
            {
                Application.Run(editor); 
            }
        } 
    }

}

