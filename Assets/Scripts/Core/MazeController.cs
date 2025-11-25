using System;
using JetBrains.Annotations;
using Maze.Interfaces;
using Maze.Player;
using Maze.Settings;
using UnityEngine;
using UnityEngine.Tilemaps;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Maze.Core {
    [UsedImplicitly]
    public class MazeController : IStartable, ITickable {
        private readonly IPlayerFactory _playerFactory;
        private readonly IGameSettings _gameSettings;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly GameCanvas _gameCanvas;
        private readonly MazeRoot _mazeRoot;
        private readonly Camera _camera;
        private readonly RuntimeData _runtimeData;
        private Tilemap _tilemap;
        public event Action OnRender;
        private int[,] _maze;


        public MazeController(
            IPlayerFactory playerFactory,
            IGameSettings gameSettings,
            IMazeGenerator mazeGenerator,
            GameCanvas gameCanvas,
            MazeRoot mazeRoot,
            Camera camera,
            RuntimeData runtimeData
        ) {
            _playerFactory = playerFactory;
            _gameSettings = gameSettings;
            _mazeGenerator = mazeGenerator;
            _gameCanvas = gameCanvas;
            _mazeRoot = mazeRoot;
            _camera = camera;
            _runtimeData = runtimeData;
            _runtimeData.MazeController = this;
        }

        public void Start() {
            var w = PlayerPrefs.GetInt(PrefsConstraints.WIDTH);
            var h = PlayerPrefs.GetInt(PrefsConstraints.HEIGHT);

            _maze = _mazeGenerator.GenerateMaze(w, h);

            var playerStart = _maze.FindPlayerStart();
            var exitsCount = PlayerPrefs.GetInt(PrefsConstraints.EXITS_COUNT, 2);

            _camera.orthographicSize = Mathf.Max(w, h) + 5f; // 5 is hack to prevent maze out of camera bound
            _camera.transform.position = new Vector3(w, h, _camera.transform.position.z);

            _runtimeData.PlayerPosition = playerStart;

            _maze.CreateExits(playerStart, exitsCount);

            var playerView = _playerFactory.CreatePlayer(new Vector3(playerStart.x, playerStart.y));
            if (playerView == null) {
                Debug.LogError("Failed to create player");
            }

            RenderMaze(_maze);
            OnRender?.Invoke();
        }

        private void RenderMaze(int[,] maze) {
            if (_tilemap != null) {
                Object.Destroy(_tilemap.gameObject);
            }

            // Create Grid
            var gridGo = new GameObject("grid");
            gridGo.transform.SetParent(_mazeRoot.transform, false);
            var grid = gridGo.AddComponent<Grid>();
            grid.cellSize = new Vector3(1, 1, 0);
            grid.cellLayout = GridLayout.CellLayout.Rectangle;

            // Create Tilemap
            var tilemapGo = new GameObject("maze tilemap");
            tilemapGo.transform.SetParent(gridGo.transform);
            _tilemap = tilemapGo.AddComponent<Tilemap>();
            var renderer = tilemapGo.AddComponent<TilemapRenderer>();
            renderer.sortingOrder = -1; // move maze behand ui
            tilemapGo.AddComponent<TilemapCollider2D>();
            _tilemap.ClearAllTiles();

            // Fill maze
            var width = maze.GetLength(0);
            var height = maze.GetLength(1);
            _runtimeData.Width = width - 1;
            _runtimeData.Height = height - 1;

            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    var tile = maze[x, y] == 1 ? _gameSettings.WallTile : _gameSettings.FloorTile;
                    _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

        public void Tick() {
            if (Input.GetMouseButtonDown(0)) {
                var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                var cellPos = _tilemap.WorldToCell(mouseWorldPos);

                var clickedTile = _tilemap.GetTile(cellPos);

                if (clickedTile != null) {
                    OnTileClicked(cellPos, clickedTile);
                }
            }
        }

        private void OnTileClicked(Vector3Int cell, TileBase tile) {
            Debug.Log($"Clicked tile at {cell} → {tile.name}");
        }

        public bool TryAndMove(Vector2Int direction, out Vector2Int newPosition) {
            newPosition = _runtimeData.PlayerPosition;

            // cancel diagonal moves
            if (direction.x == 0 && direction.y == 0) // can't jump :)
            {
                return false;
            }

            if (Mathf.Abs(direction.x) == Mathf.Abs(direction.y)) {
                return false;
            }

            newPosition = _runtimeData.PlayerPosition + direction;

            // Bounds check
            if (newPosition.x < 0 || newPosition.y < 0 ||
                newPosition.x >= _maze.GetLength(0) ||
                newPosition.y >= _maze.GetLength(1))
                return false;

            // Wall check
            if (_maze[newPosition.x, newPosition.y] == 1) {
                return false;
            }

            _runtimeData.PlayerPosition = newPosition;
            _runtimeData.Distance++;
            return true;
        }
    }
}