using System.Numerics;
using LibNoise.Model;
using Raylib_cs;

namespace Quantumify.Gui;

public class RichTextLabel : GuiElement
{

    
    private List<RichTextSegment> segments;

    private Color _color;
    private int _fontSize;
    public bool HasBox;
    
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

        
        Rectangle rect = GetRectangle();
        if (HasBox)
        {
            int scale = 5;
            Raylib.DrawRectangleRec(rect, Color.DarkGray);
            Raylib.DrawRectangleLinesEx(new Rectangle(rect.Position-new Vector2(scale,scale),rect.Size+new Vector2(scale*2,scale*2)), scale, Color.Gray);
        }
        
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

        
        
    /*#if DEBUG
        Raylib.DrawRectangleLines((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height, Color.Red);
    #endif*/
    }

    public (int, int) RenderText(string text, int x, int y, int fontSize, Color color)
    {
        float maxX = Position.X + (Scale.X * Size.X);
        float maxY = Position.Y + (Scale.Y * Size.Y);

        // Initial boundary check
        if (y > maxY)
        {
            return (x, int.MaxValue); // Signal to stop rendering
        }

        string[] words = text.Split(' ');
        string line = "";
        int offsetX = x;
        bool newLine = false; // To track if a new line was added

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
                    Raylib.DrawText(line.TrimEnd(), offsetX, y, fontSize, color); // Trim trailing spaces
                    line = ""; // Reset the line
                    offsetX = (int)Position.X; // Reset offsetX to the initial position
                    y += fontSize; // Move down to the next line
                    newLine = true;

                    // If the new y position exceeds maxY, signal to stop rendering
                    if (y + fontSize > maxY) // Adjusted check to prevent rendering the last line below the box
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
                                if (y + fontSize > maxY)
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

        // Render the last line if any
        if (line.Length > 0)
        {
            if (!newLine && y + fontSize > maxY)
            {
                return (offsetX, int.MaxValue);
            }

            Raylib.DrawText(line.TrimEnd(), offsetX, y, fontSize, color);
            offsetX += Raylib.MeasureText(line.TrimEnd(), fontSize);
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
    
    public void Clear()
    {
        segments.Clear();
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