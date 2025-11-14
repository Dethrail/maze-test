using UnityEngine;
using UnityEngine.Tilemaps;

namespace Maze.Interfaces
{
    public interface IGameSettings
    {
        public GameObject PlayerPrefab { get; }
        public GameObject HudPrefab { get; }
        public GameObject VictoryPrefab { get; }
        
        public TileBase WallTile { get; set; }
        public TileBase FloorTile { get; set; }
    }
}