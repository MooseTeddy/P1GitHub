using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle Fenetre;
        GameObject Gentil;
        GameObject[] Michant;
        GameObject[] Projectile2;
        GameObject Projectile;
        int NbMichant = 0;
        int NbProjectile2 = 0;
        float temps;
        float tempsFinal;
        public int MaxMichant = 5;
        public int MaxProjectile2 = 5;
        Vector2 max;
        GameObject Explode;

        Texture2D Back;
        SpriteFont font;

        Random rand = new Random();

        public object Break { get; private set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            this.graphics.ApplyChanges();
            this.Window.Position = new Point(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("Font");
            Back = Content.Load<Texture2D>("Back.png");

            font = Content.Load<SpriteFont>("Font");

            Fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            Fenetre.Width = graphics.GraphicsDevice.Viewport.Width;
            Fenetre.Height = graphics.GraphicsDevice.Viewport.Height;

            Gentil = new GameObject();
            Gentil.Alive = true;
            Gentil.vitesse = 15;
            Gentil.sprite = Content.Load<Texture2D>("Gentil.png");
            Gentil.position = Gentil.sprite.Bounds;
            Gentil.position.Offset((Fenetre.Width / 4), (Fenetre.Height / 4));

            this.Michant = new GameObject[MaxMichant];
            this.Projectile2 = new GameObject[MaxProjectile2];


            for (int i = 0; i < Michant.Length; i++)
            {
                Michant[i] = new GameObject();
                Michant[i].Alive = false;
                Michant[i].sprite = Content.Load<Texture2D>("Michant.png");
                Michant[i].vitesse = 10;
                Michant[i].position = Michant[i].sprite.Bounds;
                Michant[i].position.X = Fenetre.Width;
                Michant[i].position.Y = Fenetre.Height;
                Michant[i].direction.X = rand.Next(-10, 10);
                Michant[i].direction.Y = rand.Next(-10, 10);
            }

            for (int i = 0; i < Projectile2.Length; i++)
            {
                Projectile2[i] = new GameObject();
                Projectile2[i].Alive = false;
                Projectile2[i].vitesse = 20;
                Projectile2[i].position = Michant[i].position;
                Projectile2[i].sprite = Content.Load<Texture2D>("Projectile.png");
                Projectile2[i].position = Projectile2[i].sprite.Bounds;
            }

            Projectile = new GameObject();
            Projectile.Alive = true;
            Projectile.vitesse = 20;
            Projectile.position = Gentil.position;
            Projectile.sprite = Content.Load<Texture2D>("Projectile.png");
            Projectile.position = Projectile.sprite.Bounds;

            Explode = new GameObject();
            Explode.sprite = Content.Load<Texture2D>("Explode.png");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Gentil.position.X += Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Gentil.position.X -= Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Gentil.position.Y += Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Gentil.position.Y -= Gentil.vitesse;
            }

            temps += Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            UpdateMichants(gameTime);
            UpdateGentil();
            UpdateProjectile2(gameTime);
            UpdateProjectile();
            base.Update(gameTime);
        }

        public void UpdateGentil()
        {
            if (Gentil.position.X > Fenetre.Width - Gentil.position.Width)
            {
                Gentil.position.X = Fenetre.Width - Gentil.position.Width;
            }

            if (Gentil.position.Y > Fenetre.Height - Gentil.position.Height)
            {
                Gentil.position.Y = Fenetre.Height - Gentil.position.Height;
            }

            if (Gentil.position.X < 0)
            {
                Gentil.position.X = 0;
            }

            if (Gentil.position.Y < 0)
            {
                Gentil.position.Y = 0;
            }
        }
        public void UpdateProjectile2(GameTime gameTime)
        {
            for (int i = 0; i < NbMichant; i++)
            {
                if (NbProjectile2 < gameTime.TotalGameTime.Seconds && NbProjectile2 < MaxProjectile2)
                {
                    Projectile2[i].Alive = true;
                    NbProjectile2++;
                }

                if (Gentil.position.Intersects(Projectile2[i].position))
                {
                    tempsFinal = temps;
                    Gentil.Alive = false;
                }

                Projectile2[i].position.Y += (int)Projectile2[i].vitesse;

                if (Projectile2[i].Alive == false)
                {
                    Projectile2[i].Alive = true;
                    Projectile2[i].position = Michant[i].position;
                }

                if (Michant[i].position.Y > max.X || Michant[i].position.Y < Fenetre.Top)
                {
                    Projectile2[i].Alive = false;
                }

            }
        }
        public void UpdateMichants(GameTime gameTime)
        {
            if (NbMichant < gameTime.TotalGameTime.Seconds && NbMichant < MaxMichant)
            {
                Michant[NbMichant].Alive = true;
                Michant[NbMichant].position.X = 0;
                Michant[NbMichant].position.Y = 0;
                NbMichant++;
            }
            for (int i = 0; i < NbMichant; i++)
            {
                if (Gentil.position.Intersects(Michant[i].position))
                {
                    tempsFinal = temps;
                    Gentil.Alive = false;
                }

                Michant[i].position.X += (int)Michant[i].direction.X;
                Michant[i].position.Y += (int)Michant[i].direction.Y;

                if (Michant[i].Alive == false)
                {
                    Michant[i].Alive = true;

                    Michant[i].position.X = Fenetre.Width / 2;
                    Michant[i].position.Y = Fenetre.Height / 2;
                }

                if (Michant[i].position.Intersects(Projectile.position))
                {
                    Michant[i].Alive = false;
                }

                max.X = Fenetre.Width - Michant[i].sprite.Width;
                max.Y = Fenetre.Height - Michant[i].sprite.Height;

                if (Michant[i].position.X > max.X || Michant[i].position.X < Fenetre.Left)
                {
                    Michant[i].direction.X = -(Michant[i].direction.X);
                }
                if (Michant[i].position.Y > max.X || Michant[i].position.Y < Fenetre.Top)
                {
                    Michant[i].direction.Y = -(Michant[i].direction.Y);
                }

            }

        }
        public void UpdateProjectile()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (Gentil.Alive == true)
                {
                    Projectile.position = Gentil.position;
                    if (Projectile.position.Y < Fenetre.Top)
                    {
                        Projectile.position.Y = Gentil.position.Y + Gentil.sprite.Height;
                        Projectile.position.X = Gentil.position.X;
                    }
                }
                else
                {
                    Projectile.position.X = -700;
                    Projectile.position.Y = 0;
                }

            }
            Projectile.position.Y -= Projectile.vitesse;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.Draw(Back, Fenetre, Color.White);

            if (Projectile.Alive == true)
            {
                spriteBatch.Draw(Projectile.sprite, Projectile.position, Color.White);
            }

            for (int i = 0; i < MaxMichant; i++)
            {
                {
                    if (Projectile2[i].Alive == true)
                    {
                        spriteBatch.Draw(Projectile2[i].sprite, Projectile2[i].position, Color.White);
                    }
                }
            }

            if (Gentil.Alive == true)
            {
                spriteBatch.Draw(Gentil.sprite, Gentil.position, Color.White);
            }
            else
            {
              spriteBatch.Draw(Explode.sprite, Gentil.position, Color.White);

               spriteBatch.DrawString(font, "Time: " + Convert.ToInt16(tempsFinal).ToString(), new Vector2(100, 100), Color.Black);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            for (int i = 0; i < Michant.Length; i++)
            {
                if (Michant[i].Alive)
                {
                    spriteBatch.Draw(Michant[i].sprite, Michant[i].position, Color.White);
                }
            }




            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
