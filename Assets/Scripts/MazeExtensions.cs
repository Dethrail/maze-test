using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public static class MazeExtensions
    {
        private static readonly Vector2Int[] Directions = {
            new (1, 0),
            new (-1, 0),
            new (0, 1),
            new (0, -1)
        };
        
        public static Vector2Int FindPlayerStart(this int[,] maze)
        {
            var width = maze.GetLength(0);
            var height = maze.GetLength(1);

            var centerX = width / 2;
            var centerY = height / 2;

            // BFS from center to find the nearest floor
            var queue = new Queue<Vector2Int>();
            var visited = new bool[width, height];

            queue.Enqueue(new Vector2Int(centerX, centerY));
            visited[centerX, centerY] = true;

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                var x = cell.x;
                var y = cell.y;

                if (maze[x, y] == 0)
                {
                    return new Vector2Int(x, y); // found nearest floor
                }

                // explore neighbors
                for (var i = 0; i < 4; i++)
                {
                    var nx = x + dx[i];
                    var ny = y + dy[i];

                    if (nx >= 0 && nx < width && ny >= 0 && ny < height && !visited[nx, ny])
                    {
                        queue.Enqueue(new Vector2Int(nx, ny));
                        visited[nx, ny] = true;
                    }
                }
            }

            // fallback if no floor found
            return new Vector2Int(centerX, centerY);
        }

        /// <summary>
        /// Create exits, check theirs reachability from player position with bfs
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="playerStart"></param>
        /// <param name="count"></param>
        public static void CreateExits(this int[,] maze, Vector2Int playerStart, int count)
        {
            var width = maze.GetLength(0);
            var height = maze.GetLength(1);

            // BFS to find reachable cells
            var visited = new bool[width, height];
            var queue = new Queue<Vector2Int>();
            queue.Enqueue(playerStart);
            visited[playerStart.x, playerStart.y] = true;
            
            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                foreach (var dir in Directions)
                {
                    var nx = cell.x + dir.x;
                    var ny = cell.y + dir.y;
                    if (nx >= 0 && nx < width && ny >= 0 && ny < height &&
                        maze[nx, ny] == 0 && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue(new Vector2Int(nx, ny));
                    }
                }
            }

            // Only check **border cells**, not the whole grid
            var  borderCandidates = new List<Vector2Int>();
            GetherBorderCandidates(maze, width, visited, borderCandidates, height);

            // Randomly pick exits
            for (var i = 0; i < count && borderCandidates.Count > 0; i++)
            {
                var idx = Random.Range(0, borderCandidates.Count);
                var exit = borderCandidates[idx];
                maze[exit.x, exit.y] = 0;
                borderCandidates.RemoveAt(idx);
            }
        }

        private static void GetherBorderCandidates(int[,] maze, int width, bool[,] visited, List<Vector2Int> borderCandidates, int height)
        {
            for (var x = 0; x < width; x++)
            {
                if (maze[x, 0] == 1 && IsAdjacentVisited(maze, visited, x, 0))
                {
                    borderCandidates.Add(new Vector2Int(x, 0));
                }

                if (maze[x, height - 1] == 1 && IsAdjacentVisited(maze, visited, x, height - 1))
                {
                    borderCandidates.Add(new Vector2Int(x, height - 1));
                }
            }

            for (var y = 1; y < height - 1; y++)
            {
                if (maze[0, y] == 1 && IsAdjacentVisited(maze, visited, 0, y))
                {
                    borderCandidates.Add(new Vector2Int(0, y));
                }

                if (maze[width - 1, y] == 1 && IsAdjacentVisited(maze, visited, width - 1, y))
                {
                    borderCandidates.Add(new Vector2Int(width - 1, y));
                }
            }
        }

        private static bool IsAdjacentVisited(int[,] maze, bool[,] visited, int x, int y)
        {
            var width = maze.GetLength(0);
            var height = maze.GetLength(1);
            
            foreach (var dir in Directions)
            {
                var nx = x + dir.x;
                var ny = y + dir.y;
                if (nx >= 0 && nx < width && ny >= 0 && ny < height && visited[nx, ny])
                    return true;
            }

            return false;
        }
    }
}