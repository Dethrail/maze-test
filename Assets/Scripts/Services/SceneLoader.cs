using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}