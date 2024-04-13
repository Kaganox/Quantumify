using KaganoEngine.Nodes;
using Raylib_cs;
using System.Numerics;
using KaganoEngine.Scenes;
using System.Net.Http.Headers;
using Box2DX.Collision;
using Box2DX.Dynamics;
using Box2DX.Common;

namespace KaganoEngine.Components
{
    public class Collision2D : Component
    {
        public Shape shape { get; private set; }

        public Collision2D(Node node, Shape shape) : base(node)
        {
            this.shape = shape;
            /*World world = new World(new Vec2(0, 0),0);
            
            BodyDef bodyDef = new BodyDef();
            bodyDef.Position.Set(node.Position.X, node.Position.Y);
            Body body = world.CreateBody(bodyDef);*/
        }

        public override void Update()
        {

            SceneManager.activeScene?.components.ForEach(component =>
            {
                if(shape!=Shape.Rectangle&&shape!=Shape.Circle)
                {
                    Logger.Error($"{shape.ToString()} Not supported yet");
                }

                if(component is Collision2D collision2D)
                {
                    if (CheckRects(collision2D)||CheckCircles(collision2D)||
                        CheckCircleAndRect(collision2D,this)||CheckCircleAndRect(this,collision2D))
                    {
                        node.Collide(collision2D.node);
                    }
                }
            });
            Logger.Error("Collisions are not supported yet");
        }


        public bool CheckRects(Collision2D target)
        {
            return shape==Shape.Rectangle && target.shape==Shape.Rectangle &&
                   Raylib.CheckCollisionRecs(NodeToRectangle(node), NodeToRectangle(target.node));
        }

        public bool CheckCircles(Collision2D target)
        {
            Tuple<Vector2, float> circle = NodeToCircle(node);
            Tuple<Vector2, float> targetCircle = NodeToCircle(target.node);
            return shape == Shape.Circle && target.shape == Shape.Circle &&
                   Raylib.CheckCollisionCircles(circle.Item1, circle.Item2, targetCircle.Item1, targetCircle.Item2);
        }

        public bool CheckCircleAndRect(Collision2D target,Collision2D node)
        {
            Tuple<Vector2, float> circle = NodeToCircle(node.node);

            return shape == Shape.Circle && target.shape == Shape.Rectangle &&
                   Raylib.CheckCollisionCircleRec(circle.Item1, circle.Item2, NodeToRectangle(target.node));
        }




        public Rectangle NodeToRectangle(Node node)
        {
            int[] pos = node.VectorToIntArray(node.Position);
            int[] size = node.VectorToIntArray(node.Size);

            return new Rectangle(pos[0], pos[1], new Vector2(size[0], size[1]));
        }

        public Tuple<Vector2, float> NodeToCircle(Node node)
        {
            int[] pos = node.VectorToIntArray(node.Position);
            Vector2 center = new Vector2(node.Scale.X, node.Scale.Y)+new Vector2(pos[0], pos[1]);
            float radius = node.Scale.X/2;
            return new Tuple<Vector2, float>(center, radius);
        }
        /*
         * #CheckCollisionPointRec
            #CheckCollisionPointCircle
            #CheckCollisionPointTriangle
            #CheckCollisionPointPoly
            CheckCollisionLines
            CheckCollisionPointLine
         */
    }
}