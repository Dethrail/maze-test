using UnityEngine;

namespace Maze.Interfaces
{
    public interface IPlayerView
    {
        void Rotation(Vector2 direction);
        void UpdatePosition(Vector2Int position);
    }
}