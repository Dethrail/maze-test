using JetBrains.Annotations;
using Maze.Core;
using Maze.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Settings {
    [UsedImplicitly]
    public class RuntimeData : IRuntimeData, ITickable // may be runtime data should not count time
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Distance { get; set; }
        public MazeController MazeController { get; set; }
        public Vector2Int PlayerPosition { get; set; }

        public bool IsPlayerOnBounds() {
            if (PlayerPosition.x == 0 || PlayerPosition.x == Width) {
                return true;
            }

            if (PlayerPosition.y == 0 || PlayerPosition.y == Height) {
                return true;
            }

            return false;
        }

        public float TimeElapsed { get; private set; }

        public void Tick() {
            TimeElapsed += Time.deltaTime;
        }
    }
}