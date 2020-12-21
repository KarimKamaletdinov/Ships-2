using System.Drawing;

namespace KarForms
{
    public class Button : Control
    {
        public Button()
        {
            Paint += Button_Paint;

            BackColor = SystemColors.ButtonFace;
            BorderColor = SystemColors.ButtonShadow;
            SelectColor = SystemColors.ButtonHighlight;

            Width = 150;
            Height = 50;
        }


        private void Button_Paint(IGraphics obj)
        {
            var c = BackColor;

            if (Selected)
            {
                c = SelectColor;
            }

            obj.FillRectangle(0, 0, Width, Height, c);
            obj.DrawRectangle(0, 0, Width, Height, BorderColor);
            obj.DrawText((int)(Width - Text.Length * Font.Size) / 2, 
                (int)((Height - Font.Size) / 2), Text,
                Font, Width, Height, TextColor);
        }
    }
}