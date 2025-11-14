using System;
using System.Collections.Generic;
using Maze.Interfaces;
using Random = UnityEngine.Random;

namespace Maze.Maze
{
    // https://weblog.jamisbuck.org/2011/1/3/maze-generation-kruskal-s-algorithm
    public class KruskalMazeGenerator : IMazeGenerator
    {
        private int[,] _maze;
        private int[,] _sets;
        private int _width, _height;

        public int[,] GenerateMaze(int w, int h)
        {
            _width = w;
            _height = h;

            InitMaze();
            InitSets();

            var walls = CreateWalls();
            ShuffleWalls(walls);
            CarveMaze(walls);
            
            OnGenerate?.Invoke();
            return _maze;
        }

        public event Action OnGenerate;

        private void InitMaze()
        {
            var mazeW = _width * 2 + 1;
            var mazeH = _height * 2 + 1;
            _maze = new int[mazeW, mazeH];

            for (var x = 0; x < mazeW; x++)
            {
                for (var y = 0; y < mazeH; y++)
                {
                    _maze[x, y] = 1;
                }
            }
        }

        private void InitSets()
        {
            _sets = new int[_width, _height];
            var id = 1;

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    _sets[x, y] = id++;
                }
            }
        }

        private List<(int, int, int, int)> CreateWalls()
        {
            var walls = new List<(int, int, int, int)>();

            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (x < _width - 1) walls.Add((x, y, x + 1, y));
                    if (y < _height - 1) walls.Add((x, y, x, y + 1));
                }
            }

            return walls;
        }

        private void ShuffleWalls(List<(int, int, int, int)> walls)
        {
            for (var i = 0; i < walls.Count; i++)
            {
                var j = Random.Range(i, walls.Count);
                (walls[i], walls[j]) = (walls[j], walls[i]);
            }
        }

        private void CarveMaze(List<(int, int, int, int)> walls)
        {
            foreach (var wpos in walls)
            {
                var x1 = wpos.Item1;
                var y1 = wpos.Item2;
                var x2 = wpos.Item3;
                var y2 = wpos.Item4;

                if (_sets[x1, y1] == _sets[x2, y2])
                {
                    continue;
                }

                MergeSets(_sets[x2, y2], _sets[x1, y1]);
                CarvePassage(x1, y1, x2, y2);
            }
        }

        private void MergeSets(int oldSet, int newSet)
        {
            for (var x = 0; x < _width; x++)
            {
                for (var y = 0; y < _height; y++)
                {
                    if (_sets[x, y] == oldSet)
                    {
                        _sets[x, y] = newSet;
                    }
                }
            }
        }

        private void CarvePassage(int x1, int y1, int x2, int y2)
        {
            var mx1 = x1 * 2 + 1;
            var my1 = y1 * 2 + 1;
            var mx2 = x2 * 2 + 1;
            var my2 = y2 * 2 + 1;
            var wx = (mx1 + mx2) / 2;
            var wy = (my1 + my2) / 2;

            _maze[mx1, my1] = 0;
            _maze[mx2, my2] = 0;
            _maze[wx, wy] = 0;
        }
    }
}