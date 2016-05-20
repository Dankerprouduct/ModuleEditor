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
                           // output += "," + editorForm.mapDisplay1.mapdata[i, j].ToString();
                            output += "," + getIndex(editorForm.mapDisplay1.mapdata[i, j], i, j); 
                        }
                        else
                        {
                            //output += editorForm.mapDisplay1.mapdata[i, j].ToString();
                            output += getIndex(editorForm.mapDisplay1.mapdata[i, j], i, j);
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
        private int getIndex(int index, int x, int y)
        {
            Console.WriteLine(editorForm.items.Count()); 
            for(int i = 0; i < editorForm.items.Count(); i++)
            {
                
                Console.WriteLine(editorForm.items[index].name +" --- " + editorForm.mapDisplay1.tiles[i].name);

                if (editorForm.items[index].name == editorForm.mapDisplay1.tiles[i].name)
                {
                    Console.WriteLine("GETEEM");
                    return editorForm.mapDisplay1.tiles[i].id;
                }
            }
            Console.WriteLine("Failed"); 
            return 0; 
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveModule_Load(object sender, EventArgs e)
        {

        }
    }
}
