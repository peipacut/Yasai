using System;

namespace Yasai.Graphics
{
    public interface IDrawable : IUpdate, ILoad, IGeometry
    {
        public bool Visible { get; set; }
        public void Draw(IntPtr renderer);
    }
}