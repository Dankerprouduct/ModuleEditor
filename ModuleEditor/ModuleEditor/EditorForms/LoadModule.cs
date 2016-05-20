using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; 

namespace ModuleEditor.EditorForms
{
    public partial class LoadModule : Form
    {
        public EditorForm editorForm;
        public LoadModule(EditorForm form)
        {
            InitializeComponent();
            

            editorForm = form;
            editorForm.mapDisplay1.populatedList = false;
        }
        // Open
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
        // Load
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("No File Selected");
                return;
            }
            try {
                string path = textBox1.Text;
                editorForm.mapDisplay1.path = textBox1.Text;
                // Width and height of our tile array
                int width = 0;
                int height = File.ReadLines(path).Count();

                StreamReader sReader = new StreamReader(path);
                string line = sReader.ReadLine();
                string[] tileNo = line.Split(',');

                width = tileNo.Count();

                // Creating a new instance of the tile map
                editorForm.mapDisplay1.mapdata = new int[height, width];
                sReader.Close();

                // Re-initialising sReader
                sReader = new StreamReader(path);

                for (int x = 0; x < width; x++)
                {
                    line = sReader.ReadLine();
                    tileNo = line.Split(',');

                    for (int y = 0; y < height; y++)
                    {
                        editorForm.mapDisplay1.mapdata[x, y] = Convert.ToInt32(tileNo[y]);
                    }
                }
                sReader.Close();
                editorForm.mapDisplay1.populatedList = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Try Again"); 
            }
            Close(); 
        }
    }
}
