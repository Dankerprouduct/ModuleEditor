using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModuleEditor.EditorForms
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewModule module = new NewModule(mapDisplay1);
            module.Show(); 
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
