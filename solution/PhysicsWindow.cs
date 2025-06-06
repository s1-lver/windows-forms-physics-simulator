using System.Numerics;

namespace solution;

public partial class PhysicsWindow : Form
{
    private PhysicsScene _scene;
    private System.Windows.Forms.Timer _timer;
    public PhysicsWindow(PhysicsScene scene)
    {
        _scene = scene;
        
        this.SetStyle(ControlStyles.DoubleBuffer |
                      ControlStyles.UserPaint |
                      ControlStyles.AllPaintingInWmPaint, 
            true);
        this.UpdateStyles();
        
        InitializeComponent();
        _timer = new System.Windows.Forms.Timer();
        _timer.Interval = 16;
        _timer.Tick += (s, e) =>
        {
            _scene.Update(_timer.Interval * 0.001);
            Invalidate();
        };
        _timer.Start();
    }

    private void PhysicsWindow_Paint(object sender, PaintEventArgs e)
    {
        Draw.RenderObjects(e.Graphics, _scene);
    }
}

