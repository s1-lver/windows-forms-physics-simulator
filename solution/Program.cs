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
        
        PhysicsObject.Particle part1 = new("A", new Vector2(40, 40));
        PhysicsObject.Particle part2 = new("B", new Vector2(120, 40));
        part1.Mass = 2;
        part1.Velocity = new Vector2(1, 0);
        part2.Velocity = new Vector2(-2, 0);
        
        _scene.Add(part1);
        _scene.Add(part2);

        ApplicationConfiguration.Initialize();
        _physicsWindow = new PhysicsWindow(_scene);
        
        Application.Run(_physicsWindow);
    }
}