using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarForms
{
    public class Form : Control
    {
        public bool ShowTopPanel = true;

        public bool IsRounded = false;

        public bool Movable = true;

        public bool Sizable = true;

        public int MinWidth = 100;

        public int MinHeight = 100;

        private Point? lastMouseDownPoint = null;

        public Form()
        {
            Paint += Form_Paint;
            MouseUp += Form_MouseUp;
            ParentSet += Form_ParentSet;
            
            
            Width = 500;
            Height = 250;
            BackColor = SystemColors.Window;
            BorderColor = SystemColors.WindowFrame;
            SelectColor = SystemColors.ActiveBorder;
            TextColor = SystemColors.WindowText;
            Font = SystemFonts.CaptionFont;
            TextX = 5;
            TextY = 5;
        }

        private void Form_MouseUp(System.Windows.Forms.MouseEventArgs obj)
        {
            lastMouseDownPoint = null;
        }

        private void Form_ParentSet()
        {
            X = (Parent.Width - Width) / 2;
            Y = (Parent.Height - Height) / 2;

            Parent.MouseMove += Parent_MouseMove;
        }

        private void Parent_MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (lastMouseDownPoint != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (ShowTopPanel)
                    {
                        if (Movable)
                        {
                            if (((Point)lastMouseDownPoint).Y > Y - 25 &&
                                ((Point)lastMouseDownPoint).X > X &&
                                ((Point)lastMouseDownPoint).Y < Y &&
                                ((Point)lastMouseDownPoint).X < X + Width)
                            {
                                X -= ((Point)lastMouseDownPoint).X - e.X;

                                Y -= ((Point)lastMouseDownPoint).Y - e.Y;
                            }
                        }
                    }

                    if (Sizable)
                    {
                        var p = (Point)lastMouseDownPoint;

                        if (p.X > X + Width - 2 && p.X < X + Width + 2)
                        {
                            if (Width - (p.X - e.X) >= MinWidth)
                            {
                                Width -= p.X - e.X;
                            }
                        }

                        if (p.Y > Y + Height - 2 && p.Y < Y + Height + 2)
                        {
                            if (Height - (p.Y - e.Y) >= MinHeight)
                            {
                                Height -= p.Y - e.Y;
                            }
                        }
                    }
                }
            }

            lastMouseDownPoint = new Point(e.X, e.Y);
        }

        private void Form_Paint(IGraphics obj)
        {
            if (IsRounded)
            {
                obj.FillRoundedRectangle(0, 0, Width, Height, 5, BackColor);

                var c = SelectColor;

                if (Selected)
                {
                    obj.DrawRoundedRectangle(0, 0, Width, Height, 5, SelectColor, 3);
                }
                else
                {
                    obj.DrawRoundedRectangle(0, 0, Width, Height, 5, BorderColor, 3);

                    c = BorderColor;
                }

                if (ShowTopPanel)
                {
                    obj.FillRoundedRectangle(-2, -26, Width + 4, 32, 5, SystemColors.ActiveCaption);
                    obj.FillRectangle(2, 0, Width - 4, 6, BackColor);
                    obj.DrawLine(0, 0, 0, 5, c, 3); ;
                    obj.DrawLine(Width, 0, Width, 5, c, 3);
                    obj.DrawText(TextX, TextY - 25, Text,
                        Font, Width, Height, TextColor);
                }
            }

            else
            {
                obj.FillRectangle(0, 0, Width, Height, BackColor);            

                if (Selected)
                {
                    obj.DrawRectangle(0, 0, Width, Height, SelectColor, 3);
                }
                else
                {
                    obj.DrawRectangle(0, 0, Width, Height, BorderColor, 3);
                }

                if (ShowTopPanel)
                {
                    obj.FillRectangle(-2, -25, Width + 4, 26, SystemColors.ActiveCaption);
                    obj.DrawText(TextX, TextY - 25, Text,
                        Font, Width, Height, TextColor);
                }
            }
        }
    }
}
