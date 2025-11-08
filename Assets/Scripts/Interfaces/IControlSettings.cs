using UnityEngine;

namespace Maze.Interfaces
{
    public interface IControlSettings
    {
        KeyCode Left { get; }
        KeyCode Right { get; }
        KeyCode Up { get; }
        KeyCode Down { get; }
    }
}