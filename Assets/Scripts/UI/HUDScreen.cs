using System;
using Maze.Core;
using Maze.Interfaces;
using Maze.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Maze.UI {
    public class HUDScreen : MonoBehaviour, IDisposable {
        #region Inspector fields

        [SerializeField] private TMP_Text timeText;

        [SerializeField] private TMP_Text distanceText;

        [SerializeField] private Button backButton;
        [SerializeField] private Button restartButton;

        #endregion

        #region Inject fields

        private SceneLoader _sceneLoader;
        private IRuntimeData _runtimeData;
        private IGameStateService _gameStateService;

        #endregion

        [Inject]
        private void Construct(
            SceneLoader sceneLoader,
            IRuntimeData runtimeData,
            IGameStateService gameStateService
        ) {
            _sceneLoader = sceneLoader;
            _runtimeData = runtimeData;
            _gameStateService = gameStateService;
        }

        private void Start() {
            backButton.onClick.AddListener(LoadMenuScene);
            restartButton.onClick.AddListener(RestartScene);
        }

        public void Update() {
            // Guard against injection not completed yet
            if (_gameStateService == null || _runtimeData == null) {
                return;
            }

            if (_gameStateService.IsVictory) {
                return;
            }

            if (timeText != null) {
                timeText.text = $"Time: {_runtimeData.TimeElapsed:F1}";
            }


            if (distanceText != null) {
                distanceText.text = $"Dist: {_runtimeData.Distance}";
            }
        }

        private void LoadMenuScene() {
            _sceneLoader.Load("Menu");
        }

        private void RestartScene() {
            _sceneLoader.Load(SceneManager.GetActiveScene().name);
        }

        public void Dispose() {
            backButton.onClick.RemoveListener(LoadMenuScene);
            restartButton.onClick.RemoveListener(RestartScene);
        }
    }
}