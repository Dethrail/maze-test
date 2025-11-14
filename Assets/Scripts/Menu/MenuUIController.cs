using Maze.Core;
using Maze.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Maze.Menu {
    public class MenuUIController : MonoBehaviour {
        [Inject] private SceneLoader _loader;

        [SerializeField] private Toggle primsAlgorithm;
        [SerializeField] private Toggle kruskalAlgorithm;
        [SerializeField] private TMP_InputField width;
        [SerializeField] private TMP_InputField height;
        [SerializeField] private TMP_InputField exitsCount;

        private void Start() {
            // PlayerPrefs.DeleteAll();
            var algorithmSelected = PlayerPrefs.GetString(PrefsConstraints.ALGORITHM);
            if (algorithmSelected == PrefsConstraints.PRIMS) {
                primsAlgorithm.isOn = true;
            }
            else if (algorithmSelected == PrefsConstraints.KRUSKAL) {
                primsAlgorithm.isOn = false;
            }
            else {
                primsAlgorithm.isOn = true;
            }

            var w = PlayerPrefs.GetInt(PrefsConstraints.WIDTH, 10);
            width.text = w.ToString();
            var h = PlayerPrefs.GetInt(PrefsConstraints.HEIGHT, 10);
            height.text = h.ToString();
            var count = PlayerPrefs.GetInt(PrefsConstraints.EXITS_COUNT, 10);
            exitsCount.text = count.ToString();

            primsAlgorithm.onValueChanged.AddListener(OnPrimsAlgorithmSelected);
            kruskalAlgorithm.onValueChanged.AddListener(OnKruskalAlgorithmSelected);

            width.onEndEdit.AddListener(OnWidthChanged);
            height.onEndEdit.AddListener(OnHeightChanged);
            exitsCount.onEndEdit.AddListener(OnExitsCountChanged);
        }

        private void OnExitsCountChanged(string str) {
            if (int.TryParse(str, out var val)) {
                PlayerPrefs.SetInt(PrefsConstraints.EXITS_COUNT, Mathf.Clamp(val, 1, 10));
            }
        }

        private void OnWidthChanged(string str) {
            if (int.TryParse(str, out var val)) {
                PlayerPrefs.SetInt(PrefsConstraints.WIDTH, Mathf.Clamp(val, 3, 100));
            }
        }

        private void OnHeightChanged(string str) {
            if (int.TryParse(str, out var val)) {
                PlayerPrefs.SetInt(PrefsConstraints.HEIGHT, Mathf.Clamp(val, 3, 100));
            }
        }

        private void OnKruskalAlgorithmSelected(bool toggle) {
            PlayerPrefs.SetString(PrefsConstraints.ALGORITHM, PrefsConstraints.KRUSKAL);
        }

        private void OnPrimsAlgorithmSelected(bool toggle) {
            PlayerPrefs.SetString(PrefsConstraints.ALGORITHM, PrefsConstraints.PRIMS);
        }

        public void OnPlayClicked() {
            _loader.Load("Game");
        }
    }
}