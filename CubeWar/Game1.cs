using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle Fenetre;
        GameObject Gentil;
        GameObject[] Michant;
        GameObject[] Projectile2;
        GameObject[] Projectile;
        GameObject Explode;
        int NbMichant = 0;
        int NbProjectile2 = 0;
        int NbProjectile = 0;
        public int MaxMichant = 5;
        public int MaxProjectile2 = 5;
        public int MaxProjectile = 5;
        Texture2D Back;
        SpriteFont Gg;
        Random d = new Random();
        float TimeOfDeath = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            graphics.ToggleFullScreen();

            Michant = new GameObject[MaxMichant];
            Projectile2 = new GameObject[MaxProjectile2];
            Projectile = new GameObject[MaxProjectile];

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Gg = Content.Load<SpriteFont>("Font");
            Back = Content.Load<Texture2D>("Back.png");

            Fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;
            Fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;

            Gentil = new GameObject();
            Gentil.Alive = true;
            Gentil.vitesse = 10;
            Gentil.sprite = Content.Load<Texture2D>("Gentil.png");
            Gentil.position.X = Fenetre.Width / 2;
            Gentil.position.Y = Fenetre.Height / 2;

            for (int i = 0; i < Michant.Length; i++)
            {
                Michant[i] = new GameObject();
                Michant[i].Alive = false;
                Michant[i].sprite = Content.Load<Texture2D>("Michant.png");
                Michant[i].vitesse = 10;
                Michant[i].position.X = Fenetre.Width / 4;
                Michant[i].position.Y = Fenetre.Height / 4;
                Michant[i].direction.X = d.Next(-10, 10);
                Michant[i].direction.Y = d.Next(-10, 10);

                Projectile2[i] = new GameObject();
                Projectile2[i].Alive = true;
                Projectile2[i].sprite = Content.Load<Texture2D>("Projectile.png");
                Projectile2[i].vitesse = 20;
                Projectile2[i].position.X = Michant[i].position.X;
                Projectile2[i].position.Y = Michant[i].position.Y;

                Projectile[i] = new GameObject();
                Projectile[i].Alive = false;
                Projectile[i].sprite = Content.Load<Texture2D>("Projectile.png");
                Projectile[i].vitesse = 20;
                Projectile[i].position.X = Gentil.position.X;
                Projectile[i].position.Y = Gentil.position.Y;
            }

            Explode = new GameObject();
            Explode.Alive = false;
            Explode.sprite = Content.Load<Texture2D>("Explode.png");
            Explode.vitesse = 0;


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.D) && Gentil.Alive == true)
            {
                Gentil.position.X += Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) && Gentil.Alive == true)
            {
                Gentil.position.Y -= Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && Gentil.Alive == true)
            {
                Gentil.position.X -= Gentil.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && Gentil.Alive == true)
            {
                Gentil.position.Y -= Gentil.vitesse;
            }


            UpdateGentil();
            UpdateMichant();
            UpdateProjectile();
            UpdateProjectile2();
            base.Update(gameTime);

        }
            // TODO: Add your update logic here

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
        public void UpdateMichant()
        {
            if (NbMichant < TimeOfDeath && NbMichant < MaxMichant)
            {
                Michant[NbMichant].Alive = true;
                Michant[NbMichant].position.X = 0;
                Michant[NbMichant].position.Y = 0;
                NbMichant++;
            }
            for (int i = 0; i < NbMichant; i++)
            {
                if (Michant[i].GetRect().Intersects(Projectile[i].GetRect()))
                {
                    Michant[i].Alive = false;
                }
                Michant[i].position.X += Michant[i].vitesse;
                Michant[i].position.Y += Michant[i].vitesse;

            }

        }
        public void UpdateProjectile()
        {

        }
        public void UpdateProjectile2()
        {

        }


        

    
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
