using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.State
{
    public interface IGameState
    {
        void Update(Game game);
        void Draw(Game game);
        void Enter(Game game);
        void Exit(Game game);
    }
}
