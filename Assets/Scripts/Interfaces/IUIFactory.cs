using UnityEngine;

namespace Maze.Interfaces {
    public interface IUIFactory {
        GameObject CreateHUD(Transform parent);
        GameObject CreateVictoryScreen(Transform parent);
    }
}


