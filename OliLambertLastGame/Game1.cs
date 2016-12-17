using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace OliLambertLastGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameObject GeoMetro;
        GameObject[] Civic = new GameObject[10];
        GameObject[] Cone = new GameObject[1000];
        GameObject Road1;
        GameObject Road2;
        GameObject Play;
        GameObject GameOver;
        new Rectangle Window;
        bool Gamestate;
        bool MenuOver = false;
        Random Rand = new Random();
        Song Song;
        SoundEffect SonKnockout;
        SoundEffect SonDeath;
        SoundEffect SonTir;
        SoundEffectInstance DeathCivic;
        SoundEffectInstance DeathGeoMetro;
        SoundEffectInstance CasqueOrange;
        SpriteFont OCRA;
        double Kills = 0;
        double MaxKills = 0;



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
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            //this.graphics.ToggleFullScreen();
            Window = new Rectangle(0, 0, graphics.GraphicsDevice.DisplayMode.Width, graphics.GraphicsDevice.DisplayMode.Height);
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

            //Road1
            Road1 = new GameObject();
            Road1.sprite = Content.Load<Texture2D>("Road.jpg");
            Road1.position.X = 0;
            Road1.position.Y = 0;
            Road1.vitesse.X = -15;
            Road2 = new GameObject();
            Road2.sprite = Content.Load<Texture2D>("Road.jpg");
            Road2.position.X = 1920;
            Road2.position.Y = 0;
            Road2.vitesse.X = -15;

            //Play Load
            Play = new GameObject();
            Play.sprite = Content.Load<Texture2D>("Play.png");
            Play.IsAlive = true;

            //GameOver
            GameOver = new GameObject();
            GameOver.sprite = Content.Load<Texture2D>("GameOver.png");
            Play.IsAlive = true;

            //Civic Load
            for (int i = 0; i < Civic.Length; i++)
            {
                Civic[i] = new GameObject();
                Civic[i].IsAlive = true;
                Civic[i].position.X = Rand.Next(Window.Width, Window.Width * 2);
                Civic[i].position.Y = Rand.Next(Window.Top, Window.Height - 71);
                Civic[i].sprite = Content.Load<Texture2D>("Civic.png");
            }

            //GeoMetro
            GeoMetro = new GameObject();
            GeoMetro.IsAlive = true;
            GeoMetro.position.X = Window.Left + 128;
            GeoMetro.position.Y = Window.Center.Y;
            GeoMetro.sprite = Content.Load<Texture2D>("Geo.png");

            //Cone
            for (int i = 0; i < Cone.Length; i++)
            {
                Cone[i] = new GameObject();
                Cone[i].IsAlive = false;
                Cone[i].sprite = Content.Load<Texture2D>("Projectile.png");
                Cone[i].vitesse.X = 50;
            }

            //Sound
            Song PartyShacker = Content.Load<Song>("Sounds\\Sunny");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 1.00F;
            MediaPlayer.Play(PartyShacker);
            SonDeath = Content.Load<SoundEffect>("Sounds\\DeathGeoMetro");
            DeathGeoMetro = SonDeath.CreateInstance();
            SonKnockout = Content.Load<SoundEffect>("Sounds\\DeathCivic");
            DeathCivic = SonKnockout.CreateInstance();
            SonTir = Content.Load<SoundEffect>("Sounds\\CasqueOrange");
            CasqueOrange = SonTir.CreateInstance();

            //Text
            OCRA = Content.Load<SpriteFont>("Font");

            //Play
            Gamestate = false;

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
            //Quitter
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            //Fullscreen On/Off
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
            {
                this.graphics.ToggleFullScreen();
            }

            //Mouvement BackGround
            if (Gamestate == false && Play.IsAlive == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Gamestate = true;
                    Play.IsAlive = false;
                    MenuOver = false;
                    DeathGeoMetro.Stop();
                    MediaPlayer.Resume();

                    GeoMetro.IsAlive = true;
                    GeoMetro.position.X = Window.Left + 128;
                    GeoMetro.position.Y = Window.Center.Y;

                    Road1.position.X = 0;
                    Road1.position.Y = 0;
                    Road1.vitesse.X = -10;
                    Road2.position.X = 1920;
                    Road2.position.X = 0;
                    Road2.position.Y = 0;
                    Road2.vitesse.X = -10;

                    for (int i = 0; i < Cone.Length; i++)
                    {
                        Cone[i].IsAlive = false;
                    }

                    for (int i = 0; i < Civic.Length; i++)
                    {
                        Civic[i].IsAlive = true;
                        Civic[i].position.X = Rand.Next(Window.Width, Window.Width * 2);
                        Civic[i].position.Y = Rand.Next(Window.Top, Window.Height - 71);
                    }
                }
            }
            //Mouvement Civic
            if (Gamestate == true)
            {
                for (int i = 0; i < Civic.Length; i++)
                {
                    Civic[i].vitesse.X = Rand.Next(5, 25);
                }

                //Movement GeoMetro Y
                if (Keyboard.GetState().IsKeyUp(Keys.S))
                    GeoMetro.vitesse.Y = 0;

                if (Keyboard.GetState().IsKeyUp(Keys.W))
                    GeoMetro.vitesse.Y = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    GeoMetro.vitesse.Y = -10;

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    GeoMetro.vitesse.Y = 10;

                //CasqueOrange GeoMetro
                int Compteur = 0;
                for (int i = 0; i < Cone.Length; i++)
                {
                    //Gestion Cone Alive
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && Cone[i].IsAlive == false && Compteur > 10)
                    {
                        Cone[i].position.X = GeoMetro.position.X;
                        Cone[i].position.Y = GeoMetro.position.Y;
                        Cone[i].IsAlive = true;
                        CasqueOrange.Play();
                        CasqueOrange.Volume = 0.1F;
                        Compteur = 0;
                    }
                    Compteur++;
                    //Gestion Cone Mort
                    if (Cone[i].position.X > Window.Right || Cone[i].IsAlive == false)
                    {
                        Cone[i].IsAlive = false;
                        Cone[i].position.X = GeoMetro.position.X;
                        Cone[i].position.Y = GeoMetro.position.Y;
                    }
                }
                //Road1 Follow
                if (Road2.position.X == Window.Left)
                {
                    Road1.position.X = Road2.position.X + Road2.sprite.Width;
                }
                if (Road1.position.X == Window.Left)
                {
                    Road2.position.X = Road1.position.X + Road1.sprite.Width;
                }
                //Border GeoMetro
                if (GeoMetro.position.Y + 71 >= Window.Bottom)
                {
                    GeoMetro.position.Y = Window.Bottom - 71;
                }
                if (GeoMetro.position.Y <= Window.Top)
                {
                    GeoMetro.position.Y = Window.Top;
                }
                //Border Civic
                for (int i = 0; i < Civic.Length; i++)
                {
                    if (Civic[i].position.X < Window.Left)
                    {
                        if (Civic[i].IsAlive == false)
                        {
                            Civic[i].sprite = Content.Load<Texture2D>("PickUp.png");
                        }
                        else
                        {
                            Civic[i].sprite = Content.Load<Texture2D>("Civic.png");
                        }
                        Civic[i].IsAlive = false;
                        Civic[i].position.X = Rand.Next(Window.Width, Window.Width + (Window.Width/2));
                        Civic[i].position.Y = Rand.Next(Window.Top, Window.Height - 71);
                        Civic[i].IsAlive = true;
                    }
                    if (Civic[i].GetRect().Intersects(Civic[Rand.Next(0,Civic.Length)].GetRect()))
                    {

                    }
                }
            }
            //Gestion Mort
            for (int i = 0; i < Civic.Length; i++)
            {
                for (int y = 0; y < Cone.Length; y++)
                {
                    if (GeoMetro.GetRect().Intersects(Civic[i].GetRect()) && Civic[i].IsAlive == true)
                    {
                        //Mort GeoMetro
                        GeoMetro.IsAlive = false;
                        Cone[y].IsAlive = false;
                        MenuOver = true;
                        Play.IsAlive = true;
                        Gamestate = false;
                        Civic[i].IsAlive = false;

                        MediaPlayer.Pause();
                        DeathCivic.Stop();
                        DeathGeoMetro.Play();
                        Kills = 0;
                    }

                    //Mort Civic
                    if ((Cone[y].GetRect().Intersects(Civic[i].GetRect()) && Civic[i].IsAlive == true))
                    {
                        Civic[i].IsAlive = false;
                        Cone[y].IsAlive = false;
                        MediaPlayer.Pause();
                        DeathCivic.Play();
                        DeathCivic.Volume = 0.10F;
                        MediaPlayer.Resume();
                        Kills++;
                    }

                }
            }
            //GameOver
            if (Gamestate == false && MenuOver == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Gamestate = true;
                    Play.IsAlive = false;
                    MenuOver = false;
                }
            }

            base.Update(gameTime);
            UpdateBackground();
            UpdateBackground2();
            UpdateGeoMetro();
            UpdateCone();
            UpdateEnemy();
        }

        public void UpdateBackground()
        {
            Road1.position += Road1.vitesse;
        }
        public void UpdateBackground2()
        {
            Road2.position += Road2.vitesse;
        }
        public void UpdateGeoMetro()
        {
            GeoMetro.position += GeoMetro.vitesse;
        }
        public void UpdateCone()
        {

            for (int i = 0; i < Cone.Length; i++)
            {
                if (Cone[i].IsAlive == true)
                    Cone[i].position += Cone[i].vitesse;
            }
        }
        public void UpdateEnemy()
        {
            for (int i = 0; i < Civic.Length; i++)
            {
                Civic[i].position -= Civic[i].vitesse;
            }
        }
    
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //Menu
            if (Gamestate == false)
            {
                spriteBatch.Draw(Play.sprite, Play.position);
            }
            if (MenuOver == true)
            {
                spriteBatch.DrawString(OCRA, "Press ENTER to play again", new Vector2(Window.X+ 50, Window.Bottom - 150), Color.LightGray);
            }
            //Jeu
            if (Gamestate == true)
            {
                //Road1
                spriteBatch.Draw(Road1.sprite, Road1.position);
                spriteBatch.Draw(Road2.sprite, Road2.position);
                //Civic
                for (int i = 0; i < Civic.Length; i++)
                {
                    if (Civic[i].IsAlive == true)
                    {
                        spriteBatch.Draw(Civic[i].sprite, Civic[i].position);
                    }
                    if (Civic[i].IsAlive == false)
                    {
                        Civic[i].sprite = Content.Load<Texture2D>("Death.png");
                        spriteBatch.Draw(Civic[i].sprite, Civic[i].position);
                    }
                }
                //GeoMetro
                if (GeoMetro.IsAlive == true)
                {
                    spriteBatch.Draw(GeoMetro.sprite, GeoMetro.position);
                    //Cone
                    for (int i = 0; i < Cone.Length; i++)
                    {
                        if (Cone[i].IsAlive == true)
                        {
                            spriteBatch.Draw(Cone[i].sprite, Cone[i].position);
                        }
                    }
                    //GeoMetro Mort
                    if (GeoMetro.IsAlive == false)
                    {
                        GeoMetro.sprite = Content.Load<Texture2D>("DeathGeoMetro.png");
                        spriteBatch.Draw(GeoMetro.sprite, GeoMetro.position);
                        GeoMetro.sprite = Content.Load<Texture2D>("Geo.png");
                        Gamestate = false;
                        Play.IsAlive = true;
                    }
                }

                //Font 
                if (Gamestate == true)
                {
                    if (MaxKills < Kills)
                    {
                        MaxKills = Kills;
                    }
                    spriteBatch.DrawString(OCRA, Kills.ToString(), new Vector2(10, 10), Color.LightGray);
                    spriteBatch.DrawString(OCRA, "Highscore : " + MaxKills.ToString(), new Vector2(200, 10), Color.LightGray);
                }
                else
                {
                    Kills = 0;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}