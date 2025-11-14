using Maze.Core;
using UnityEngine;

namespace Maze.Interfaces
{
    public interface IRuntimeData
    {
        public int Height { get; }
        public int Width { get; }
        public int Distance { get; }
        public MazeController MazeController { get; }
        public Vector2Int PlayerPosition { get; }
        public bool IsPlayerOnBounds();
        public float TimeElapsed { get; }
    }
}