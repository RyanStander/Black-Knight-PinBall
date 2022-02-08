using System;
using GXPEngine;

public struct Vec2 
{
	public float x;
	public float y;
    //public float degree;
    //public float radians;

	public Vec2 (float pX = 0, float pY = 0) 
	{
		x = pX;
		y = pY;
	}
    //|||||||||||||||||operator +|||||||||||||||||||||||||||
    public static Vec2 operator+ (Vec2 left, Vec2 right) {
		return new Vec2(left.x+right.x, left.y+right.y);
	}
    //|||||||||||||||||operator -|||||||||||||||||||||||||||
    public static Vec2 operator- (Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    //|||||||||||||||||operator *|||||||||||||||||||||||||||
    public static Vec2 operator* (Vec2 point,  float scalar)
    {
        return new Vec2(scalar * (point.x), scalar * (point.y));
    }

    public static Vec2 operator *(float scalar,Vec2 point )
    {
        return new Vec2(scalar * (point.x), scalar * (point.y));
    }
    public static Vec2 operator *(Vec2 point, Vec2 otherPoint)
    {
        return new Vec2((otherPoint.x) * (point.x), (otherPoint.y) * (point.y));
    }
    //|||||||||||||||||length|||||||||||||||||||||||||||
    public float Length()
    {
        return (Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)));
    }
    //|||||||||||||||||normalize|||||||||||||||||||||||||||
    public void Normalize()
    {
        Vec2 normalizedVector = this.Normalized();
        x = normalizedVector.x;
        y = normalizedVector.y;
    }
    //|||||||||||||||||normalized|||||||||||||||||||||||||||
    public Vec2 Normalized()
    {
        if (Mathf.Abs(Length()) < float.Epsilon)
        {
            return new Vec2(0f, 0f);
        }
        else
        return new Vec2(x / Length(), y / Length());
    }
    //|||||||||||||||||ToString|||||||||||||||||||||||||||
    public override string ToString () 
	{
		return String.Format ("({0},{1})", x, y);
	}
    //|||||||||||||||||SetXY|||||||||||||||||||||||||||
    public void SetXY(float tempX, float tempY)
    {
        x = tempX;
        y = tempY;
    }
    //Assignment 2
    public static float DegToRad(float tempRotation)
    {
        tempRotation = tempRotation * (Mathf.PI / 180);
        return tempRotation;
    }
    public static float RadToDeg(float tempDegrees)
    {
        tempDegrees = tempDegrees * (180 / Mathf.PI);
        return tempDegrees;
    }
    public static Vec2 GetUnitVectorDeg(float tempDegrees)
    {
        return new Vec2(Mathf.Cos(DegToRad(tempDegrees)),Mathf.Sin(DegToRad(tempDegrees)));
    }
    public static Vec2 GetUnitVectorRad(float tempRadians)
    {
        return new Vec2(Mathf.Cos(tempRadians), Mathf.Sin(tempRadians));
    }
    public static Vec2 RandomUnitVector()
    {
        float tempX = Utils.Random(-1f,1f);
        return new Vec2(tempX, Mathf.Sqrt(1 - Mathf.Pow(tempX, 2)));
    }
    public void SetAngleDegrees(float tempDegrees)
    {
        float tempRad= DegToRad(tempDegrees);
        float tempLength = Length();
        x = Mathf.Cos(tempRad) * tempLength;
        y = Mathf.Sin(tempRad) * tempLength;
    }
    public void SetAngleRadians(float tempRad)
    {
        float tempLength = Length();
        x = Mathf.Cos(tempRad) * tempLength;
        y = Mathf.Sin(tempRad) * tempLength;
    }
    public float GetAngleRadians()
    {
        return Mathf.Atan2(y , x);  
    }
    public float GetAngleDegrees()
    {
        return RadToDeg(Mathf.Atan2(y , x));
    }
    public void RotateRadians(float tempRadians)
    {
        Vec2 tempVector = this;
        x = tempVector.x * Mathf.Cos(tempRadians)-tempVector.y * Mathf.Sin(tempRadians);
        y = tempVector.x * Mathf.Sin(tempRadians) + tempVector.y * Mathf.Cos(tempRadians);
    }
    public void RotateDegrees(float tempDegrees)
    {
        float tempRadian = DegToRad(tempDegrees);
        Vec2 tempVector = this;
        x = tempVector.x * Mathf.Cos(tempRadian) - tempVector.y * Mathf.Sin(tempRadian);
        y = tempVector.x * Mathf.Sin(tempRadian) + tempVector.y * Mathf.Cos(tempRadian);
    }
    public void RotateAroundRadians(Vec2 point, float tempRadians)
    {
        this -= point;
        RotateRadians(tempRadians);
        this += point;
    }
    public void RotateAroundDegrees(Vec2 point, float tempDegrees)
    {
        this -= point;
        RotateDegrees(tempDegrees);
        this += point;
    }
    public float Dot(Vec2 other)
    {
        return x * other.x + y * other.y;
    }
    public Vec2 Normal()
    {
        Vec2 tempVector = new Vec2(-y, x);
        return new Vec2(-y/ tempVector.Length(), x/ tempVector.Length());
    }
    public void Reflect(float bounciness, Vec2 givenNormal)
    {
        this = this - (1 + bounciness) * (Dot(givenNormal)*givenNormal);
    }
}

