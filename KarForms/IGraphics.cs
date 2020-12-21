using System.Drawing;

namespace KarForms
{
    public interface IGraphics
    {
        int X { get; set; }

        int Y { get; set; }
        
        void DrawLine(int x1, int y1, int x2, int y2, Color color, float s = 1);
        
        void DrawRectangle(int x, int y, int width, int height, Color color, float s = 1);

        void FillRectangle(int x, int y, int width, int height, Color color);

        void DrawRoundedRectangle(int x, int y, int width, int height, int round,
            Color color, float s = 1);

        void FillRoundedRectangle(int x, int y, int width, int height, int round, Color color);

        void DrawImage(int x, int y, Bitmap image);

        void DrawTransparentImage(int x, int y, Bitmap image);

        void DrawText(int x, int y, int size, string text, Color color);

        void DrawText(int x, int y, string text, Font font, int width, int height, Color color);
    }
}