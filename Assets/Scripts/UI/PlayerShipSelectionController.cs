using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_prefab;

        [SerializeField] private Text m_shipname;
        [SerializeField] private Text m_hitpoints;
        [SerializeField] private Text m_speed;
        [SerializeField] private Text m_agility;

        [SerializeField] private Image m_previewImage;

        private void Start()
        {
            if (m_prefab != null)
            {
                m_shipname.text = m_prefab.Nickname;
                m_hitpoints.text = "HP: " + m_prefab.MaxHitPoints.ToString();
                m_speed.text = "SPD: " + m_prefab.Speed.ToString();
                m_agility.text = "AGL: " + m_prefab.Agility.ToString();
                m_previewImage.sprite = m_prefab.PreviewImage;
            }
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = m_prefab;

            MainMenuController.Instance.gameObject.SetActive(true);
        }
    }
}
