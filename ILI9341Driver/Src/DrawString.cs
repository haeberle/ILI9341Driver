namespace ILI9341Driver
{
    public partial class ILI9341
    {        
        public void DrawString(int x, int y, string s, Color565 color, Font font)
        {
            var currentX = x;

            // Needed because string in nanoframework doesn't implement IEnumerable
            var schars = s.ToCharArray();

            foreach (var c in schars)
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
                        DrawChar(currentX, y, color, character);
                    }
                    currentX += character.Width + character.Space;
                }
            }
        }

        public void DrawString(int x, int y, string s, Color565 foreground, Color565 background, Font font)
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
                        DrawChar(currentX, y, foreground, background, character);
                    }
                    currentX += character.Width + character.Space;
                }
            }
        }
    }
}

