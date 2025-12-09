using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
namespace Matteuppgift_3_a
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Vector2 start;
        private Rectangle canon;
        public Texture2D pixel;
        public Texture2D rektangel;
        private Vector2 startPos;
        private List<Vector2> slutPositioner;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
            canon = new Rectangle(0, Window.ClientBounds.Height, 50, 25);
            start = new Vector2(canon.Right, canon.Bottom);
            rektangel = Content.Load<Texture2D>("Rektangel");
            startPos = new Vector2(1, 799);
            slutPositioner = GenerateAngles(startPos, 750.0f);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        void DrawLine(SpriteBatch spriteBatch, Vector2 startPos, Vector2 slutPosition, float thickness = 1.0f) // Metod för utritning av vektorerna.
        {
            Vector2 u = slutPosition - startPos; // Skapar vektor u.
            float längden = MathF.Sqrt(u.X * u.X + u.Y * u.Y); // Bestämmer längden efter ||u||
            float vinkel = MathF.Atan2(u.Y, u.X); // Bestämmer vinkeln på u mot X-Axeln. 

            spriteBatch.Draw(pixel, startPos, null, Color.BlanchedAlmond, vinkel, Vector2.Zero, new Vector2(längden, thickness), SpriteEffects.None, 0f);
        }

        public float GraderTillRadianer(float grader) // Uträkning för att ändra grader till radianer. För att kunna använda det i kod.
        {
            return grader * (MathF.PI / 180);
        }

        public List <Vector2> GenerateAngles(Vector2 startpos, float length )  // 
        {
            slutPositioner = new List<Vector2>(); // Vi skapar en lista som ska hålla riktningsVektorer från en startPunkt med en viss längd.

            for(float vinkel = 0; vinkel <= 90; vinkel += 5) // Vi lägger in dem med här och en vinkel med ökning på 5 grader för varje iteration, upp till 90 grader.
            {
                float rad = GraderTillRadianer(vinkel);
                Vector2 slutPosition = startpos + new Vector2((float)Math.Cos(rad), -(float)Math.Sin(rad)) * length; // Skapar en slutposition genom att addera startPositionen med en enhetsvektor enligt enhetscirkeln som tar hänsyn till vinkel och längd (skalning) för utritning. 
                slutPositioner.Add(slutPosition);
            }
            return slutPositioner;
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
 
            foreach(var slutPositioner in slutPositioner)
            {
                    DrawLine(spriteBatch, startPos, slutPositioner, 1.0f);
            }




            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
