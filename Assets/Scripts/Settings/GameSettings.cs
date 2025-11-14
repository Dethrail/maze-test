using Maze.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Settings {
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings {
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        [field: SerializeField] public GameObject HudPrefab { get; private set; }
        [field: SerializeField] public GameObject VictoryPrefab { get; private set; }

        // 20 space and name Maze
        [field: Header("Maze settings")]
        [field: SerializeField]
        public GameObject MazeWall { get; private set; }

        [field: SerializeField] public GameObject MazeFloor { get; private set; }

        [field: SerializeField] public TileBase WallTile { get; set; }
        [field: SerializeField] public TileBase FloorTile { get; set; }
    }
}