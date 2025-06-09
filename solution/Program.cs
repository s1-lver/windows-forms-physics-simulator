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
        
        PhysicsObject.Particle part1 = new("A", new Vector2(50, 200));
        PhysicsObject.Particle part2 = new("B", new Vector2(150, 200));

        part1.Mass = 1;
        part2.Mass = 5;
        part1.Velocity = new (2.5f, 3f);
        part2.Velocity = new (-0.5f, 3f);
        
        _scene.Add(part1);
        _scene.Add(part2);

        ApplicationConfiguration.Initialize();
        _physicsWindow = new PhysicsWindow(_scene);
        
        Application.Run(_physicsWindow);
    }
}