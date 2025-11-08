using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class HUDController : MonoBehaviour, ITickable, IDisposable
{
    #region Inspector fields

    [SerializeField] private TMP_Text timeText;

    [SerializeField] private TMP_Text distanceText;

    [SerializeField] private Button backButton;
    [SerializeField] private Button restartButton;

    #endregion

    #region Inject fields

    private Timer _timer;
    // private PlayerMovement _player;
    private SceneLoader _sceneLoader;

    #endregion

    [Inject]
    private void Construct(Timer timer, SceneLoader sceneLoader)
    {
        _timer = timer;
        // _player = player;
        _sceneLoader = sceneLoader;

        backButton.onClick.AddListener(LoadMenuScene);
        restartButton.onClick.AddListener(RestartScene);
    }



    public void Tick()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {_timer.TimeElapsed:F1}";
        }


        if (distanceText != null)
        {
            // distanceText.text = $"Dist: {_player.DistanceTraveled:F1}";
        }
    }

    private void LoadMenuScene()
    {
        _sceneLoader.Load("Menu");
    }
    
    private void RestartScene()
    {
        _sceneLoader.Load(SceneManager.GetActiveScene().name);
    }
    
    public void Dispose()
    {
        backButton.onClick.RemoveListener(LoadMenuScene);
        restartButton.onClick.RemoveListener(RestartScene);
    }
}