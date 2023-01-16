using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
namespace TrynaRenderACube
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int VERTEX_COUNT = 4;
        private const int INDEX_COUNT = 6;
        private VertexPositionColor[] _vertices;
        private int[] _indices;
        // private Texture2D box;
        private BasicEffect _effect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _vertices = new VertexPositionColor[VERTEX_COUNT]
            {
                new VertexPositionColor(new Vector3(100,100,0), Color.Red),//0
                new VertexPositionColor(new Vector3(200,100,0), Color.Red),//1
                new VertexPositionColor(new Vector3(200,200,0), Color.Red),//2
                new VertexPositionColor(new Vector3(100,200,0), Color.Red),//3
            };

            _indices = new int[INDEX_COUNT]
            {
                0,1,2,
                0,2,3
            };

            // Matrix.CreatePerspective(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0.01f, 300, out _cameraTransform);
            // _effect.Projection = _cameraTransform;

            _effect = new BasicEffect(GraphicsDevice);
            _effect.VertexColorEnabled = true;
            _effect.World = Matrix.Identity;
            _effect.View = Matrix.CreateLookAt(
                new Vector3(150, 150, -200f), // camera position
                new Vector3(150, 150, 0),   // camera target
                -Vector3.Up                   // up vector
            );
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                1000f
            );

            // System.Console.WriteLine("view : " + _effect.View);
            // System.Console.WriteLine("proj : " + _effect.Projection);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // box = Content.Load<Texture2D>("Box");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    VERTEX_COUNT,
                    _indices,
                    0,
                    INDEX_COUNT / 3 //primitive count
                );
            }
            // _spriteBatch.Begin();
            // _spriteBatch.DrawLine(_vertices[0].Position.X, _vertices[0].Position.Y, _vertices[1].Position.X, _vertices[1].Position.Y, Color.Black, 1, 0);
            // _spriteBatch.DrawLine(_vertices[1].Position.X, _vertices[1].Position.Y, _vertices[2].Position.X, _vertices[2].Position.Y, Color.Black, 1, 0);
            // _spriteBatch.DrawLine(_vertices[2].Position.X, _vertices[2].Position.Y, _vertices[3].Position.X, _vertices[3].Position.Y, Color.Black, 1, 0);
            // _spriteBatch.DrawLine(_vertices[0].Position.X, _vertices[0].Position.Y, _vertices[3].Position.X, _vertices[3].Position.Y, Color.Black, 1, 0);
            // _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
