using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
namespace Matteuppgift_3
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
        private List<Vector2> riktningsVektorer;
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
            riktningsVektorer = GenerateAngles(startPos, 750.0f);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        void DrawLine(SpriteBatch spriteBatch, Vector2 startPos, Vector2 slutPosition, float thickness = 1.0f)
        {
            Vector2 differens = slutPosition - startPos;
            float längden = MathF.Sqrt(differens.X * differens.X + differens.Y * differens.Y);
            float vinkel = MathF.Atan2(differens.Y, differens.X);

            spriteBatch.Draw(pixel, startPos, null, Color.BlanchedAlmond, vinkel, Vector2.Zero, new Vector2(längden, thickness), SpriteEffects.None, 0f);
        }

        public float GraderTillRadianer(float grader)
        {
            return grader * (MathF.PI / 180);
        }

        public List <Vector2> GenerateAngles(Vector2 startpos, float length )
        {
            riktningsVektorer = new List<Vector2>();

            for(float vinkel = 0; vinkel <= 90; vinkel += 5)
            {
                float rad = GraderTillRadianer(vinkel);
                Vector2 slutPosition = startpos + new Vector2((float)Math.Cos(rad) * length, -(float)Math.Sin(rad) * length);
                riktningsVektorer.Add(slutPosition);
            }
            return riktningsVektorer;
           
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
 
            foreach(var slutPositioner in riktningsVektorer)
            {
                for (int i = 0; i < riktningsVektorer.Count; i++)
                {

                }
                DrawLine(spriteBatch, startPos, slutPositioner, 1.0f);
            }




            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
