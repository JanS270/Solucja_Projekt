using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Widget;
using System.Collections.Generic;


namespace Projekt_Pum
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public List<Texture2D> PlayerMoveTexture; // Tworzenie Listy na teksturyPlayera

        private Player Gracz; // Tworzenie istancji

        int kierunek = 1;
        private SpriteFont font; // Napis




        ///-----------------------------------------------------------------------------------------------/////


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800; //800
            graphics.PreferredBackBufferHeight = 480; //480
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }


        ///---------------------------      INITIALIZE        --------------------------------------------------------------------/////



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            PlayerMoveTexture = new List<Texture2D>(); // Inicjalizacja LIST TEKSTUR



            base.Initialize();

        }



        ///----------------------------     LOAD        ------------------------------------------------------------------/////



        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            font = Content.Load<SpriteFont>("File"); // Use the name of your sprite font file

            PlayerMoveTexture.Add(Content.Load<Texture2D>("Right_140"));   //0
            PlayerMoveTexture.Add(Content.Load<Texture2D>("Left_140"));    //1
            PlayerMoveTexture.Add(Content.Load<Texture2D>("Back_140"));    //2
            PlayerMoveTexture.Add(Content.Load<Texture2D>("Front_140"));   //3 
            PlayerMoveTexture.Add(Content.Load<Texture2D>("Idle_140"));    //4

            Gracz = new Player(PlayerMoveTexture[0], 1, 4, 10, 600); // Przekazuje teksture do postaci
        }




        ///---------------------------      UNLOAD      ---------------------------------------------/////



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        ///------------------       UPDATE    ------------------------------------------/////




        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            Gracz.gin(gameTime); // TODO: opdowiada za obnizanie sie statystyk wywoluje metody Player
            Gracz.Update(gameTime);

            if (Gracz.PosX <= 10) kierunek = 1;      // PRAWO
            if (Gracz.PosX >= 380) kierunek = 3;     // UP
            if (Gracz.PosY <= 20) kierunek = 2;      // LEWO
            if (Gracz.PosX <= 5) kierunek = 4;       // Down
            if (Gracz.PosY >= 650) kierunek = 0;     // IDLE

            Gracz.Move(kierunek, PlayerMoveTexture);    // Sterowanie Postacia

            base.Update(gameTime);
        }


        ///------------------       DRAW    ------------------------------------------/////


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            // Wyswietlanie tekstu ilosci HP
            if (Gracz.HP != 0) { spriteBatch.DrawString(font, "HP: " + Gracz.HP, new Vector2(100, 100), Color.Black); }
            else { spriteBatch.DrawString(font, "HP: " + Gracz.HP + " YOU DIED!", new Vector2(100, 100), Color.Black); }

            spriteBatch.End();

            Gracz.Draw(spriteBatch, new Vector2(Gracz.PosX, Gracz.PosY)); // Rysowanie Gracza

            base.Draw(gameTime);
        }
    }
}
