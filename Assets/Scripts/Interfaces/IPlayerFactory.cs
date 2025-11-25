using Maze.Player;
using UnityEngine;

namespace Maze.Interfaces {
    public interface IPlayerFactory {
        PlayerView CreatePlayer(Vector3 position);
    }
}


