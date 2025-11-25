using System;
using JetBrains.Annotations;
using Maze.Core;
using Maze.Interfaces;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Maze.Services {
    [UsedImplicitly]
    public class UiService : IStartable, ITickable, IDisposable {
        private readonly IUIFactory _uiFactory;
        private readonly IGameStateService _gameStateService;
        private readonly GameCanvas _gameCanvas;

        private GameObject _hud;
        private GameObject _victory;

        public UiService(
            IUIFactory uiFactory,
            IGameStateService gameStateService,
            GameCanvas gameCanvas
        ) {
            _uiFactory = uiFactory;
            _gameStateService = gameStateService;
            _gameCanvas = gameCanvas;

            _gameStateService.OnVictory += OnVictory;
        }

        private void OnVictory() {
            if (_hud != null) {
                Object.Destroy(_hud);
            }
            _victory = _uiFactory.CreateVictoryScreen(_gameCanvas.transform);
        }

        public void Start() {
            _hud = _uiFactory.CreateHUD(_gameCanvas.transform);
            // some magic with window controller etc :)
        }

        public void Tick() {
            // some magic with window controller etc :)
        }

        public void Dispose() {
            _gameStateService.OnVictory -= OnVictory;
        }
    }
}