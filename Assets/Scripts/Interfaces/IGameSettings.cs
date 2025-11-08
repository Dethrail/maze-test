using UnityEngine;

namespace Maze.Interfaces
{
    public interface IGameSettings
    {
        public GameObject PlayerPrefab { get; }
        public GameObject HudPrefab { get; }

        public int Width { get; }
        public int Height { get; }
        public GameObject MazeWall { get; }
        public GameObject MazeFloor { get; }
    }
}