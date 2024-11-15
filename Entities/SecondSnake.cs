using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Utilities;

namespace SnakeGame
{
    public class SecondSnake : Snake
    {
        private SnakeRenderer _renderer;

        public SecondSnake()
        {
            _positions = new List<Point2D>
            {
                new Point2D { X = 38 * SQUARE_SIZE, Y = 0 },
                new Point2D { X = 38 * SQUARE_SIZE, Y = SQUARE_SIZE },
                new Point2D { X = 38 * SQUARE_SIZE, Y = 2 * SQUARE_SIZE },
                new Point2D { X = 38 * SQUARE_SIZE, Y = 3 * SQUARE_SIZE }
            };
            _direction = Direction.Down;
            _growthSegments = 0;
            
            // Initialize the renderer with second snake's specific images
            _renderer = new SnakeRenderer(
                headImagePath: "assets\\images\\second_snake_head.png",
                bodyImagePath: "assets\\images\\second_snake_body.png",
                headImageName: "Second Snake Head",
                bodyImageName: "Second Snake Body"
            );
        }

        public override void Draw()
        {
            _renderer.DrawSnake(_positions, _direction);
        }
    }
}