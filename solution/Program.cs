using System.Numerics;

namespace solution;

static class Program
{
    private static PhysicsScene _scene;
    private static PhysicsWindow _physicsWindow;
    
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        _scene = new PhysicsScene();
        
        PhysicsObject.Particle part1 = new("A", new Vector2(100, 40));
        PhysicsObject.Particle part2 = new("B", new Vector2(200, 40));
        PhysicsObject.Particle part3 = new("C", new Vector2(250, 40));
        part1.Mass = 2;
        part2.Mass = 1.5f;
        part1.Velocity = new (1, 0);
        part2.Velocity = new (-2, 0);
        part3.Velocity = new(-0.16666667f, 0);
        
        _scene.Add(part1);
        _scene.Add(part2);
        _scene.Add(part3);

        ApplicationConfiguration.Initialize();
        _physicsWindow = new PhysicsWindow(_scene);
        
        Application.Run(_physicsWindow);
    }
}