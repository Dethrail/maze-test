using Maze.Interfaces;
using UnityEngine;
using VContainer;

namespace Maze.Factories {
    public class UIFactory : IUIFactory {
        private readonly IObjectResolver _resolver;
        private readonly IGameSettings _gameSettings;

        public UIFactory(
            IObjectResolver resolver,
            IGameSettings gameSettings
        ) {
            _resolver = resolver;
            _gameSettings = gameSettings;
        }

        public GameObject CreateHUD(Transform parent) {
            var hud = Object.Instantiate(_gameSettings.HudPrefab, parent);
            // Inject into all MonoBehaviour components on the GameObject
            var components = hud.GetComponents<MonoBehaviour>();
            foreach (var component in components) {
                _resolver.Inject(component);
            }
            return hud;
        }

        public GameObject CreateVictoryScreen(Transform parent) {
            var victory = Object.Instantiate(_gameSettings.VictoryPrefab, parent);
            // Inject into all MonoBehaviour components on the GameObject
            var components = victory.GetComponents<MonoBehaviour>();
            foreach (var component in components) {
                _resolver.Inject(component);
            }
            return victory;
        }
    }
}


