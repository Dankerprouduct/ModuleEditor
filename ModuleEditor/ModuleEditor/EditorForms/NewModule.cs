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
    public partial class NewModule : Form
    {
        CustomControls.MapDisplay mapDisplay;
        public NewModule(CustomControls.MapDisplay _mapDisplay)
        {
            InitializeComponent();
            mapDisplay = _mapDisplay; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mapDisplay.SetDimensions(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            this.Close(); 
        }
    }
}
