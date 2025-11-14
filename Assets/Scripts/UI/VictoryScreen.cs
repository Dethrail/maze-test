using Maze.Core;
using Maze.Interfaces;
using Maze.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace Maze {
    public class VictoryScreen : MonoBehaviour {
        [SerializeField] private ParticleSystem victoryParticles;
        [SerializeField] private TMP_Text timeText;

        [SerializeField] private TMP_Text distanceText;

        [SerializeField] private Button backButton;
        [SerializeField] private Button restartButton;

        private SceneLoader _sceneLoader;
        private IRuntimeData _runtimeData;
        private IGameStateService _gameStateService;

        [Inject]
        private void Construct(
            SceneLoader sceneLoader,
            IRuntimeData runtimeData,
            IGameStateService gameStateService) {
            _sceneLoader = sceneLoader;
            _runtimeData = runtimeData;
            _gameStateService = gameStateService;
        }

        private void Start() {
            victoryParticles.Play();
            backButton.onClick.AddListener(LoadMenuScene);
            restartButton.onClick.AddListener(RestartScene);

            timeText.text = $"Time: {_runtimeData.TimeElapsed}";
            distanceText.text = $"Dist: {_runtimeData.Distance}";
        }

        private void LoadMenuScene() {
            _sceneLoader.Load("Menu");
        }

        private void RestartScene() {
            _sceneLoader.Load(SceneManager.GetActiveScene().name);
        }
    }
}