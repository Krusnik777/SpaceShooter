using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {
        [Range(0.0f, 4.0f)]
        [SerializeField] private float m_parallaxPower;
        [SerializeField] private float m_textureScale;

        private Material m_quadMaterial;
        private Vector2 m_initialOffset;

        private void Start()
        {
            m_quadMaterial = GetComponent<MeshRenderer>().material;
            m_initialOffset = UnityEngine.Random.insideUnitCircle;

            m_quadMaterial.mainTextureScale = Vector2.one * m_textureScale;
        }

        private void Update()
        {
            Vector2 offset = m_initialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_parallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_parallaxPower;

            m_quadMaterial.mainTextureOffset = offset;
        }
    }
}
