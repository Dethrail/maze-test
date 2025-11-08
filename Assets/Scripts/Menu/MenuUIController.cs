using UnityEngine;
using VContainer;

public class MenuUIController : MonoBehaviour
{
    [Inject] private SceneLoader _loader;

    public MenuUIController(SceneLoader loader)
    {
        _loader = loader;
    }

    public void OnPlayClicked()
    {
        _loader.Load("Game");
    }
}