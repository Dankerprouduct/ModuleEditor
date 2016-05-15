using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModuleEditor.EditorForms
{
    public partial class SaveModule : Form
    {
        EditorForm editorForm; 
        public SaveModule(EditorForms.EditorForm from)
        {
            InitializeComponent();
            editorForm = from;
            editorForm.mapDisplay1.populatedList = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("No File Selected");
                return; 
            }

            try {
                bool flag = false;
                StreamWriter streamWriter = new StreamWriter(textBox1.Text);
                string output = "";
                for (int i = 0; i < editorForm.mapDisplay1.mapdata.GetLength(0); i++)
                {
                    flag = false;
                    for (int j = 0; j < editorForm.mapDisplay1.mapdata.GetLength(1); j++)
                    {
                        if (flag)
                        {
                            output += "," + editorForm.mapDisplay1.mapdata[i, j].ToString();
                        }
                        else
                        {
                            output += editorForm.mapDisplay1.mapdata[i, j].ToString();
                            flag = true;
                        }
                    }
                    streamWriter.WriteLine(output);
                    output = "";
                }
                streamWriter.Close();
                editorForm.mapDisplay1.populatedList = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please Try Again");
            }
            Close(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveModule_Load(object sender, EventArgs e)
        {

        }
    }
}
