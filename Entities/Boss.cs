using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Boss : Monster
    {
        protected int _size;
        private Bitmap _bossImage;

        public Boss(int size)
        {
            _size = size;
            _bossImage = SplashKit.LoadBitmap("Boss", "assets\\images\\ghast.png");
            GenerateNewPosition();
        }

        public override void Draw()
        {
            // Draw the boss image at the calculated position
            SplashKit.DrawBitmap(_bossImage, _x * SQUARE_SIZE, _y * SQUARE_SIZE);
        }

        public override void Move()
        {

            // The Boss will no longer move randomly like other Monsters
        }

        public void ChasePlayer(Snake snake1, Snake snake2, bool isMultiplayer)
        {
            int targetHeadX; // Variable to store the x-coordinate of the target snake's head
            int targetHeadY; // Variable to store the y-coordinate of the target snake's head

            // Calculate which snake to chase based on multiplayer status
            if (isMultiplayer)
            {
                // Calculate the distance between the boss and the first snake (snake1)
                double distanceToSnake1 = CalculateDistance(this._x, this._y, snake1.HeadX, snake1.HeadY);
                // Calculate the distance between the boss and the second snake (snake2)
                double distanceToSnake2 = CalculateDistance(this._x, this._y, snake2.HeadX, snake2.HeadY);

                // Determine which snake is closer by comparing distances
                if (distanceToSnake2 < distanceToSnake1)
                {
                    // Set target coordinates to snake2's head if it's closer
                    targetHeadX = snake2.HeadX;
                    targetHeadY = snake2.HeadY;
                }
                else
                {
                    // Otherwise, set target coordinates to snake1's head
                    targetHeadX = snake1.HeadX;
                    targetHeadY = snake1.HeadY;
                }
            }
            else
            {
                // In single-player mode, always target snake1
                targetHeadX = snake1.HeadX;
                targetHeadY = snake1.HeadY;
            }

            // Move the boss one step towards the selected target snake's head position
            MoveTowards(targetHeadX, targetHeadY);
        }

        private double CalculateDistance(int bossX, int bossY, int targetX, int targetY)
        {
            // Calculate the horizontal distance between the boss and target
            int dx = bossX - targetX;
            // Calculate the vertical distance between the boss and target
            int dy = bossY - targetY;
            // Return the Euclidean distance between the boss and target
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private void MoveTowards(int targetX, int targetY)
        {
            // Move horizontally towards target
            if (_x < targetX) _x++;  // Move right
            else if (_x > targetX) _x--;  // Move left

            // Move vertically towards target
            if (_y < targetY) _y++;  // Move down
            else if (_y > targetY) _y--;  // Move up

            // Constrain movement within the window boundaries, accounting for boss size (6 x 6 tiles)
            int maxX = (800 / SQUARE_SIZE) - _size;  // Max x boundary accounting for boss width
            int maxY = (600 / SQUARE_SIZE) - _size;  // Max y boundary accounting for boss height

            _x = Math.Max(0, Math.Min(_x, maxX)); // Keep within horizontal bounds
            _y = Math.Max(0, Math.Min(_y, maxY)); // Keep within vertical bounds
        }


        public override void GenerateNewPosition()
        {
            base.GenerateNewPosition(); // Use the base monster method for random position
        }

        public void GenerateNewPosition(int snake1HeadX, int snake1HeadY, int snake2HeadX, int snake2HeadY)
        {
            do
            {
                _x = _random.Next(0, (800 / SQUARE_SIZE) - _size);
                _y = _random.Next(0, (600 / SQUARE_SIZE) - _size);
            }
            // Ensure a safe distance from both snake1 and snake2 heads
            while ((Math.Abs(_x - snake1HeadX) < 10 && Math.Abs(_y - snake1HeadY) < 10) ||
           (Math.Abs(_x - snake2HeadX) < 10 && Math.Abs(_y - snake2HeadY) < 10));
        }

        public int Size => _size;
    }
}