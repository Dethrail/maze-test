using JetBrains.Annotations;
using Maze.Interfaces;
using VContainer.Unity;

namespace Maze.Player {
    [UsedImplicitly]
    public class PlayerController : ITickable {
        private readonly PlayerInput _input;
        private readonly IRuntimeData _runtimeData;
        private IPlayerView _playerView;

        public PlayerController(
            PlayerInput input,
            IRuntimeData runtimeData
        ) {
            _input = input;
            _runtimeData = runtimeData;
        }

        /// <summary>
        /// Runtime injections, not clear di way, but much easier and not conflicts with dependency inversion
        /// TODO: investigate this case in future
        /// </summary>
        /// <param name="playerView"></param>
        public void SetPlayerView(IPlayerView playerView) {
            _playerView = playerView;
        }

        public void Tick() {
            if (_playerView == null) {
                return;
            }

            var direction = _input.ReadRawInput();
            _playerView.Rotation(direction);

            if (_runtimeData.MazeController.TryAndMove(direction, out var newPosition)) {
                _playerView.UpdatePosition(newPosition);
            }
        }
    }
}