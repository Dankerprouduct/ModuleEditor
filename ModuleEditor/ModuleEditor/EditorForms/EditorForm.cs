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
            listBox1.DrawMode = DrawMode.OwnerDrawVariable;
            listBox1.SelectionMode = SelectionMode.One; 

        }

        public void AddTile(Tile tile)
        {
            listBox1.Items.Add(tile); 
        }
        private int itemMargin = 5;
        private int pictureHeight = 64; 
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            
            e.ItemHeight = (int)pictureHeight + 2 * itemMargin;
            
        }
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Get the ListBox and the item.
            ListBox lst = sender as ListBox;
            Tile tile = (Tile)lst.Items[e.Index];

            // Draw the background.
            e.DrawBackground();

            // Draw the picture. // THIS SHIT RIGHT HERE IS EXTREMELY IMPORTANT 
            float scale = pictureHeight / 32;
            RectangleF source_rect = tile.sourceRect; 



            float picture_width = scale * 32;
            RectangleF dest_rect = new RectangleF(
                e.Bounds.Left + itemMargin, e.Bounds.Top + itemMargin,
                picture_width, pictureHeight);
            e.Graphics.DrawImage(tile.Picture, dest_rect,
                source_rect, GraphicsUnit.Pixel);

            // See if the item is selected.
            Brush br;
            if ((e.State & DrawItemState.Selected) ==
                DrawItemState.Selected)
                br = SystemBrushes.HighlightText;
            else
                br = new SolidBrush(e.ForeColor);

            // Find the area in which to put the text.
            float x = e.Bounds.Left + picture_width + 3 * itemMargin;
            float y = e.Bounds.Top + itemMargin;
            float width = e.Bounds.Right - itemMargin - x;
            float height = e.Bounds.Bottom - itemMargin - y;
            RectangleF layout_rect = new RectangleF(x, y, width, height);

            // Draw the text.
            string txt = tile.Name;
            e.Graphics.DrawString(txt, this.Font, br, layout_rect);

            // Outline the text.
            e.Graphics.DrawRectangle(Pens.Red,
                Rectangle.Round(layout_rect));

            // Draw the focus rectangle if appropriate.
            e.DrawFocusRectangle();
        }

        

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewModule module = new NewModule(mapDisplay1, this);
            module.Show(); 
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadModule module = new LoadModule(this);
            module.Show(); 
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveModule module = new SaveModule(this);
            module.Show(); 
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
    
}
