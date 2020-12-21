using SharpDX.Direct2D1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarForms;
using SharpDX;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Ships_2
{
    public class Desctop : Control
    {
        private Dform _directXForm;

        private DXGraphics _graphics = new DXGraphics();

        public Desctop()
        {
            Start += start;

            var f = new TestForm();
            Controls.Add(f);

            Width = 1300;
            Height = 700;
        }

        private void start()
        {
            _directXForm = new Dform();

            _directXForm.Paint += () =>
            {
                _graphics.target = _directXForm.RenderTarget2D;
                OnPaint(_graphics);
            };

            _directXForm._KeyDown += OnKeyDown;

            _directXForm._KeyUp += OnKeyUp;

            _directXForm._KeyPress += OnKeyPress;

            _directXForm._MouseDown += OnMouseDown;

            _directXForm._MouseUp += OnMouseUp;

            _directXForm._MouseWheel += OnMouseWheel;

            _directXForm._MouseMove += OnMouseMove;
        }

        public void Run()
        {
            _directXForm.Run();
        }
    }
}
