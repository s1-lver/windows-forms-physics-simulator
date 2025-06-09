using System.Drawing;
using System.Numerics;

namespace solution;

public static class Draw
{

    public static Dictionary<string, dynamic> ParticleProperties = new()
    {
        ["Size"] = 11,
        ["LabelOffset"] = new Vector2(8, 8)
    };
    
    public static void RenderObjects(Graphics graphicsHandler, PhysicsScene scene)
    {
        var pen = new Pen(Color.Black, 2);
        var brush = new SolidBrush(Color.Black);
        var font = new Font("Arial", 10);

        var objects = scene.GetObjectsInScene();
        foreach (var obj in objects)
        {
            if (obj is PhysicsObject.Particle)
            {
                if (obj.ObjectLabel == "B") brush.Color = Color.Red;
                graphicsHandler.FillEllipse(brush, obj.Position.X, obj.Position.Y, ParticleProperties["Size"], ParticleProperties["Size"]);
                // Label
                var offsetPos = obj.Position + ParticleProperties["LabelOffset"];
                graphicsHandler.DrawString(obj.ObjectLabel, font, brush, offsetPos.X, offsetPos.Y);
            }
        }
        
        pen.Dispose();
        brush.Dispose();
        font.Dispose();
    }
}