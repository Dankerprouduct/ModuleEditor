using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using System.Diagnostics;

namespace ModuleEditor.CustomControls
{
    public class MapDisplay : WinFormsGraphicsDevice.GraphicsDeviceControl
    {

        private ContentManager content;
        private SpriteBatch spriteBatch;
        private int width = 0;
        private int height = 0;
        Texture2D blankTile;

        Stopwatch timer; 
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
            Console.WriteLine(width + " " + height);
        }
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(); 
            
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    spriteBatch.Draw(blankTile, new Vector2(x * 32,  y * 32), Color.White); 
                }
            }
            
            spriteBatch.End(); 
        }
    }
}
