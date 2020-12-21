using DirectXWinForms.Common;
using System;
using System.Windows.Forms;

namespace Ships_2
{
    class Dform : DirectXForm
    {
        public event Action Paint;

        public event Action<MouseEventArgs> _MouseDown;

        public event Action<MouseEventArgs> _MouseUp;

        public event Action<MouseEventArgs> _MouseMove;

        public event Action<MouseEventArgs> _MouseWheel;

        public event Action<KeyEventArgs> _KeyDown;

        public event Action<KeyEventArgs> _KeyUp;

        public event Action<KeyPressEventArgs> _KeyPress;

        protected override void Start()
        {
            Paint += () => { };

            _MouseDown += (s) => { };

            _MouseUp += (s) => { };

            _MouseMove += (s) => { };

            _MouseWheel += (s) => { };

            _KeyDown += (s) => { };

            _KeyUp += (s) => { };

            _KeyPress += (s) => { };
            
            ThisForm.FormBorderStyle = FormBorderStyle.None;
            ThisForm.WindowState = FormWindowState.Maximized;
            ThisForm.MouseWheel += (s, e) => _MouseWheel(e);
            ThisForm.KeyPress += (s, e) => _KeyPress(e);
        }

        protected override void OnRender()
        {
            base.OnRender();

            RenderTarget2D.Clear(null);

            Paint();
        }

        protected override void MouseDown(MouseEventArgs e)
        {
            _MouseDown(e);
        }

        protected override void MouseUp(MouseEventArgs e)
        {
            _MouseUp(e);
        }

        protected override void MouseMove(MouseEventArgs e)
        {
            _MouseMove(e);
        }

        protected override void KeyDown(KeyEventArgs e)
        {
            _KeyDown(e);
        }

        protected override void KeyUp(KeyEventArgs e)
        {
            _KeyUp(e);
        }
    }
}
