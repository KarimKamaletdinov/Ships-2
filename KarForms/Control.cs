using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using KeyPressEventArgs = System.Windows.Forms.KeyPressEventArgs;
using System.Drawing;

namespace KarForms
{
    public class Control
    {
        public Control Parent
        {
            get
            {
                return _parent;
            }
            
            set
            {
                if (value == null)
                {
                    _parent.Controls.Remove(this);
                }
                else
                {
                    value.Controls.Add(this);
                }
                
                _parent = value;

                ParentSet();
            }
        }
        
        private Control _parent { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int TextX { get; set; }

        public int TextY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Selected { get; set; } = false;

        public string Text { get; set; } = "";

        public List<Control> Controls { get; } = new List<Control>();
        
        public Control SelectedControl { get; set; }

        public Color BackColor { get; set; } = SystemColors.Control;

        public Color BorderColor { get; set; } = SystemColors.ControlDark;

        public Color SelectColor { get; set; } = SystemColors.Control;

        public Color TextColor { get; set; } = SystemColors.ControlText;

        public Font Font { get; set; } = SystemFonts.DefaultFont;

        public event Action Start;

        public event Action ParentSet;

        public event Action<IGraphics> Paint;

        public event Action<MouseEventArgs> MouseDown;

        public event Action<MouseEventArgs> MouseUp;

        public event Action<MouseEventArgs> MouseMove;

        public event Action<MouseEventArgs> MouseWheel;

        public event Action<KeyEventArgs> KeyDown;

        public event Action<KeyEventArgs> KeyUp;

        public event Action<KeyPressEventArgs> KeyPress;

        public void OnStart()
        {
            MouseDown += (s) => { };
            MouseUp += (s) => { };
            MouseMove += (s) => { };
            KeyDown += (s) => { };
            KeyUp += (s) => { };
            KeyPress += (s) => { };
            Paint += (s) => { };
            Start += () => { };
            ParentSet += () => { };

            Start();

            foreach (var control in Controls)
            {
                control.OnStart();
            }
        }

        public void OnPaint(IGraphics render)
        {
            Paint(render);

            foreach (var c in Controls.ToList())
            {
                var r = render;

                r.X = c.X;

                r.Y = c.Y;
                
                c.OnPaint(r);
                
                if(c.Parent != this)
                {
                    c.Parent = this;
                }
            }
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            MouseDown(e);
            SelectedControl = null;
            
            foreach(var c in Controls)
            {
                c.Selected = false;
            }

            foreach(var c in Controls)
            {
                if(c.X < e.X && c.Y < e.Y && c.X + c.Width > e.X && c.Y + c.Height > e.Y)
                {
                    SelectedControl = c;
                    c.Selected = true;
                    Selected = false;
                }
            }

            if (SelectedControl != null)
            {
                SelectedControl.OnMouseDown(new MouseEventArgs(e.Button, e.Clicks,
                    e.X - SelectedControl.X, e.Y - SelectedControl.Y, e.Delta));
            }
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            MouseUp(e);

            if (SelectedControl != null)
            {
                SelectedControl.OnMouseUp(new MouseEventArgs(e.Button, e.Clicks,
                    e.X - SelectedControl.X, e.Y - SelectedControl.Y, e.Delta));
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            MouseMove(e);

            foreach (var c in Controls)
            {
                if (c.X < e.X && c.Y < e.Y && c.X + c.Width > e.X && c.Y + c.Height > e.Y)
                {
                    c.OnMouseMove(new MouseEventArgs(e.Button, e.Clicks, e.X - c.X, e.Y - c.Y, e.Delta));
                }
            }
        }

        public void OnMouseWheel(MouseEventArgs e)
        {
            MouseWheel(e);

            foreach (var c in Controls)
            {
                if (c.X < e.X && c.Y < e.Y && c.X + c.Width > e.X && c.Y + c.Height > e.Y)
                {
                    c.OnMouseWheel(new MouseEventArgs(e.Button, e.Clicks, e.X - c.X, e.Y - c.Y, e.Delta));
                }
            }
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            KeyDown(e);

            if (SelectedControl != null)
            {
                SelectedControl.OnKeyDown(e);
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            KeyUp(e);

            if (SelectedControl != null)
            {
                SelectedControl.OnKeyUp(e);
            }
        }

        public void OnKeyPress(KeyPressEventArgs e)
        {
            KeyPress(e);

            if (SelectedControl != null)
            {
                SelectedControl.OnKeyPress(e);
            }
        }
    }
}
