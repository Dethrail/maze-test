using Maze.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Maze.Core
{
    public class MazeRoot : MonoBehaviour
    {
        private IObjectResolver _container;
        private IGameSettings _gameSettings;

        [Inject]
        public void Constructor(
            IObjectResolver container,
            IGameSettings gameSettings
        )
        {
            _container = container;
            _gameSettings = gameSettings;
        }

        public void RenderMaze(int[,] maze)
        {
            for (var x = 0; x < maze.GetLength(0); x++)
            {
                for (var y = 0; y < maze.GetLength(1); y++)
                {
                    if (maze[x, y] == 0)
                    {
                        // floor
                        var cellView = _container.Instantiate(_gameSettings.MazeFloor, transform);
                        cellView.transform.localPosition = new Vector3(x * 25, y * 25);
                    }

                    if (maze[x, y] == 1)
                    {
                        // wall
                        var cellView = _container.Instantiate(_gameSettings.MazeWall, transform);
                        cellView.transform.localPosition = new Vector3(x * 25, y * 25);
                    }
                }
            }
        }
    }
}