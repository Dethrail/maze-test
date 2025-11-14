using System;

namespace Maze.Core {
    public interface IGameStateService {
        public event Action OnVictory;
        public bool IsVictory { get; }
    }
}