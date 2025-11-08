using Maze.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Core
{
    public class GamePresenter : IStartable
    {
        private readonly IObjectResolver _container;
        private readonly IGameSettings _gameSettings;
        private readonly IMazeGenerator _mazeGenerator;
        private readonly GameCanvas _gameCanvas;
        private readonly MazeRoot _mazeRoot;

        public GamePresenter(
            IObjectResolver container,
            IGameSettings gameSettings,
            IMazeGenerator mazeGenerator,
            GameCanvas gameCanvas,
            MazeRoot mazeRoot
        )
        {
            _container = container;
            _gameSettings = gameSettings;
            _mazeGenerator = mazeGenerator;
            _gameCanvas = gameCanvas;
            _mazeRoot = mazeRoot;
        }

        public void Start()
        {
            _container.Instantiate(_gameSettings.PlayerPrefab, Vector3.zero, Quaternion.identity);
            _container.Instantiate(_gameSettings.HudPrefab, _gameCanvas.transform);

            var maze = _mazeGenerator.GenerateMaze(_gameSettings.Width, _gameSettings.Height);
            _mazeRoot.RenderMaze(maze);
        }
    }
}