using System.Numerics;

namespace solution;

public static class PhysicalMath
{
    public static Vector2 CalculateMomentum(PhysicsObject obj)
    {
        return obj.Velocity * obj.Mass;
    }

    public static (Vector2, Vector2) ResolveMomentum(PhysicsObject obj1, PhysicsObject obj2, float e = 1)
    {
        Vector2 p_obj1 = 0.5f * ((obj1.Mass + e * obj2.Mass) * obj1.Velocity + (e + 1f) * obj2.Mass * obj2.Velocity);
        Vector2 p_obj2 = CalculateMomentum(obj1) + CalculateMomentum(obj2) - p_obj1;
        
        return (p_obj1, p_obj2);
    }

    public static float GetInsideAngle(Vector2 vect)
    {
        var normVect = new Vector2(Math.Abs(vect.X), Math.Abs(vect.Y));
        
        return (float)Math.Atan(normVect.Y / normVect.X);
    }

    public static float GetDistance(Vector2 vect1, Vector2 vect2)
    {
        return (vect1 - vect2).Length();
    }
    public static bool CheckParticlesWillPass(PhysicsObject obj1, PhysicsObject obj2)
    {
        if (GetDistance(obj1.Position, obj2.Position) <= 1 && (GetInsideAngle(obj1.Velocity) < (float)Math.PI / 2 || GetInsideAngle(obj2.Velocity) < (float)Math.PI / 2))
        {
            return true;
        }
        return false;
    }
}