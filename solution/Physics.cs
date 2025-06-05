using System.Numerics;

namespace solution;

public class PhysicsScene
{
    public PhysicsHandler PhysicsHandler = new();
    private HashSet<PhysicsObject> _physicsObjects = new();

    public void Update()
    {
        List<PhysicsObject> listObjects = _physicsObjects.ToList();
        
        for (int i = 0; i < _physicsObjects.Count; i++)
        {
            for (int j = i + 1; j < _physicsObjects.Count; j++)
            {
                PhysicsHandler.CheckParticleCollisions(listObjects[i], listObjects[j]);
            }
        }
    }

    void Add(PhysicsObject physicsObject)
    {
        _physicsObjects.Add(physicsObject);
    }

    void Destroy(PhysicsObject physicsObject)
    {
        _physicsObjects.Remove(physicsObject);
    }
}

public class PhysicsHandler(int gStrength = 10)
{
    public int GravitationalStrength = gStrength;
    
    public bool CheckParticleCollisions(PhysicsObject object1, PhysicsObject object2)
    {
        if (object1.Position == object2.Position)
        {
            CollideParticles(object1, object2);
            return true;
        }

        return false;
    }

    private void CollideParticles(PhysicsObject object1, PhysicsObject object2)
    {
        var (p_obj1, p_obj2) = PhysicalMath.ResolveMomentum(object1, object2);
        
        object1.Velocity = p_obj1 / object1.Mass;
        object2.Velocity = p_obj2 / object2.Mass;
    }
}
public class PhysicsObject(string label, Vector2 pos)
{
    public string ObjectLabel = label;
    public Vector2 Position = pos, Velocity = Vector2.Zero, ResultantForce = Vector2.Zero;
    public float Mass = 1;
    
    public class Particle(string label, Vector2 pos) : PhysicsObject(label, pos)
    {
        
    }
}


