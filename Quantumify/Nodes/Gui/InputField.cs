using System.Numerics;
using Raylib_cs;
using System.Text;

using Timer = Quantumify.Nodes.Timer;

namespace Quantumify.Gui;

public class InputField : GuiElement
{
    
    
    public StringBuilder Text;
    public int MaxInputLength;
    
    int _startSelection;
    int _endSelection;

    private int _cursor;
    private bool _showCursor;
    private Timer _timer;
    
    private bool _selection;
    
    public string GetSelectedText()
    {
        return Text.ToString().Substring(_startSelection, _endSelection - _startSelection);
    }
    
    LabelSettings labelSettings;
    public InputField(float x, float y, float width, float height,int maxInputLength = 20, LabelSettings labelSettings = null)
    {
        
        Position = new Vector2(x, y);
        Size = new Vector2(width, height);
        Text = new StringBuilder();

        MaxInputLength = maxInputLength;
        Text = new StringBuilder();
        
        _timer = new Timer();
        _timer.Time = 1f;
        _timer.Repeat = true;
        _timer.OnTimerRanOut += () => _showCursor = !_showCursor;
        _timer.Start();
        
        
        
        if (labelSettings == null)
        {
            this.labelSettings = new LabelSettings()
            {
                FontSize = 25
            };
        }
    }


    public unsafe override void Update()
    {
        base.Update();
        
        int key = Raylib.GetKeyPressed();
        
        if (key != 0)
        {
            KeyboardKey k = (KeyboardKey)key;
         
            
            
            if (_cursor > Text.Length)
            {
                _cursor = Text.Length;
            }
            
            if (_cursor < 0)
            {
                _cursor = 0;
            }
            
            if(k == KeyboardKey.Delete||k == KeyboardKey.Backspace)
            {
                if (Text.Length > 0 && _startSelection > -1 && _endSelection > -1)
                {
                    int max = Math.Max(_startSelection, _endSelection);
                    int min = Math.Min(_startSelection, _endSelection);
                    
                    Text.Remove(min, max-min);
                    
                    _cursor = min;
                    _startSelection = -1;
                    _endSelection = -1;
                    return;
                }
            }
            

            if (k == KeyboardKey.Delete)
            {
                return;
            }
            if(k == KeyboardKey.Backspace)
            {
                RemoveCharAtCursor();
                return;
            }


            if (Raylib.IsKeyDown(KeyboardKey.LeftControl) || Raylib.IsKeyDown(KeyboardKey.RightControl))
            {
                if (k == KeyboardKey.A)
                {
                    _startSelection = 0;
                    _endSelection = Text.Length;
                }
                
                if (k == KeyboardKey.C)
                { 
                    if (_startSelection > -1 && _endSelection > -1)
                    {
                        string selectedText = Text.ToString().Substring(_startSelection, _endSelection - _startSelection);
                        Raylib.SetClipboardText(selectedText);
                    }
                }
                
                if (k == KeyboardKey.V)
                {
                    string clipboardText = PtrToString(Raylib.GetClipboardText());
                    if (clipboardText != null)
                    {
                        Text.Append(clipboardText);
                    }
                }
                return;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                this._cursor--;
                return;
            }
            
            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                this._cursor++;
                return;
            }
            
            
            if(k == KeyboardKey.LeftShift||k == KeyboardKey.RightShift)
            {
                return;
            }
            
            if (Text.Length < MaxInputLength)
            {
                string c = ((char)key).ToString();
                
                
                if(Raylib.IsKeyDown(KeyboardKey.LeftShift)||Raylib.IsKeyDown(KeyboardKey.RightShift))
                {
                    c = c.ToUpper();
                }
                else
                {
                    c = c.ToLower();
                }
                
                Text.Insert(_cursor,c);
                this._cursor++;
            }
        }

        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            
            bool collide = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), GetRectangle());

            if (!collide && _startSelection != -1 && _endSelection != -1&&!_selection)
            {
                _startSelection = -1;
                _endSelection = -1;
            }
            
            if (collide&&!_selection)
            {
                _startSelection = KeyCollide();
                _selection = true;
            }

            if (_selection)
            {
                _endSelection = KeyCollide();
            }
        }
        
        if (Raylib.IsMouseButtonReleased(MouseButton.Left)&&_selection)
        {
            _selection = false;
            _endSelection = KeyCollide();
        }
    }


    private unsafe string PtrToString(sbyte* ptr)
    {
        if (ptr == null) return null;

        // Find the length of the string
        int len = 0;
        while (ptr[len] != 0) len++;

        // Convert sbyte* to byte[] and then to string
        byte[] byteArray = new byte[len];
        for (int i = 0; i < len; i++)
        {
            byteArray[i] = (byte)ptr[i];
        }

        return Encoding.UTF8.GetString(byteArray);
    }

    private void RemoveCharAtCursor()
    {
        
        if (Text.Length > 0&& this._cursor > 0)
        {
            Text.Remove(this._cursor-1, 1);
            this._cursor--;
        }
    }
    
    public void DrawSelection()
    {
        int higherSelection = Math.Max(_startSelection, _endSelection);
        int lowerSelection = Math.Min(_startSelection, _endSelection);
        
        Vector2 position = Position;
        int i = 0;
        
        Vector2 first = position;
        Vector2 second = position;
        
        
        Rand.SetSeed(0);
        foreach (Char character in Text.ToString())
        {
            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), character.ToString(), labelSettings.FontSize,labelSettings.Spacing);


            Vector2 label = new Vector2(labelSettings.Spacing, 0)*2;
            
            Rectangle finalRect = new Rectangle(position-label, textSize+label);
            
            //Raylib.DrawRectangleRec(finalRect, Engine.RandomColor());
            
            if (i == lowerSelection)
            {
                first = position;
            }

            
            position += new Vector2(textSize.X+labelSettings.Spacing*2,0);
            i++;
            
            
            if (i == higherSelection)
            {
                second = position;
            }
        }
        
        
        
        Raylib.DrawRectangleRec(new Rectangle(new Vector2(first.X,position.Y), new Vector2(second.X - first.X,labelSettings.FontSize)), Color.Blue);
    }
    
    public int KeyCollide()
    {
        Vector2 position = Position;
        int i = 0;

        if (Raylib.GetMousePosition().X < position.X)
        {
            return 0;
        }
    
        foreach (Char character in Text.ToString())
        {
            // Measure the size of the current character
            Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), character.ToString(), labelSettings.FontSize, labelSettings.Spacing);
        
            // Adjust the rectangle for the character
            Vector2 label = new Vector2(labelSettings.Spacing, 0) * 2;
            Rectangle finalRect = new Rectangle(position - label, textSize + label);
        
            Vector2 mouse = Raylib.GetMousePosition();
        
            if (Raylib.CheckCollisionPointRec(mouse, finalRect))
            {
                return i;
            }
        
            // Update the position to the next character's position
            position += new Vector2(textSize.X + labelSettings.Spacing * 2, 0);
            i++;
        }

        if (Raylib.GetMousePosition().X > position.X)
        {
            return Text.Length;
        }
    
        return -1;
    }

    public override void Overlay()
    {
        base.Overlay();
        Rectangle rect = GetRectangle();

        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, (int)rect.Width, (int)rect.Height, Color.LightGray);

        
        
        string text = Text.ToString();

        if (this._showCursor)
        {
            if (this._cursor > text.Length)
            {
                this._cursor = text.Length;
            }
            
            text = text.Insert(this._cursor, "|");
        }

        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, labelSettings.FontSize, labelSettings.Spacing) * new Vector2(1, 1.35f) + new Vector2(8, 0);
        
        this.Size = new Vector2(Size.X, textSize.Y);
        
        Color color = Color.Black;
        
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rect))
        {
            color = Color.DarkBlue;
        }
        
        DrawSelection();
        
        
        Raylib.DrawText(text, (int)Position.X, (int)Position.Y, labelSettings.FontSize, labelSettings.Color);
        Raylib.DrawRectangleLinesEx(rect, 1, color);
            
    }
}
