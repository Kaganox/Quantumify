using System.Numerics;
using LibNoise.Renderer;
using Quantumify.Nodes;
using Quantumify.Scenes;
using Quantumify.Windowing;
using Raylib_cs;
using Test;
using Color = Raylib_cs.Color;
using Image = Raylib_cs.Image;
using Timer = Quantumify.Nodes.Timer;

namespace Quantumify;

public class ProvinceGenerator : Node
{

    public static Image img;
    public static Texture2D texture;
    public static Dictionary<int, Image>? Provinzes;
    public static Dictionary<int, List<int>>? Neighbours;
    
    private Timer _timer;
    
    public static (Dictionary<Image,(int x,int y)> dic, List<int> color) Create(Image image)
    {
        Provinzes = new Dictionary<int, Image>();
        Neighbours = new Dictionary<int, List<int>>();
        img = image;
        
        List<int> colors = new List<int>();
        
        Dictionary<Image,(int x,int y)> finalProvinces = new Dictionary<Image, (int x, int y)>();
        Dictionary<int,List<(int x,int y)>> provinceMap = new Dictionary<int, List<(int x, int y)>>();

        for (int x = 0; x < image.Width; x++)
        {
            for (int y = 0; y < image.Height; y++)
            {
                Color color = Raylib.GetImageColor(image,x,y);
                
                int colorInt = Raylib.ColorToInt(color);


                
                List<int> neighbours;

                if (Neighbours.ContainsKey(colorInt))
                {
                    neighbours = Neighbours[colorInt];
                }
                else
                {
                    neighbours = new List<int>();
                }
                for (int neighbourX = -1; neighbourX < 2; neighbourX++)
                {
                    for (int neighbourY = -1; neighbourY < 2; neighbourY++)
                    {
                        if ((neighbourX == 0 && neighbourY == 0)||
                            (neighbourX+x<0 || neighbourX+x>=image.Width || neighbourY+y<0 || neighbourY+y>=image.Height))
                        {
                            continue;
                        }

                        int neighbourColor = Raylib.ColorToInt(Raylib.GetImageColor(image, neighbourX + x, neighbourY + y));
                        if (!neighbours.Contains(neighbourColor) && neighbourColor != colorInt)
                        {
                            neighbours.Add(neighbourColor);
                            
                        }
                    }
                }
                Neighbours[colorInt] = neighbours;

                if (provinceMap.ContainsKey(colorInt))
                {
                    provinceMap[colorInt].Add((x,y));
                }else
                {
                    provinceMap.Add(colorInt,new List<(int x,int y)>{(x,y)});
                }
            }
        }
        
        foreach (var province in provinceMap)
        {
            unsafe
            {
                int minX = int.MaxValue;
                int maxX = int.MinValue;
            
            
                int minY = int.MaxValue;
                int maxY = int.MinValue;
                foreach (var coords in province.Value)
                {
                    minX = Math.Min(minX,coords.x);
                    maxX = Math.Max(maxX,coords.x);
                
                    minY = Math.Min(minY,coords.y);
                    maxY = Math.Max(maxY,coords.y);
                }
                Image img = Raylib.GenImageColor(maxX-minX+1,maxY-minY+1,new Color(0,0,0,0));
                Raylib.ImageFormat(ref img,PixelFormat.UncompressedR8G8B8A8);

                foreach (var coords in province.Value)
                {

                    Raylib.ImageDrawPixel(ref img, coords.x - minX, coords.y - minY,Color.White);//Engine.IntToColor(province.Key));
                }
                Logger.Error($"Size: {maxX-minX+1},{maxY-minY+1}");
                Raylib.ExportImage(img,$"C:\\Users\\Mika\\source\\repos\\KaganoEngine\\Test\\content\\{province.Key}.png");
                //provinceImages.Add(image);
                
                finalProvinces.Add(img,(minX,minY));
                Provinzes.Add(province.Key,image);
                colors.Add(province.Key);
                

            }
        }
        texture = Raylib.LoadTextureFromImage(image);
        return (finalProvinces,colors);

    }
    
    public ProvinceGenerator()
    {
        _timer = new Timer();
        _timer.Time = 0.5f;
    }

    public override void Update()
    {
        base.Update();
        if (Raylib.IsMouseButtonDown(MouseButton.Left)&&!_timer.Running)
        {
            Vector2 vector2 = Raylib.GetMousePosition();//Raylib.GetWorldToScreen2D(Raylib.GetMousePosition(), SceneCamera.Camera.GetCamera2D());

            Vector2 camera = -SceneCamera.Camera.GetCamera2D().Target+new Vector2(400,240);

            vector2 -= camera;
            
            Color color = Raylib.GetImageColor(img,(int)vector2.X, (int)vector2.Y);
            int colorInt = Raylib.ColorToInt(color);
            if (Provinzes.ContainsKey(colorInt))
            {
                Image image = Provinzes[colorInt];

                //Raylib.DrawTexture(texture, (int)camera.X,(int)camera.Y, Color.White);
                foreach (var activeSceneNode in SceneManager.ActiveScene.Nodes)
                {
                    if (activeSceneNode is Provinze provinze)
                    {
                        if (provinze.Color == colorInt)
                        {
                            _timer.Start();
                            provinze.OnClick();
                            Logger.Warn("Clicked on: " + provinze.Color);
                        }
                    }
                }
            }
        }
    }


    public static List<Provinze>? FindPath(Provinze start, Provinze goal)
    {
        List<int>? path = FindPath(start.Color, goal.Color);
        if (path == null)
        {
            return null;
        }

        List<Provinze> list = new List<Provinze>();
        foreach (int provinceColor in path)
        {
            Provinze? provinze = Provinze.GetProvinze(provinceColor);
            if (provinze == null)
            {
                continue;
            }
            list.Add(provinze);
        }

        return list;
    }

    public static List<int>? FindPath(int start, int goal)
    {
        if (Neighbours == null || !Neighbours.ContainsKey(start) || !Neighbours.ContainsKey(goal))
            return null;

        var queue = new PriorityQueue<int, int>();
        var cameFrom = new Dictionary<int, int>();
        var costSoFar = new Dictionary<int, int>();

        queue.Enqueue(start, 0);
        cameFrom[start] = -1;
        costSoFar[start] = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (current == goal)
                return ReconstructPath(cameFrom, start, goal);

            foreach (var neighbor in Neighbours[current])
            {
                int newCost = costSoFar[current] + 1; // Assuming uniform cost (change if weighted)
                
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    costSoFar[neighbor] = newCost;
                    queue.Enqueue(neighbor, newCost);
                    cameFrom[neighbor] = current;
                }
            }
        }
        return null; // No path found
    }

    private static List<int> ReconstructPath(Dictionary<int, int> cameFrom, int start, int goal)
    {
        var path = new List<int>();
        int current = goal;

        while (current != -1)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
    }
}