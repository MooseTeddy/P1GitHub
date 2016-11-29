using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
    class GameObject
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool Alive;
        public Vector2 direction;


        public Rectangle rectCollision = new Rectangle();
        public Rectangle GetRect()
        {
            rectCollision.X = (int)this.position.X;
            rectCollision.Y = (int)this.position.Y;
            rectCollision.Width = this.sprite.Width;
            rectCollision.Height = this.sprite.Height;

            return rectCollision;
        }

    }
}