using System.Numerics;
using Microsoft.VisualBasic;
using Raylib_cs;

namespace Quantumify.Gui;

public class Calendar : GuiElement
{
    
    
    private bool open;
    
    DateTime selectedDate,hoverDate;
    
    Button button, leftButton, rightButton;
    Label label;
    public Calendar()
    {
        selectedDate = DateTime.Now;
        hoverDate = DateTime.Now;
        
        button = new Button(new LabelSettings());
        selectedDate = DateTime.Now;
        button.OnClicked += () => open = !open;
        
        leftButton = new Button(new LabelSettings());
        leftButton.OnClicked += () =>
        {
            hoverDate = hoverDate.AddMonths(-1);
        };
        
        rightButton = new Button(new LabelSettings());
        rightButton.OnClicked += () =>
        {
            hoverDate = hoverDate.AddMonths(1);
        };
        
        label = new Label(new LabelSettings());
    }
    
    public override void Overlay()
    {
        base.Overlay();

        if (open)
        {
            Raylib.DrawRectangle((int)Position.X-5, (int)Position.Y-5,50*7+10,50*5+10,Color.Gray);
            Raylib.DrawRectangleLines((int)Position.X-5, (int)Position.Y-5,50*7+10,50*5+10,Color.Black);
            int days = DateTime.DaysInMonth(hoverDate.Year, hoverDate.Month);
            for (int i = 0; i < days; i++)
            {
                int x = i % 7 * 50 + (int)Position.X;
                int y = i / 7 * 50 + (int)Position.Y;

                
                Color color = Color.DarkGray;
                if (i == selectedDate.Day-1 && hoverDate.Month == selectedDate.Month &&
                    hoverDate.Year == selectedDate.Year)
                {
                    color = Color.Red;
                }
                
                Raylib.DrawRectangle((int)x, (int)y, 50, 50, color);
                Raylib.DrawText((i + 1).ToString(), x + 25, y + 25, 15, Color.Black);
                Raylib.DrawRectangleLines((int)x, (int)y, 50, 50, Color.Black);

                if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), new Rectangle(x, y, 50, 50)))
                {
                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        selectedDate = new DateTime(hoverDate.Year, hoverDate.Month, i+1);
                    }
                }
            }
        }
        
        button.Text = $"{selectedDate.Day}.{selectedDate.Month}.{selectedDate.Year}";
        button.Position = Position-new Vector2(0, 25);
        button.Size = Size;
        button.Scale = Scale;
            
        leftButton.Text = "<";
        leftButton.Position = Position + new Vector2(-14, -25);
        leftButton.Size = new Vector2(50, 50);
        leftButton.Scale = Scale;
            
        rightButton.Text = ">";
        rightButton.Position = Position + new Vector2(button.TextSize, -25);
        rightButton.Size = new Vector2(50, 50);
        rightButton.Scale = Scale;
            
        label.Text = $"{hoverDate.ToString("MMMM")} {hoverDate.Year}";
        label.Position = Position + new Vector2(120, -20);
        label.Size = Size;
        label.Scale = Scale;

    }

    public override void OnDestroy()
    {
        button.Destroy();
        leftButton.Destroy();
        rightButton.Destroy();
        label.Destroy();
        base.OnDestroy();
    }
}