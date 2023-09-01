using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleArea))]
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Debris[] m_debrisPrefabs;
        [SerializeField] private CircleArea m_area;
        [SerializeField] private int m_numDebris;
        [SerializeField] private float m_randomSpeed;

        private void Start()
        {
            for(int i = 0; i < m_numDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_debrisPrefabs.Length);

            GameObject debris = Instantiate(m_debrisPrefabs[index].gameObject);

            debris.GetComponent<Debris>().SetSize((Debris.Size)Random.Range(0, 3));

            debris.transform.position = m_area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if (rb != null && m_randomSpeed > 0)
            {
                rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_randomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }
}
