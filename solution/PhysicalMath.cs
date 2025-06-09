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
    
    public static Vector3 CrossProduct(Vector2 v1, Vector2 v2, float threshold = 0.001f)
    {
        var result = new Vector3(0, 0, v1.X*v2.Y - v1.Y*v2.X);
        if (result.Z <= threshold) return Vector3.Zero;
        return result;
    }
    
    public static bool CheckParticlesWillPass(PhysicsObject obj1, PhysicsObject obj2, float threshold = 0.001f)
    {
        var A = obj1.Position;
        var B = obj1.Position + obj1.Velocity;
        var C = obj2.Position;
        
        if (CrossProduct(A - B, C - A) == Vector3.Zero)
        {
            var Kac = Vector2.Dot(A - B, C - A);
            var Kab = Vector2.Dot(A - B, A - B);
            if (Kac >= 0 && Kab >= Kac) return true;
        }

        return false;
    }
}