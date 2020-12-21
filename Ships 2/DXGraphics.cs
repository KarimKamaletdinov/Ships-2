using System;
using System.Drawing;
using KarForms;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using Bitmap = System.Drawing.Bitmap;

namespace Ships_2
{
    public class DXGraphics : IGraphics
    {
        public RenderTarget target;

        public int X { get; set; }
       
        public int Y { get; set; }

        public void DrawLine(int x1, int y1, int x2, int y2, Color color, float s = 1)
        {
            target.DrawLine(new RawVector2(X + x1, Y + y1), new RawVector2(X + x2, Y + y2), 
                new SolidColorBrush(target, parseFloatColor(color)), s);
        }

        public void DrawRectangle(int x, int y, int width, int height, Color color, float s = 1)
        {
            target.DrawRectangle(new RawRectangleF(X + x, Y + y, X + x + width, 
                    Y + y + height), new SolidColorBrush(target, parseFloatColor(color)), s);
        }

        public void FillRectangle(int x, int y, int width, int height, Color color)
        {
            target.FillRectangle(new RawRectangleF(X + x, Y + y, X + x + width,
                    Y + y + height), new SolidColorBrush(target, parseFloatColor(color)));
        }

        public void DrawRoundedRectangle(int x, int y, int width, int height, int radius, 
            Color color, float s = 1)
        {

            target.DrawRoundedRectangle(new RoundedRectangle()
            {
                Rect = new RawRectangleF(X + x, Y + y, X + x + width,
                    Y + y + height),
                RadiusX = radius,
                RadiusY = radius
            }, new SolidColorBrush(target, parseFloatColor(color)), s);

            
        }

        public void FillRoundedRectangle(int x, int y, int width, int height, int radius, Color color)
        {

            target.FillRoundedRectangle(new RoundedRectangle()
            {
                Rect = new RawRectangleF(X + x, Y + y, X + x + width,
                    Y + y + height),
                RadiusX = radius,
                RadiusY = radius
            }, new SolidColorBrush(target, parseFloatColor(color)));
        }

        public void DrawImage(int x, int y, Bitmap image)
        {
            for (int _x = 0; _x < image.Width; _x++)
            {
                for (int _y = 0; _y < image.Height; _y++)
                {
                    DrawRectangle(X + x + _x, Y + y + _y, 1, 1,
                        image.GetPixel(_x, _y));
                }
            }
        }

        public void DrawTransparentImage(int x, int y, Bitmap image)
        {
            for (int _x = 0; _x < image.Width; _x++)
            {
                for (int _y = 0; _y < image.Height; _y++)
                {
                    if (image.GetPixel(_x, _y) != Color.White)
                    {
                        DrawRectangle(X + x + _x, Y + y + _y, 1, 1,
                            image.GetPixel(_x, _y));
                    }
                }
            }
        }

        public void DrawText(int x, int y, int size, string text, Color color)
        {
            target.DrawText(text, new SharpDX.DirectWrite.TextFormat(
                new SharpDX.DirectWrite.Factory(), "arial", size), new RawRectangleF(
                    X + x, Y + y, X + x + 100000, Y + y + 100000), new SolidColorBrush(target,
                    parseColor(color)));
        }

        public void DrawText(int x, int y, string text, Font font, int width, int height, 
            Color color)
        {
            var style = SharpDX.DirectWrite.FontStyle.Normal;
            var weight = SharpDX.DirectWrite.FontWeight.Normal;

            if (font.Italic) style = SharpDX.DirectWrite.FontStyle.Italic;
            if (font.Bold) weight = SharpDX.DirectWrite.FontWeight.Bold;

            target.DrawText(text, new SharpDX.DirectWrite.TextFormat(
                new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared), font.Name,
                weight, style, font.Size), new RawRectangleF(
                    X + x, Y + y, X + x + width, Y + y + height), new SolidColorBrush(target,
                    parseColor(color)));
        }

        private static RawColor4 parseColor(Color color)
        {
            return new RawColor4(color.R, color.G, color.B, color.A * 2.56f);
        }

        private static RawColor4 parseFloatColor(Color color)
        {
            return new RawColor4(color.R / 256f, color.G / 256f, color.B / 256f, 
                color.A / 100f);
        }
    }
}