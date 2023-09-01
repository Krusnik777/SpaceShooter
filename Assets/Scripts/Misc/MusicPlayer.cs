using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_mainMenuMusic;

        private void Start()
        {
            SetMainMenuMusic();
            m_audioSource.loop = true;

            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void SetMainMenuMusic()
        {
            m_audioSource.clip = m_mainMenuMusic;
            m_audioSource.Play();
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name == "main_menu") SetMainMenuMusic();
            else ChangeClip();
        }

        private void ChangeClip()
        {
            m_audioSource.clip = LevelSequenceController.Instance.CurrentEpisode.LevelsBGM[LevelSequenceController.Instance.CurrentLevel];
            m_audioSource.Play();
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

    }
}
