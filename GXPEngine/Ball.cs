using System;
using GXPEngine;

public class Ball : EasyDraw
{
	public int radius {
		get {
			return _radius;
		}
	}

	public Vec2 velocity;
	public Vec2 position;
    public Vec2 acceleration = new Vec2(0, 0.25f);
    public Vec2 resetPosition;
    public float bounciness = 0.8f;
	private int _radius;
	private float _speed;

	public Ball (int pRadius, Vec2 pPosition, float pSpeed=5) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		_radius = pRadius;
		position = pPosition;
		_speed = pSpeed;

		UpdateScreenPosition ();
		SetOrigin (_radius, _radius);

		Draw (255, 255, 255);
	}

    public void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (_radius, _radius, 2*_radius, 2*_radius);
        velocity = new Vec2(0, 0);
            }

    public void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

	public void Step () {

        //FollowMouse ();
        velocity += acceleration;
        position += velocity;
        BallReset();
        PaddleReset();
        UpdateScreenPosition ();
        CollisionInfo firstCollision = FindEarliestCollision();
        if (firstCollision != null)
        {
            ResolveCollision(firstCollision);

        }
    }
    //------------------------------------------------------
    //                       Paddle Collisions
    //------------------------------------------------------
    public void PaddleReset()
    {
        MyGame myGame = (MyGame)game;
        if (myGame.CalculateBallToPaddleLeft() < radius && 150<x && 400>x && y<1200)
        {
            SetColor(1, 0, 0);
            position += (-myGame.CalculateBallToPaddleLeft() + radius) * myGame.paddleVecLeft.Normal();
            PaddleReflectLeft();
        }
        else
        {
            SetColor(0, 1, 0);
        }
        if (myGame.CalculateBallToPaddleRight() < radius && 480 < x && 620 > x && y < 1200)
        {
            SetColor(1, 0, 0);
            position += (-myGame.CalculateBallToPaddleRight() + radius) * myGame.paddleVecRight.Normal();
            PaddleReflectRight();
        }
        else
        {
            SetColor(0, 1, 0);
        }

    }
    public void PaddleReflectLeft()
    {
        MyGame myGame = (MyGame)game;
        if (myGame._leftHitTimer+500 >  Time.time)
        {
            velocity += new Vec2(0, -20);
        }
            velocity.Reflect(bounciness, myGame.paddleVecLeft.Normal());
    }
    public void PaddleReflectRight()
    {
        MyGame myGame = (MyGame)game;
        if (myGame._rightHitTimer+500 >  Time.time)
        {           
            velocity += new Vec2(0, -20);
        }
            velocity.Reflect(bounciness * 1, myGame.paddleVecRight.Normal());
    }
    //------------------------------------------------------
    //                       Line Collisions
    //------------------------------------------------------
    public void BallReset()
    {
        MyGame myGame = (MyGame)game;
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            if (myGame.CalculateBallDistance(i) < radius && 1215>y)
            {
                SetColor(1, 0, 0);
                position += (-myGame.CalculateBallDistance(i) + radius) * myGame.lineVec.Normal();
                ballReflect();
            }
            else
            {
                SetColor(0, 1, 0);
            }
        }
    }
    public void ballReflect()
    {
        MyGame myGame = (MyGame)game;
        velocity.Reflect(bounciness, myGame.lineVec.Normal());

    }
    //------------------------------------------------------
    //                       Ball Collisions
    //------------------------------------------------------
    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;		
        for (int i = 0; i < myGame.GetNumberOfPins(); i++)
        {
            Ball pins = myGame.GetPinBall(i);
            if (pins != this)
            {
                Vec2 relativePosition = position - pins.position;
                if (relativePosition.Length() < radius + pins.radius)
                {
                    return new CollisionInfo(new Vec2(1, 0), pins, 0);                    
                }
            }
        }
        return null;
    }
    public void ResolveCollision(CollisionInfo col)
    {
        if (col.other is Ball)
        {
            MyGame myGame = (MyGame)game;
            myGame.Bonk();
            myGame.ScoreInc();
            Ball otherBall = (Ball)col.other;
            Vec2 _difference = position - otherBall.position;
            float overlap = otherBall.radius + radius - _difference.Length();
            _difference.Normalize();
            position += _difference * overlap;
            velocity.Reflect(bounciness*1.5f, _difference);
        }
    }
}
