using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    class Bullet: GameObject
    {
        public Bullet(Point Position, Size Size) : base(Position, new Point(), Size) { }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.Orange, new Rectangle(_Position, _Size));
        }

        public override void Update()
        {
            _Position.X += 3;
        }
    }
}
