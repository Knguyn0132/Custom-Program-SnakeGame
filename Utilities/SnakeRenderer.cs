using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Utilities
{
    public class SnakeRenderer
    {
        private Bitmap _snakeHeadImage;
        private Bitmap _snakeBodyImage;

        public SnakeRenderer(string headImagePath = "assets\\images\\snake_head.png", 
                           string bodyImagePath = "assets\\images\\snake_body.png",
                           string headImageName = "Snake Head",
                           string bodyImageName = "Snake Body")
        {
            _snakeHeadImage = SplashKit.LoadBitmap(headImageName, headImagePath);
            _snakeBodyImage = SplashKit.LoadBitmap(bodyImageName, bodyImagePath);
        }

        public void DrawSnake(List<Point2D> positions, Direction direction)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Point2D position = positions[i];
                if (i == positions.Count - 1)
                {
                    // Draw the snake head with rotation
                    float angle = GetHeadRotationAngle(direction);
                    SplashKit.DrawBitmap(_snakeHeadImage, position.X, position.Y, SplashKit.OptionRotateBmp(angle));
                }
                else
                {
                    // Draw the snake body
                    SplashKit.DrawBitmap(_snakeBodyImage, position.X, position.Y);
                }
            }
        }

        private float GetHeadRotationAngle(Direction direction)
        {
            return direction switch
            {
                Direction.Up => 180,
                Direction.Down => 0,
                Direction.Left => 90,
                Direction.Right => 270,
                _ => 0
            };
        }
    }
}
