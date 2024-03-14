using UnityEngine;

namespace SpriteFlip
{
    public class SpriteFlipper
    {
        private SpriteRenderer _renderer;
        public SpriteFlipper(SpriteRenderer renderer)
        {
            _renderer = renderer;
        }
        public void FlipTowardOnX(float x)
        {
            if (IsDirectingRight(x))
            {
                FlipSprite(SpriteFlipDirection.Right);
            }
            else
            {
                FlipSprite(SpriteFlipDirection.Left);
            }
        }
        public void FlipSprite(SpriteFlipDirection direction)
        {
            switch (direction)
            {
                case SpriteFlipDirection.Left:
                    _renderer.flipX = true;
                    break;
                case SpriteFlipDirection.Right:
                    _renderer.flipX = false;
                    break;
                case SpriteFlipDirection.Up:
                    _renderer.flipY = false;
                    break;
                case SpriteFlipDirection.Down:
                    _renderer.flipY = true;
                    break;
            }
        }
        private bool IsDirectingRight(float targetX)
        {
            if (targetX > 0)
            {
                return true;
            }
            return false;
        }
    }
    public enum SpriteFlipDirection
    {
        Left,
        Right,
        Up,
        Down
    }
}
