using System.Numerics;
using LibNoise.Model;
using Raylib_cs;

namespace Quantumify.Gui;

public class RichTextLabel : GuiElement
{

    
    private List<RichTextSegment> segments;

    private Color _color;
    private int _fontSize;
    
    public RichTextLabel()
    {
        
        Size = new Vector2(5, 50);
        _color = Color.Black;
        _fontSize = 14;
        segments = new List<RichTextSegment>();
    }

      public override void Overlay()
    {
        base.Overlay();

        float offsetX = Position.X;
        float y = Position.Y;
        bool stop = false;

        foreach (var segment in segments)
        {
            string[] lines = segment.Text.Split("\n");

            int index = 0;
            foreach (string line in lines)
            {
                
                if (index > 0)
                {
                    offsetX = Position.X;
                    y += segment.FontSize;
                }
                // If the vertical position exceeds maxY, stop rendering
                if (y > Position.Y + (Scale.Y * Size.Y))
                {
                    stop = true;
                    break;
                }

                var (nextOffsetX, nextY) = RenderText(line, (int)offsetX, (int)y, segment.FontSize, segment.Color);

                // Update the current position
                offsetX = nextOffsetX;
                y = nextY;

                // If the text exceeds the vertical boundary, stop rendering
                if (stop)
                    break;

                // Reset offsetX and move to the next line if necessary
                if (offsetX == Position.X)
                    y += segment.FontSize;

                index++;
            }

            if (stop)
                break;
        }

    #if DEBUG
        Rectangle rect = GetRectangle();
        Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, Color.Red);
    #endif
    }

public (int, int) RenderText(string text, int x, int y, int fontSize, Color color)
{
    float maxX = Position.X + (Scale.X * Size.X);
    float maxY = Position.Y + (Scale.Y * Size.Y);

    if (y > maxY)
    {
        return (x, int.MaxValue); // Signal to stop rendering
    }

    string[] words = text.Split(' ');
    string line = "";
    int offsetX = x;

    foreach (string word in words)
    {
        // Measure the width of the current line plus the new word
        int lineWidth = Raylib.MeasureText(line + word, fontSize);
        // If the line width exceeds maxX, render the current line and start a new one
        if (offsetX + lineWidth > maxX)
        {
            if (line.Length > 0)
            {
                // Render the current line if it is not empty
                Raylib.DrawText(line, offsetX, y, fontSize, color);
                line = ""; // Reset the line
                offsetX = (int)Position.X; // Reset offsetX to the initial position
                y += fontSize; // Move down to the next line

                // If the new y position exceeds maxY, signal to stop rendering
                if (y > maxY)
                {
                    return (offsetX, int.MaxValue);
                }
            }

            // Check if the word itself exceeds the maxX width
            if (Raylib.MeasureText(word, fontSize) > (maxX - Position.X))
            {
                int startIndex = 0;
                while (startIndex < word.Length)
                {
                    int i;
                    for (i = startIndex + 1; i <= word.Length; i++)
                    {
                        string subWord = word.Substring(startIndex, i - startIndex);
                        if (Raylib.MeasureText(subWord, fontSize) > (maxX - offsetX))
                        {
                            // Render the part of the word that fits
                            Raylib.DrawText(word.Substring(startIndex, i - startIndex - 1), offsetX, y, fontSize, color);
                            // Start a new line with the remaining part of the word
                            offsetX = (int)Position.X;
                            y += fontSize;

                            // If the new y position exceeds maxY, signal to stop rendering
                            if (y > maxY)
                            {
                                return (offsetX, int.MaxValue);
                            }

                            startIndex = i - 1; // Update startIndex for the next part of the word
                            break;
                        }
                    }

                    // If the whole remaining part of the word fits, render it and break the loop
                    if (i > word.Length)
                    {
                        
                        Raylib.DrawText(word.Substring(startIndex), offsetX, y, fontSize, color);
                        offsetX += Raylib.MeasureText(word.Substring(startIndex), fontSize);
                        
                        
                        if (y > maxY)
                        {
                            return (offsetX, int.MaxValue);
                        }
                        break;
                    }
                }
            }
            else
            {
                // Start the new line with the current word
                line = word + " ";
            }
        }
        else
        {
            line += word + " ";
        }
    }

    // Render the last line
    if (line.Length > 0)
    {
        Raylib.DrawText(line, offsetX, y, fontSize, color);
        offsetX += Raylib.MeasureText(line, fontSize);
        
        
        if (y > maxY)
        {
            return (offsetX, int.MaxValue);
        }
    }

    // Return the next position for further rendering
    return (offsetX, y);
}





    

    public RichTextLabel AppendText(string text)
    {
        segments.Add(new RichTextSegment(text, _color, _fontSize));
        return this;
    }

    public RichTextLabel SetColor(Color color)
    {
        _color = color;
        return this;
    }
    
    public RichTextLabel SetFontSize(int fontSize)
    {
        _fontSize = fontSize;
        return this;
    }


    public class RichTextSegment
    {
        public string Text { get; set; }
        public Color Color { get; set; }
        public int FontSize { get; set; }

        public RichTextSegment(string text, Color color, int fontSize)
        {
            Text = text;
            Color = color;
            FontSize = fontSize;
        }
    }
    
}