using Raylib_cs;
using System.Numerics;

namespace KaganoEngine.Nodes
{
    public class Node3D : Node
    {
        public Model? Model;
        public Texture2D? Texture;
        public Color Color;
        public float RotationAxis;


        public Vector3 Rotation;
        
        public Node3D() : base()
        {
            Color = Color.White;
            RotationAxis = 0;
            //Rotation = new Vector3(0, 0, 0);
        }

        unsafe public void SetMaterialTexture()
        {
            if (Model != null && Texture != null)
            {
                for (int i = 0; i < Model.Value.MaterialCount; i++)
                {
                    Raylib.SetMaterialTexture(ref Model.Value.Materials[i], MaterialMapIndex.Albedo, Texture.Value);
                }
            }
        }

        public override void Draw()
        {
            if (Model != null)
            {
                Raylib.DrawModelEx(Model.Value, Position, Rotation, RotationAxis, Scale, Color);
            }
        }
    }
}
