using UnityEngine.SceneManagement;

namespace Maze.Services {
    public class SceneLoader {
        public void Load(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}