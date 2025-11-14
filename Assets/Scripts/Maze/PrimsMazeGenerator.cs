using System;
using System.Collections.Generic;
using Maze.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Maze.Maze {
    // https://weblog.jamisbuck.org/2011/1/10/maze-generation-prim-s-algorithm
    public class PrimsMazeGenerator : IMazeGenerator {
        private int _width;
        private int _height;
        private int[,] _maze;

        private readonly Vector2Int[] _directions = {
            new(0, 1), // up
            new(1, 0), // right
            new(0, -1), // down
            new(-1, 0) // left
        };

        public void CreateExits(Vector2Int playerStart, int count) {
            throw new NotImplementedException();
        }

        public event Action OnGenerate;

        /// <summary>
        /// width & height will by multiplied by 2 and + 1, because it should contain walls and floors ceil in o
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>generated maze :) or cat</returns>
        public int[,] GenerateMaze(int width, int height) {
            _width = width * 2 + 1;
            _height = height * 2 + 1;
            _maze = new int[_width, _height];

            // wall = 1, floor = 0
            for (var x = 0; x < _width; x++) {
                for (var y = 0; y < _height; y++) {
                    _maze[x, y] = 1;
                }
            }

            var frontier = new List<Vector2Int>();

            var start = new Vector2Int(
                Random.Range(0, width) * 2 + 1,
                Random.Range(0, height) * 2 + 1
            );
            _maze[start.x, start.y] = 0;

            AddFrontier(start, frontier);

            while (frontier.Count > 0) {
                var idx = Random.Range(0, frontier.Count); // random frontier cell

                var cell = frontier[idx];
                frontier.RemoveAt(idx);

                var inMazeNeighbors = GetInMazeNeighbors(cell);

                if (inMazeNeighbors.Count > 0) {
                    var neighbor = inMazeNeighbors[Random.Range(0, inMazeNeighbors.Count)];
                    var wall = (cell + neighbor) / 2;

                    _maze[cell.x, cell.y] = 0;
                    _maze[wall.x, wall.y] = 0;

                    AddFrontier(cell, frontier);
                }
            }

            OnGenerate?.Invoke();
            return _maze;
        }

        private List<Vector2Int> GetInMazeNeighbors(Vector2Int cell) {
            var result = new List<Vector2Int>();
            foreach (var direction in _directions) {
                var next = cell + direction * 2;
                if (InBounds(next) && _maze[next.x, next.y] == 0) {
                    result.Add(next);
                }
            }

            return result;
        }

        private void AddFrontier(Vector2Int cell, List<Vector2Int> frontier) {
            foreach (var direction in _directions) {
                var next = cell + direction * 2;
                if (InBounds(next) && _maze[next.x, next.y] == 1 && !frontier.Contains(next)) {
                    frontier.Add(next);
                }
            }
        }

        private bool InBounds(Vector2Int cell) {
            return cell.x > 0 && cell.x < _width - 1 && cell.y > 0 && cell.y < _height - 1;
        }
    }
}