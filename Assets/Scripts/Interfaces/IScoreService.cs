using System;

namespace Maze.Interfaces
{
    public interface IScoreService
    {
        event Action<int> OnScoreChanged;
        
        int CurrentScore { get; set; }
        void AddScore();
        void SubtractScore();
    }
}