using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

    static void Main()
    {
        new MyGame().Start();
    }
    //scoreboard text
    private EasyDraw _text;
    //pinball
    private Ball _ball;
    //playfield
    private List<Ball> _pins;
    
    private List<NLineSegment> _lineBoundaries;   
    public Vec2 lineVec;
   
    private Block _paddleLeft;
    private Block _paddleRight;
    public Vec2 paddleVecLeft;
    public Vec2 paddleVecRight;
    private int _leftPaddleSlap;
    private int _rightPaddleSlap;
    //testing if vec2 class works
    private UnitTesting _unitTest;
    //Timer for ball slapping
    private bool _leftHasSlapped=false;
    private bool _rightHasSlapped=false;

    public float _leftHitTimer;
    public float _rightHitTimer;
    //sound player
    private SoundPlayer _soundPlayer;

    //Magnet Save
    private MagnetSave _magSave;
    private bool _magnetActive = false;
    private float _timeStamp;

    //Overlay
    private List<SpriteOverlay> _overlay;
    private SpriteOverlay _ballOverlay;
    private SpriteOverlay _leftPaddleOverlay;
    private SpriteOverlay _rightPaddleOverlay;

    //Scoreboard
    public int points;
    public MyGame() : base(900, 1300, false, false)
    {        
        SoundSetup();             
        PaddleSetup();
        _ball = new Ball(15, new Vec2(width / 1.7f, height / 2));
        AddChild(_ball);

        _magSave = new MagnetSave(width / 1.95f, height / 1.3f);
        AddChild(_magSave);
        //------------------------------------------------------
        //                       Boundries
        //------------------------------------------------------
        AddLines();
        //------------------------------------------------------
        //                       Pins
        //------------------------------------------------------
        AddPins();
        //------------------------------------------------------
        //                       Unit Testing
        //------------------------------------------------------
        _unitTest = new UnitTesting();
        AddChild(_unitTest);
        //------------------------------------------------------
        //                       Sprite overlay
        //------------------------------------------------------
        OverlaySetup();
        //------------------------------------------------------
        //                       Scoreboard
        //------------------------------------------------------
        _text = new EasyDraw(250, 25);
        _text.TextAlign(CenterMode.Min, CenterMode.Min);
        AddChild(_text);
        //------------------------------------------------------
        //                       Controlls
        //------------------------------------------------------
        Console.WriteLine("Use left and right keys to use paddles, space bar to use magnet save");
        Console.WriteLine("Special thanks to Jacquas for helping me get through this course");
        Console.WriteLine("Thanks to Tobias and Leo for helping me when we were seriously struggling");
        Console.WriteLine("Last but not least thanks to Yiwer for being our teacher. You've helped tremendously!");
    }


    public void Update()
    {
        _ball.Step();
        PaddleHandler();
        _ballOverlay.x = _ball.x-15;
        _ballOverlay.y = _ball.y-14;
        _text.Clear(Color.Red);
        _text.Text("Score: " + points, 0, 0);
    }

    public float CalculateBallDistance(int lineNum)
    {
        Vec2 difference = _ball.position - _lineBoundaries[lineNum].start;
        lineVec = _lineBoundaries[lineNum].end - _lineBoundaries[lineNum].start;
        return difference.Dot(lineVec.Normal());
    }
    public float CalculateBallToPaddleLeft()
    {
        Vec2 difference = _ball.position - new Vec2(_paddleLeft.x + width, _paddleLeft.y + _leftPaddleSlap);
        paddleVecLeft =  new Vec2(_paddleLeft.x, _paddleLeft.y)- new Vec2(_paddleLeft.x+width, _paddleLeft.y+_leftPaddleSlap);
        return difference.Dot(paddleVecLeft.Normal());
    }
    public float CalculateBallToPaddleRight()
    {
        Vec2 difference = _ball.position - new Vec2(_paddleRight.x + width, _paddleRight.y + _rightPaddleSlap);
        paddleVecRight = new Vec2(_paddleRight.x, _paddleRight.y) - new Vec2(_paddleRight.x + width, _paddleRight.y + _rightPaddleSlap);
        return difference.Dot(paddleVecRight.Normal());
    }
    public float GetNumberOfLines()
    {
        return _lineBoundaries.Count;
    }
    public void AddLines()
    {
        _lineBoundaries = new List<NLineSegment>();
        //left wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(100, 1100), new Vec2(100, 250), 0xff00ff00, 3));
        //right wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(800, 250), new Vec2(800, 1100), 0xff00ff00, 3));
        //bottom wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(350, 1200), new Vec2(100, 1100), 0xff00ff00, 3));//left bottom wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(800, 1100), new Vec2(550, 1200), 0xff00ff00, 3));//right bottom wall
        //top wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(100, 250), new Vec2(350, 100), 0xff00ff00, 3));//left top wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(350, 100), new Vec2(550, 100), 0xff00ff00, 3));//middle top wall
        _lineBoundaries.Add(new NLineSegment(new Vec2(550, 100), new Vec2(800, 250), 0xff00ff00, 3));//right top wall
        //Line Adding
        foreach (NLineSegment i in _lineBoundaries)
        {
            AddChild(i);
        }
    }
    public Ball GetPinBall(int index)
    {
        if (index >= 0 && index < _pins.Count)
        {
            return _pins[index];
        }
        return null;
    }
    public int GetNumberOfPins()
    {
        return _pins.Count;
    }
    public void AddPins()
    {
        _pins = new List<Ball>();
        //middle balls
        _pins.Add(new Ball(30, new Vec2(400, 500)));
        _pins.Add(new Ball(30, new Vec2(600, 450)));
        _pins.Add(new Ball(30, new Vec2(500, 600)));
        //bottom balls
        _pins.Add(new Ball(40, new Vec2(300, 1000)));
        _pins.Add(new Ball(40, new Vec2(600, 1000)));
        //top squares
        _pins.Add(new Ball(20, new Vec2(350, 200)));
        _pins.Add(new Ball(20, new Vec2(350, 240)));
        _pins.Add(new Ball(20, new Vec2(450, 200)));
        _pins.Add(new Ball(20, new Vec2(450, 240)));
        _pins.Add(new Ball(20, new Vec2(550, 200)));
        _pins.Add(new Ball(20, new Vec2(550, 240)));
        foreach (Ball b in _pins)
        {
            AddChild(b);
        }
    }
    public void OverlaySetup()
    {
        _overlay = new List<SpriteOverlay>();
        //------------------------------------------------------
        //                       Wall overlay
        //------------------------------------------------------
        float tempX = 0;
        float tempY = 0;
        for (int b = 0; b < 9; b++)
        {
            for (int i = 0; i < 4; i++)
            {
                _overlay.Add(new SpriteOverlay("floor", tempX, tempY, 225, 150,0));
                tempX += 225;
            }
            tempX = 0;
            tempY += 150;
        }
        _overlay.Add(new SpriteOverlay("Wall", 350, 25, 200,75,0));//top middle wall
        _overlay.Add(new SpriteOverlay("Wall", 590, 34, 290, 75, 31));//top right wall
        _overlay.Add(new SpriteOverlay("Wall", 64, 184, 290, 75, -31));//top left wall
        //right wall
        _overlay.Add(new SpriteOverlay("Wall", 875, 245, 290, 75, 90));
        _overlay.Add(new SpriteOverlay("Wall", 875, 535, 290, 75, 90));
        _overlay.Add(new SpriteOverlay("Wall", 875, 825, 280, 75, 90));
        //left wall
        _overlay.Add(new SpriteOverlay("Wall", 25, 535, 290, 75, -90));
        _overlay.Add(new SpriteOverlay("Wall", 25, 825, 290, 75, -90));
        _overlay.Add(new SpriteOverlay("Wall", 25, 1105, 280, 75, -90));

        _overlay.Add(new SpriteOverlay("Wall", 835, 1165, 280, 75, 158));//right bottom wall

        _overlay.Add(new SpriteOverlay("Wall", 323, 1270, 280, 75, 202));//left bottom wall
        //------------------------------------------------------
        //                       Pillars overlay
        //------------------------------------------------------
        _overlay.Add(new SpriteOverlay("Pillar", 490, -15, 125, 125, 0));
        _overlay.Add(new SpriteOverlay("Pillar", 280, -15, 125, 125, 0));
        _overlay.Add(new SpriteOverlay("Pillar", 775, 145, 125, 125, 0));
        _overlay.Add(new SpriteOverlay("Pillar", 775, 1055, 125, 125, 0));
        _overlay.Add(new SpriteOverlay("Pillar", 5, 155, 125, 125, 0));
        _overlay.Add(new SpriteOverlay("Pillar", 10, 1055, 125, 125, 0));
        //------------------------------------------------------
        //                       Ball overlay
        //------------------------------------------------------
        //middle balls
        _overlay.Add(new SpriteOverlay("Targe",371,471,60,60, 0));
        _overlay.Add(new SpriteOverlay("Targe", 571, 421, 60, 60, 0));
        _overlay.Add(new SpriteOverlay("Targe", 471, 571, 60, 60, 0));
        //bottom balls
        _overlay.Add(new SpriteOverlay("Targe", 261, 961, 80, 80, 0));
        _overlay.Add(new SpriteOverlay("Targe", 561, 961, 80, 80, 0));
        //------------------------------------------------------
        //                       Block overlay
        //------------------------------------------------------
        _overlay.Add(new SpriteOverlay("Tower", 330, 180, 41, 81, 0));
        _overlay.Add(new SpriteOverlay("Tower", 430, 180, 41, 81, 0));
        _overlay.Add(new SpriteOverlay("Tower", 530, 180, 41, 81, 0));
        //------------------------------------------------------
        //                       Magnet overlay
        //------------------------------------------------------
        _overlay.Add(new SpriteOverlay("Magnet", width / 2.2f, height / 1.4f, 120, 120, 0));

        //Add all sprites
        foreach (SpriteOverlay s in _overlay)
        {
            AddChild(s);
        }
        
        //------------------------------------------------------
        //                       Paddle overlay
        //------------------------------------------------------
        _leftPaddleOverlay = new SpriteOverlay("Sword",350 ,1193 , 75, 30, 0);
        AddChild(_leftPaddleOverlay);
        _rightPaddleOverlay = new SpriteOverlay("Sword",542, 1222, 75,30,180);
        AddChild(_rightPaddleOverlay);
        //------------------------------------------------------
        //                       Ball overlay
        //------------------------------------------------------
        _ballOverlay = new SpriteOverlay("Ball", _ball.x, _ball.y, _ball.radius * 2, _ball.radius * 2, 0);
        AddChild(_ballOverlay);
    }
    public void PaddleHandler()
    {
        if (Input.GetKeyDown(Key.B))
        {
            _ball.acceleration *= -1;
        }
        if (Input.GetKeyDown(Key.LEFT))
        {
            _paddleLeft.rotation = -45;
            _leftPaddleOverlay.rotation = -45;
            _leftPaddleSlap = -600;
            if (!_leftHasSlapped)
            {
                _leftHitTimer = Time.time;

            }
            _leftHasSlapped = true;
        }
        if (Input.GetKeyUp(Key.LEFT))
        {
            _paddleLeft.rotation = 0;
            _leftPaddleOverlay.rotation = 0;
            _leftPaddleSlap = 0;
            _leftHasSlapped = false;
        }
        if (Input.GetKeyDown(Key.RIGHT))
        {
            _paddleRight.rotation = 45;
            _rightPaddleOverlay.rotation = 225;
            _rightPaddleSlap = 600;
            if (!_rightHasSlapped)
            {
                _rightHitTimer = Time.time;

            }
            _rightHasSlapped = true;
        }
        if (Input.GetKeyUp(Key.RIGHT))
        {
            _paddleRight.rotation = 0;
            _rightPaddleOverlay.rotation = 180;
            _rightPaddleSlap = 0;
            _rightHasSlapped = false;
        }
        //magnet save
        if (Input.GetKeyDown(Key.SPACE))
        {
            _magnetActive = true;
            _timeStamp = Time.time;
        }
        if (Input.GetKeyUp(Key.SPACE))
        {
            _magnetActive = false;
        }
        if (_magnetActive)
        {
            Vec2 MagnetDirection = _magSave.position - _ball.position;
            _ball.velocity += MagnetDirection *0.01f * (Time.time / _timeStamp);
            _ball.velocity *= 0.98f;
        }
    }
    public void SoundSetup()
    {
        _soundPlayer = new SoundPlayer();
        AddChild(_soundPlayer);
        _soundPlayer.gameMusic();
    }

    public void Bonk()
    {
        _soundPlayer.Bonk();
    }
    public void PaddleSetup()
    {
        _paddleLeft = new Block(150, 70, new Vec2(350, 1200));
        AddChild(_paddleLeft);

        _paddleRight = new Block(150, 70, new Vec2(550, 1200));
        _paddleRight.SetOrigin(_paddleRight.width, 1);
        AddChild(_paddleRight);
    }

    public void ScoreInc()
    {
        points++;
    }
    
}

