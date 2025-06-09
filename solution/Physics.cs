using System.Numerics;

namespace solution;

public class PhysicsScene
{
    public PhysicsHandler PhysHandler = new();
    private List<PhysicsObject> _physicsObjects = new();

    static int _updateNo = 0;
    public void Update()
    {
        for (int i = 0; i < _physicsObjects.Count; i++)
        {
            _physicsObjects[i].Update();
            for (int j = i + 1; j < _physicsObjects.Count; j++)
            {
                PhysHandler.CheckParticleCollisions(_physicsObjects[i], _physicsObjects[j]);
            }
        }
        
        PhysHandler.Update();
        _updateNo++;
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
    
    public class PhysicsHandler()
    {
        private List<(int, PhysicsObject[])> _lastCollisions = new();

        public void Update()
        {
            _lastCollisions = _lastCollisions.Where(x => x.Item1 != _updateNo - 1).ToList();
        }
        public bool CheckParticleCollisions(PhysicsObject object1, PhysicsObject object2)
        {
            if (_lastCollisions.Any(x => x.Item2.Contains(object1) && x.Item2.Contains(object2))) return false;
            if (PhysicalMath.CheckParticlesWillPass(object1, object2))
            {
                CollideParticles(object1, object2);
                return true;
            }

            return false;
        }

        private void CollideParticles(PhysicsObject object1, PhysicsObject object2)
        {
            var (v_obj1, v_obj2) = PhysicalMath.ResolveVelocityFromMomentum(object1, object2);
        
            object1.Velocity = v_obj1;
            object2.Velocity = v_obj2;
        
            Console.WriteLine($"collision: {object1.Velocity}, {object2.Velocity}");
            _lastCollisions.Add((_updateNo, [object1, object2]));
        }
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


