using KaganoEngine.Nodes;
using Raylib_cs;
using System.Net.Http.Headers;
using System.Numerics;

namespace KaganoEngine.Components
{
    public class Collision : Component
    {
        private Shape shape;

        public Collision(Node node, Shape shape) : base(node)
        {
            this.shape = shape;
        }

        public Rectangle NodeToRectangle(Node node)
        {
            int[] pos = node.VectorToIntArray(node.Position);
            int[] size = node.VectorToIntArray(node.Size);

            return new Rectangle(pos[0], pos[1], new Vector2(size[0], size[1]));
        }

        public override void Update()
        {
            Logger.Error("Collisions are not supported yet");
        }
    }
}