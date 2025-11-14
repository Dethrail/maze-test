using JetBrains.Annotations;
using UnityEngine;

namespace Maze.Player {
    [UsedImplicitly]
    public class PlayerInput {
        private float _inputCooldown = 0.2f;
        private float _lastInputTime = 0f;

        public Vector2Int ReadRawInput() {
            if (Time.time - _lastInputTime < _inputCooldown) {
                return Vector2Int.zero;
            }

            var dir = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));

            // Only update last input time if there was input
            if (dir != Vector2Int.zero) {
                _lastInputTime = Time.time;
            }

            return dir;
        }
    }
}