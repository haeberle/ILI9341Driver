﻿namespace ILI9341Driver.Fonts
{
    public abstract class Font
    {
        public abstract byte SpaceWidth { get; }
        public abstract FontCharacter GetFontData(char character);
    }
}
