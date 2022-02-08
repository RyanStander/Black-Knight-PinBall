using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MagnetSave : Sprite
    {
        public Vec2 position;
        public MagnetSave(float posX,float posY) : base("Magnet.png")
        {
            SetOrigin(width / 2, height / 2);
            x = posX;
            y = posY;
            position = new Vec2(posX, posY);
        }
    }
}
