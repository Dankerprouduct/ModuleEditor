using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ShipGameLibrary;
using Microsoft.Xna.Framework.Input;

namespace ModuleEditor.CustomControls
{
    public class MapDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl
    {
        MouseState mouseState;
        MouseState oldMouseState;
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private int width = 0;
        private int height = 0;
        Texture2D blankTile;
        Texture2D spriteSheet; 
        Stopwatch timer;
        public string path; 
        EditorForms.EditorForm editorform;
        int index = 0;
        public int[,] mapdata;
        public List<Microsoft.Xna.Framework.Rectangle> sourceRects = new List<Microsoft.Xna.Framework.Rectangle>(); 
        public bool populatedList = false;
        public ShipGameLibrary.Tile[] tiles; 
        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadContent(content);

            // Start the animation timer.
            timer = Stopwatch.StartNew();

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }
        public void LoadContent(ContentManager content)
        {
            blankTile = content.Load<Texture2D>("BlankTile"); 



        }
        public void SetDimensions(int _width, int _height)
        {
            width = _width;
            height = _height;
            mapdata = new int[width, height]; 
            for(int x = 0; x < mapdata.GetLength(0); x++)
            {
                for(int y = 0; y < mapdata.GetLength(1); y++)
                {
                    mapdata[x, y] = 0;
                }
            }
            Console.WriteLine(width + " " + height);
        }
        public void LoadTileMap(string txtPath, string pngPath, EditorForms.EditorForm form)
        {
            populatedList = true; 
            editorform = form; 
            //form.AddTile(new EditorForms.Tile("", new Bitmap(pngPath)));
            string path = txtPath; 
            StreamReader sr = new StreamReader(path);
                        
            int height = File.ReadLines(path).Count();

            char[] splits = { '=', ' ' };
            sourceRects = null;
            sourceRects = new List<Microsoft.Xna.Framework.Rectangle>(); 
            tiles = content.Load<ShipGameLibrary.Tile[]>("Xml/Tile");
            spriteSheet = ConvertToTexture(new Bitmap(pngPath), GraphicsDevice);
            form.listBox1.Items.Clear(); 
            for (int y = 0; y < height; y++)
            {
                string line = sr.ReadLine();
                string[] rectData = line.Split(splits);

                foreach (string s in rectData)
                {
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        if (tiles[i].name == s)
                        {
                            sourceRects.Add(new Microsoft.Xna.Framework.Rectangle(Convert.ToInt32(rectData[3]), Convert.ToInt32(rectData[4]), Convert.ToInt32(rectData[5]), Convert.ToInt32(rectData[6])));
                            form.AddTile(new EditorForms.Tile(s, new Bitmap(pngPath), new RectangleF(Convert.ToInt32(rectData[3]), Convert.ToInt32(rectData[4]), Convert.ToInt32(rectData[5]), Convert.ToInt32(rectData[6]))), tiles[i]);

                        }
                    }

                }
            }
        }
        public static Texture2D ConvertToTexture(System.Drawing.Bitmap b, GraphicsDevice graphicsDevice)
        {
            Texture2D tx = null;
            using (MemoryStream s = new MemoryStream())
            {
                b.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                s.Seek(0, SeekOrigin.Begin);
                tx = Texture2D.FromStream(graphicsDevice, s);
            }
            return tx;
        }
        private Vector2 GetWorldMouseCoord()
        {
            System.Drawing.Point p = PointToScreen(this.Location);
            return new Vector2(mouseState.X - p.X, mouseState.Y - p.Y);
        }
        protected override void Draw()
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            if (populatedList && editorform.listBox1.Items != null)
            {
                if (editorform.listBox1.Items.Count > 0)
                {
                    index = editorform.listBox1.SelectedIndex;
                }
            }
            mouseState = Mouse.GetState();
            Vector2 position = GetWorldMouseCoord(); 
            int xCord = Convert.ToInt32(position.X / 32);
            int yCord = Convert.ToInt32(position.Y / 32);


            if (populatedList && editorform.listBox1.Items != null)
            {
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                   // Console.WriteLine("Pressed"); 
                    if (xCord >= 0 && xCord < mapdata.GetLength(0))
                    {
                        if (yCord >= 0 && yCord < mapdata.GetLength(1))
                        {
                            mapdata[xCord, yCord] = index;
                        }
                    }
                }
            }
            
            oldMouseState = mouseState; 
            spriteBatch.Begin();
            if (populatedList && editorform.listBox1.Items != null)
            {
                for (int x = 0; x < mapdata.GetLength(0); x++)
                {
                    for (int y = 0; y < mapdata.GetLength(1); y++)
                    {
                        spriteBatch.Draw(spriteSheet, new Microsoft.Xna.Framework.Rectangle(x * 32, y * 32, 32, 32), sourceRects[mapdata[x, y]], Microsoft.Xna.Framework.Color.White);
                    }
                }
            }
            spriteBatch.End(); 
        }

        public int GetIndex(int i)
        {
            return 0; 

            
        }
    }
}
