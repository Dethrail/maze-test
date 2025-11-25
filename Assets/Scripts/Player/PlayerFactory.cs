using Maze.Interfaces;
using Maze.Player;
using UnityEngine;
using VContainer;

namespace Maze.Factories {
    public class PlayerFactory : IPlayerFactory {
        private readonly IObjectResolver _resolver;
        private readonly IGameSettings _gameSettings;
        private readonly PlayerController _playerController;

        public PlayerFactory(
            IObjectResolver resolver,
            IGameSettings gameSettings,
            PlayerController playerController
        ) {
            _resolver = resolver;
            _gameSettings = gameSettings;
            _playerController = playerController;
        }

        public PlayerView CreatePlayer(Vector3 position) {
            var player = Object.Instantiate(_gameSettings.PlayerPrefab, position, Quaternion.identity);
            var playerView = player.GetComponent<PlayerView>();
            
            if (playerView == null) {
                Debug.LogError("PlayerPrefab does not have a PlayerView component");
                Object.Destroy(player);
                return null;
            }

            // Inject dependencies into the PlayerView (if it has any)
            _resolver.Inject(playerView);
            
            // Wire PlayerView to PlayerController
            _playerController.SetPlayerView(playerView);

            return playerView;
        }
    }
}


