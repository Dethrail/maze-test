using System;
using JetBrains.Annotations;
using Maze.Core;
using Maze.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Maze.Services
{
    [UsedImplicitly]
    public class GameStateService : ITickable, IGameStateService
    {
        public event Action OnVictory;
        private readonly IRuntimeData _runtimeData;
        public bool IsVictory { get; private set; }

        public GameStateService(IRuntimeData runtimeData)
        {
            _runtimeData = runtimeData;
        }

        public void Tick()
        {
            if (IsVictory)
            {
                return;
            }

            CheckWin();
        }

        private void CheckWin()
        {
            if (_runtimeData.IsPlayerOnBounds())
            {
                IsVictory = true;
                OnVictory?.Invoke();
                Debug.Log("victory");
            }
        }
    }
}