using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class EpisodeSelectionController : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private Text m_episodeNickname;
        [SerializeField] private Image m_previewImage;

        private void Start()
        {
            if (m_episodeNickname != null)
                m_episodeNickname.text = m_episode.EpisodeName;
            if (m_previewImage != null)
                m_previewImage.sprite = m_episode.PreviewImage;
        }

        public void OnStartEpisodeButtonClicked()
        {
            LevelSequenceController.Instance.StartEpisode(m_episode);
        }
    }
}
