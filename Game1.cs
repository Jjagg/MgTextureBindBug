using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextureBindBug
{
    public class Game1 : Game
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }

        Texture2D texture1;
        Texture2D texture2;
        Effect effect;
        VertexPositionTexture[] vertices = {
            new VertexPositionTexture(new Vector3(-1, 1, 0),  new Vector2(0, 1)), 
            new VertexPositionTexture(new Vector3(1, 1, 0),   new Vector2(1, 1)), 
            new VertexPositionTexture(new Vector3(-1, -1, 0), new Vector2(0, 0)), 
        };

        public Game1()
        {
            new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            effect = Content.Load<Effect>("effect");
            texture1 = new Texture2D(GraphicsDevice, 1, 1);
            texture1.SetData(new[] { Color.Red.PackedValue });

            // uncommenting these lines will make the project render a blue triangle
            //texture2 = new Texture2D(GraphicsDevice, 1, 1));
            //texture2.SetData(new[] { Color.Blue.PackedValue });
        }

        protected override void UnloadContent()
        {
            texture1.Dispose();
            texture2.Dispose();
            base.UnloadContent();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // we didn't apply a texture, so we'd expect garbage or black
            // but we get a red texture (i.e. the texture created above)
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);

            base.Draw(gameTime);
        }
    }
}
