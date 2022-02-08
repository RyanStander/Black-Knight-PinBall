using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class SpriteOverlay : Sprite
    {
        public SpriteOverlay(string imgName, float xPos, float yPos,int tWidth,int tHeight,int tRotation) : base(imgName + ".png")
        {
            x = xPos;
            y = yPos;
            width = tWidth;
            height = tHeight;
            rotation = tRotation;
        }
    }
}
