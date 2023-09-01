using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class PauseMenuPanel : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonShowPause()
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
        }

        public void OnButtonResume()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        public void OnButtonMainMenu()
        {
            Time.timeScale = 1f;

            gameObject.SetActive(false);

            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickname);
        }
    }
}
