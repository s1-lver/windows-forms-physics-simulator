using System.Numerics;

namespace solution;

public class PhysicsScene
{
    public PhysicsHandler PhysicsHandler = new();
    private List<PhysicsObject> _physicsObjects = new();

    public void Update()
    {
        for (int i = 0; i < _physicsObjects.Count; i++)
        {
            _physicsObjects[i].Update();
            for (int j = i + 1; j < _physicsObjects.Count; j++)
            {
                PhysicsHandler.CheckParticleCollisions(_physicsObjects[i], _physicsObjects[j]);
            }
        }
    }

    public List<PhysicsObject> GetObjectsInScene()
    {
        return _physicsObjects;
    }
    public void Add(PhysicsObject physicsObject)
    {
        _physicsObjects.Add(physicsObject);
    }

    public void Destroy(PhysicsObject physicsObject)
    {
        _physicsObjects.Remove(physicsObject);
    }
}

public class PhysicsHandler(int gStrength = 10)
{
    public int GravitationalStrength = gStrength;
    
    public bool CheckParticleCollisions(PhysicsObject object1, PhysicsObject object2)
    {
        if (object1.Position == object2.Position || PhysicalMath.CheckParticlesWillPass(object1, object2))
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
        
        Console.WriteLine($"collision: {object1.Velocity}, {object2.Velocity}");
    }
}
public class PhysicsObject(string label, Vector2 pos)
{
    public string ObjectLabel = label;
    public Vector2 Position = pos, Velocity = Vector2.Zero, ResultantForce = Vector2.Zero;
    public float Mass = 1;
    
    public void Update()
    {
        Position += Velocity;
        
    }
    
    public class Particle(string label, Vector2 pos) : PhysicsObject(label, pos)
    {
        
    }
}


