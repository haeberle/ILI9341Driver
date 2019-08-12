using ILI9341Driver.Fonts;
using System;
using System.Text;

namespace ILI9341Driver._18Bit
{
    public partial class ILI9341Bit18
    {
        public void DrawString(int x, int y, string s, Font font, Color666 foreground, Color666 background = Color666.Black )
        {
            var currentX = x;
            char[] chars = s.ToCharArray();

            foreach (char c in chars)
            {
                var character = font.GetFontData(c);

                if (c == '\n') //line feed
                {
                    y += character.Height;
                }
                else if (c == '\r') //carriage return
                {
                    currentX = x;
                }
                else
                {
                    if (currentX + character.Width > Width)
                    {
                        currentX = x; //start over at the left and go to a new line.
                        y += character.Height;
                    }
                    if (character.Data != null)
                    {
                        DrawChar(currentX, y, character, foreground, background);
                    }
                    currentX += character.Width + character.Space;
                }
            }
        }
    }
}
