using System;
using JetBrains.Annotations;
using Maze.Core;
using Maze.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Maze.Services {
    [UsedImplicitly]
    public class UiService : IStartable, ITickable, IDisposable {
        private readonly IObjectResolver _builder;
        private readonly IGameSettings _gameSettings;
        private readonly IGameStateService _gameStateService;
        private readonly GameCanvas _gameCanvas;

        private GameObject _hud;
        private GameObject _victory;

        public UiService(
            IObjectResolver builder,
            IGameSettings gameSettings,
            IGameStateService gameStateService,
            GameCanvas gameCanvas
        ) {
            _builder = builder;
            _gameSettings = gameSettings;
            _gameStateService = gameStateService;
            _gameCanvas = gameCanvas;

            _gameStateService.OnVictory += OnVictory;
        }

        private void OnVictory() {
            Object.Destroy(_hud.gameObject);
            _victory = _builder.Instantiate(_gameSettings.VictoryPrefab, _gameCanvas.transform);
            _builder.Inject(_victory);
        }

        public void Start() {
            _hud = _builder.Instantiate(_gameSettings.HudPrefab, _gameCanvas.transform);
            _builder.Inject(_hud);
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