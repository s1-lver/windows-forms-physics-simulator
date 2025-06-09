using System.Numerics;

namespace solution;

public static class PhysicalMath
{
    public static Vector2 CalculateMomentum(PhysicsObject obj)
    {
        return obj.Velocity * obj.Mass;
    }

    public static (Vector2, Vector2) ResolveVelocityFromMomentum(PhysicsObject obj1, PhysicsObject obj2, float e = 1)
    {
        Vector2 n = e * (obj1.Velocity - obj2.Velocity);
        Vector2 v_obj1 = (obj1.Mass * obj1.Velocity + obj2.Mass * obj2.Velocity - obj2.Mass * n) / (obj1.Mass + obj2.Mass);
        Vector2 v_obj2 = v_obj1 + n;
        
        return (v_obj1, v_obj2);
    }
    
    public static Vector3 CrossProduct(Vector2 v1, Vector2 v2)
    {
        var result = new Vector3(0, 0, v1.X*v2.Y - v1.Y*v2.X);
        return result;
    }
    
    public static bool CheckParticlesWillPass(PhysicsObject obj1, PhysicsObject obj2)
    {
        var A = obj1.Position;
        var B = obj1.Position + obj1.Velocity;
        var C = obj2.Position;
        var AtoB = B - A;
        if (Math.Abs(CrossProduct(-AtoB, C - A).Z) < obj1.Velocity.Length())
        {
            var Kac = Vector2.Dot(-AtoB, C - A);
            var Kab = Vector2.Dot(-AtoB, -AtoB);
            if (Kac >= 0 && Kab >= Kac) return true;
        } 
        if (Math.Abs(CrossProduct(AtoB, C - A).Z) < obj1.Velocity.Length())
        {
            var Kac = Vector2.Dot(AtoB, C - A);
            var Kab = Vector2.Dot(AtoB, AtoB);
            if (Kac >= 0 && Kab >= Kac) return true;
        }

        return false;
    }
}