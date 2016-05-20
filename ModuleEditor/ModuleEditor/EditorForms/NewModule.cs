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
        EditorForm editorForm;
        public NewModule(CustomControls.MapDisplay _mapDisplay, EditorForm form)
        {

            InitializeComponent();
            editorForm = form;
            
            mapDisplay = _mapDisplay;
            textBox1.Text = "5";
            textBox2.Text = "5"; 
        }
        private void NewModule_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(430, 147);
            this.MaximumSize = new Size(430, 147); 
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            editorForm.mapDisplay1.populatedList = false;
          //  editorForm.mapDisplay1.sourceRects = new List<Microsoft.Xna.Framework.Rectangle>(); 
            if (textBox3.Text == string.Empty || textBox4.Text == string.Empty)
            {
                MessageBox.Show("No File Opened");
                return;
            }
            try {
                mapDisplay.SetDimensions(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
                mapDisplay.LoadTileMap(textBox3.Text, textBox4.Text, editorForm);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Try Again"); 
            }
            this.Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (.txt)|*.txt"; 
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
                editorForm.mapDisplay1.path = textBox3.Text; 
            }
                        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.png)|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = ofd.FileName;
            }
        }

        
    }
}
