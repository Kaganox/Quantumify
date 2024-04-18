using Raylib_cs;
using System.Numerics;

namespace KaganoEngine.Nodes
{
    public class Node3D : Node
    {
        public Model? Model;
        public Texture2D? Texture;
        public Color Color;

        public Quaternion Rotation;
        
        public Node3D() : base()
        {
            Color = Color.White;
            Rotation = new Quaternion();
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

        public override unsafe void Draw()
        {
            if (Model != null)
            {
                Vector3 axis;
                float angle;
           
                Raymath.QuaternionToAxisAngle(this.Rotation, &axis, &angle);
                
                Raylib.DrawModelEx(Model.Value, GlobalPosition, axis, angle * Raylib.RAD2DEG, Scale, Color);
            }
        }
    }
}
