using System;
using UnityEngine;

namespace Maze.Interfaces
{
    public interface IMazeGenerator
    {
        /// <summary>
        /// width and height will by multiplied by 2 and + 1, because it should contain walls and floors cells of maze
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>generated maze :) or cat</returns>
        int[,] GenerateMaze(int width, int height);

        event Action OnGenerate;
    }
}