using System;
using GXPEngine;


public class Block : EasyDraw
{
    public static bool drawDebugLine = false;
    public static bool wordy = false;
    public static Vec2 acceleration = new Vec2(0, 0);

    public readonly int radius;

    public Vec2 velocity;


    private Vec2 _position;

    private Arrow _velocityIndicator;



    public Block(int pWidth,int pHeight, Vec2 pPosition) : base(pWidth * 2, pWidth * 2)
    {
        width = pWidth;
        height = pHeight;
        _position = pPosition;
        draw();
        UpdateScreenPosition();

        _velocityIndicator = new Arrow(_position, velocity, 10);
        AddChild(_velocityIndicator);
    }

    void UpdateScreenPosition()
    {
        x = _position.x;
        y = _position.y;
    }

    void draw()
    {
        Fill(200);
        NoStroke();
        ShapeAlign(CenterMode.Min, CenterMode.Min);
        Rect(0, 0, width, height);
    }
}
