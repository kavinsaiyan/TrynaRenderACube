using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using CodingMath.Utils;
namespace TrynaRenderACube
{
    public class AnimationSample : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _box;

        private AnimationData _lerpAnimData;
        private AnimationData _smoothStepAnimData;
        private AnimationData _followMouseAnimData;

        public AnimationSample()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _lerpAnimData = new AnimationData();
            _lerpAnimData.currentPos = new Vector2(100, 100);
            _lerpAnimData.endPos = new Vector2(200, 100);
            _lerpAnimData.duration = 2f;
            _lerpAnimData.GetAnimVal = (timer) => { return MathHelper.Lerp(0, 1, timer); };

            _smoothStepAnimData = new AnimationData();
            _smoothStepAnimData.currentPos = new Vector2(100, 200);
            _smoothStepAnimData.endPos = new Vector2(200, 200);
            _smoothStepAnimData.duration = 2f;
            _smoothStepAnimData.GetAnimVal = (timer) => { return MathHelper.SmoothStep(0, 1, timer); };


            _followMouseAnimData = new AnimationData();
            _followMouseAnimData.currentPos = new Vector2(100, 300);
            _followMouseAnimData.endPos = new Vector2(200, 300);
            _followMouseAnimData.duration = 2f;
            _followMouseAnimData.GetAnimVal = (timer) => { return MathHelper.SmoothStep(0, 1, timer); };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _box = Content.Load<Texture2D>("Box");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CheckAndReset(gameTime, _lerpAnimData, new Vector2(100, 100));
            CheckAndReset(gameTime, _smoothStepAnimData, new Vector2(100, 200));
            CheckAndResetEnd(gameTime, _followMouseAnimData, Mouse.GetState().Position.ToVector2());

            base.Update(gameTime);
        }

        private void CheckAndReset(GameTime gameTime, AnimationData animationData, Vector2 resetPos)
        {
            animationData.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (animationData.timer > animationData.duration)
            {
                animationData.timer = 0;
                animationData.currentPos = resetPos;
            }
        }

        private void CheckAndResetEnd(GameTime gameTime, AnimationData animationData, Vector2 resetPos)
        {
            animationData.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (animationData.timer > animationData.duration)
            {
                animationData.timer = 0;
                animationData.endPos = resetPos;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _lerpAnimData.Draw(_spriteBatch, _box);
            _smoothStepAnimData.Draw(_spriteBatch, _box);
            _followMouseAnimData.Draw(_spriteBatch, _box);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public class AnimationData
        {
            public Vector2 endPos;
            public float duration;
            public float timer = 0;
            public Vector2 currentPos;
            public Func<float, float> GetAnimVal;
            public void Update(float deltaTime)
            {
                if (GetAnimVal != null)
                {
                    timer += deltaTime;
                    currentPos = Vector2.Lerp(currentPos, endPos, GetAnimVal(timer / duration));
                }
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D texture2D)
            {
                spriteBatch.Draw(texture2D, currentPos, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            }
        }
    }
}
