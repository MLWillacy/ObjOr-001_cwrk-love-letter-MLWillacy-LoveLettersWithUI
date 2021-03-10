using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackerChase
{
    class Target : Sprite
    {
        public int Score { get; set; }
        static Random rand = new Random();
        override public void Reset()
        {
            int x = rand.Next(0, screenWidth - rectangle.Width);
            int y = rand.Next(0, screenHeight - rectangle.Height);
            SetPosition(x, y);
        }

        public Target(int inScreenWidth, int inScreenHeight, Texture2D inSpriteTexture, int inDrawWidth, float inResetX, float inResetY) :
            base(inScreenWidth, inScreenHeight, inSpriteTexture, inDrawWidth, inResetX, inResetY)
        {
            Score = 10;
        }

    }
}
