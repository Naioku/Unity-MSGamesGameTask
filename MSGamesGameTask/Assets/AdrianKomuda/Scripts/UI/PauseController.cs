using AdrianKomuda.Scripts.Core;
using AdrianKomuda.Scripts.StateMachine.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdrianKomuda.Scripts.UI
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        private InputReader _inputReader;
        private PlayerStateMachine[] _players;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _players = FindObjectsOfType<PlayerStateMachine>();
        }

        private void OnEnable()
        {
            _inputReader.PauseEvent += HandlePause;
        }

        private void OnDisable()
        {
            _inputReader.PauseEvent -= HandlePause;
        }

        public void OnContinue()
        {
            HandlePause();
        }

        public void OnReload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

        public void OnQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void HandlePause()
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            foreach (PlayerStateMachine player in _players)
            {
                player.PlayerMover.IsMovementDisabled = pauseMenu.activeSelf;
            }

            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f;
        }
    }
}
