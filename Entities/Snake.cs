using SnakeGame.Utilities;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
     public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public class Snake : GameObject
    {
        protected List<Point2D> _positions;  
        protected Direction _direction;  
        protected int _growthSegments;  // Counter for the number of growth segments
        protected Direction _nextDirection; // Field to store the upcoming direction
        private SnakeRenderer _renderer;  // Renderer to draw the snake

        public Snake()
        {
            _positions = new List<Point2D>
                {
                    new Point2D { X = 2 * SQUARE_SIZE, Y = 0 },  // Initial position of the snake's head
                    new Point2D { X = 2 * SQUARE_SIZE, Y = SQUARE_SIZE },
                    new Point2D { X = 2 * SQUARE_SIZE, Y = 2 * SQUARE_SIZE }, 
                    new Point2D { X = 2 * SQUARE_SIZE, Y = 3 * SQUARE_SIZE }  
                };
            _direction = Direction.Down;  // Set the initial direction of the snake
            _growthSegments = 0;  // Initialize growth segments to 0
            _nextDirection = _direction; // Initialize nextDirection
            _renderer = new SnakeRenderer();  
        }

        public override void Draw()
        {
            _renderer.DrawSnake(_positions, _direction);  // Draw the snake using the renderer
        }

        public void Grow()
        {
            _growthSegments++;  // Increment the growth segments counter
        }

        public void Move()
        {
            _direction = _nextDirection;  // Update the direction of the snake
            if (_growthSegments <= 0)  // Only remove the tail if we're not growing
            {
                _positions.RemoveAt(0);  // Remove the tail segment
            }
            else
            {
                _growthSegments--;  // Decrease the growth counter
            }

            _positions.Add(NextPosition());  // Add a new segment to the snake's head
        }

        public bool CanChangeDirectionTo(Direction newDirection)
        {
            // Prevent the snake from reversing direction
            return !((_direction == Direction.Up && newDirection == Direction.Down) ||
                 (_direction == Direction.Down && newDirection == Direction.Up) ||
                 (_direction == Direction.Left && newDirection == Direction.Right) ||
                 (_direction == Direction.Right && newDirection == Direction.Left));
        }

        public int X => (int)_positions[_positions.Count - 1].X / SQUARE_SIZE;  // Get the X coordinate of the snake's head
        public int Y => (int)_positions[_positions.Count - 1].Y / SQUARE_SIZE;  // Get the Y coordinate of the snake's head

        private Point2D NextPosition()
        {
            Point2D head = _positions[_positions.Count - 1];  // Get the current head position
            Point2D newHead = new Point2D { X = head.X, Y = head.Y };  // Create a new point for the next head position

            switch (_direction)
            {
                case Direction.Down: newHead.Y += SQUARE_SIZE; break;  // Move the head down
                case Direction.Up: newHead.Y -= SQUARE_SIZE; break;  // Move the head up
                case Direction.Left: newHead.X -= SQUARE_SIZE; break;  // Move the head left
                case Direction.Right: newHead.X += SQUARE_SIZE; break;  // Move the head right
            }

            newHead.X = (newHead.X + 800) % 800;  // Wrap the head position horizontally
            newHead.Y = (newHead.Y + 600) % 600;  // Wrap the head position vertically

            return newHead;  // Return the new head position
        }

        private double CalculateDistance(Point2D point1, Point2D point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));  // Calculate the distance between two points
        }

        private bool CheckCollisionWith(Point2D head, List<Point2D> segments)
        {
            for (int i = 0; i < segments.Count - 1; i++) // Only counts the body
            {
                if (CalculateDistance(head, segments[i]) < SQUARE_SIZE)  // Check if the head collides with any segment
                {
                    return true;  // Collision detected
                }
            }
            return false;  // No collision detected
        }

        public bool HitItself()
        {
            Point2D head = _positions[_positions.Count - 1];  
            return CheckCollisionWith(head, _positions);  // Check if the head collides with any segment of the snake
        }

        public bool CollidesWith(Snake other)
        {   
            // Counts to the body so it doesn't detect
            Point2D head = _positions[_positions.Count - 1];  
            return CheckCollisionWith(head, other._positions);  // Check if the head collides with any segment of the other snake
        }

        public Direction Direction
        {
            get => _nextDirection; // Return nextDirection to reflect upcoming change
            set
            {
                // Update _nextDirection if the new direction is valid
                if (CanChangeDirectionTo(value))
                {
                    _nextDirection = value;
                }
            }
        }

        public int HeadX => (int)_positions[_positions.Count - 1].X / SQUARE_SIZE;  // Get the X coordinate of the snake's head
        public int HeadY => (int)_positions[_positions.Count - 1].Y / SQUARE_SIZE;  // Get the Y coordinate of the snake's head
    }
}