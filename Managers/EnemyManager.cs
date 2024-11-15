using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame;

namespace SnakeGame
{
    public class EnemyManager
    {
        private List<Monster> _monsters;
        private Boss? _boss; // Make _boss nullable
        private bool _bossActive;
        private int _currentBossSize;
        private int _additionalMonstersCount;
        private List<RaidMonster> _additionalMonsters;
        private int _raidsSurvived;
        private bool _bossWarningActive;

        private static EnemyManager? _instance;

        
        public EnemyManager(List<GameObject> gameObjects)
        {

            _monsters = new List<Monster> { (Monster)GameObjectFactory.CreateGameObject("Monster") };
            gameObjects.AddRange(_monsters);
            _boss = null;
            _bossActive = false;
            _bossWarningActive = false;
            _currentBossSize = 6;
            _additionalMonstersCount = 6;
            _additionalMonsters = new List<RaidMonster>();
            _raidsSurvived = 0;
            
        }
        public List<Monster> Monsters => _monsters;
        public Boss? Boss => _boss;
        public List<RaidMonster> AdditionalMonsters => _additionalMonsters;
        public int CurrentBossSize => _currentBossSize;
        public int AdditionalMonstersCount => _additionalMonstersCount;
        public bool BossActive => _bossActive;
        public bool BossWarningActive => _bossWarningActive;
        public int RaidsSurvived => _raidsSurvived;

        public void EnemiesSpawn(List<GameObject> gameObjects, SnakeManager snakeManager)
        {
            // Handle Boss and RaidMonster logic
            if (_bossActive && _boss != null)
            {
                _boss.Draw();

                // Despawn the boss and RaidMonsters after raid ends
                if (GameTimersManager.Instance.BossTimer.Ticks > 30000)
                {
                    _bossActive = false;
                    gameObjects.Remove(_boss);
                    _boss = null;
                    GameTimersManager.Instance.BossTimer.Reset();

                    // Despawn additional raid monsters
                    foreach (RaidMonster raidMonster in _additionalMonsters)
                    {
                        gameObjects.Remove(raidMonster);
                    }
                    _additionalMonsters.Clear();

                    // Increase the number of additional monsters for the next raid
                    _additionalMonstersCount += 2;
                    _raidsSurvived++;
                }
            }
            else if (GameTimersManager.Instance.BossTimer.Ticks > 26500 && !_bossWarningActive)
            {
                _bossWarningActive = true;
                GameTimersManager.Instance.BossWarningTimer.Start();
            }
            else if (GameTimersManager.Instance.BossTimer.Ticks > 30000)
            {
                // Spawn the boss
                _boss = new Boss(_currentBossSize);
                _boss.GenerateNewPosition(snakeManager.Snake.HeadX, snakeManager.Snake.HeadY, snakeManager.SecondSnake.HeadX, snakeManager.SecondSnake.HeadY);
                _bossActive = true;
                gameObjects.Add(_boss);
                GameTimersManager.Instance.BossTimer.Reset();
                _bossWarningActive = false;
                GameTimersManager.Instance.BossWarningTimer.Stop();
                GameTimersManager.Instance.BossWarningTimer.Reset();

                // Spawn and activate RaidMonsters during the raid
                for (int i = 0; i < _additionalMonstersCount; i++)
                {
                    RaidMonster raidMonster = new RaidMonster();
                    _additionalMonsters.Add(raidMonster);
                    gameObjects.Add(raidMonster);
                }
            }

            // Regular monster spawn logic
            if (GameTimersManager.Instance.MonsterSpawnTimer.Ticks > 15000)
            {
                Monster newMonster = (Monster)GameObjectFactory.CreateGameObject("Monster");
                newMonster.GenerateNewPosition(); // Set position explicitly
                _monsters.Add(newMonster);
                gameObjects.Add(newMonster);
                GameTimersManager.Instance.MonsterSpawnTimer.Reset();
            }
        }

        public void EnemiesMovement(SnakeManager snakeManager, bool isMultiplayer)
        {
            if (GameTimersManager.Instance.MonsterTimer.Ticks <= 400)
            {
                return;
            }

            foreach (Monster monster in _monsters)
            {
                monster.Move();
            }

            foreach (RaidMonster raidMonster in _additionalMonsters)
            {
                raidMonster.Move();
            }

            if (_bossActive && _boss != null)
            {
                // Move boss towards the closest player based on BossMoveTimer
                if (GameTimersManager.Instance.BossMoveTimer.Ticks > 200) // Keep using BossMoveTimer to control speed
                {
                    _boss.ChasePlayer(snakeManager.Snake, snakeManager.SecondSnake, isMultiplayer);
                    GameTimersManager.Instance.BossMoveTimer.Reset();
                }
            }

            GameTimersManager.Instance.MonsterTimer.Reset();
        }

    }
}
