using Maze.Interfaces;
using UnityEngine;

namespace Maze.Player
{
    public class PlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField] private Transform target;

        public void Rotation(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, 0);
            }
            else if (direction == Vector2.down)
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, 180);
            }
            else if (direction == Vector2.left)
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, 90);
            }
            else if (direction == Vector2.right)
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, 270);
            }
        }

        public void UpdatePosition(Vector2Int position)
        {
            transform.position = new Vector3(position.x, position.y);
        }
    }
}