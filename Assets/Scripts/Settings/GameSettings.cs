using Maze.Interfaces;
using UnityEngine;

namespace Maze
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {


        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject HudPrefab { get; private set; }
        
        // 20 space and name Maze
        [field: Header("Maze settings")]
        [field: SerializeField] public int Width { get; set; } = 10;
        [field: SerializeField] public int Height { get; set; } = 10;
        [field: SerializeField] public GameObject MazeWall { get; private set; }
        [field: SerializeField] public GameObject MazeFloor { get; private set; }
    }
}