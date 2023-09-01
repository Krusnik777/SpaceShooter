using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Debris : Destructible
    {
        public enum Size
        {
            Small,
            Normal,
            Original
        }

        [SerializeField] private Size m_size;
        [SerializeField] private int m_maxChildDebris;
        [SerializeField] private float m_minChildRandomSpeed;
        [SerializeField] private float m_maxChildRandomSpeed;
        [SerializeField] private GameObject m_destructionPrefab;

        private float spawnOffsetZ;

        private float timer;

        private void Awake()
        {
            timer = 0;

            spawnOffsetZ = transform.position.z;

            SetSize(m_size);
        }

        private void Update()
        {
            if (m_indestrutible)
            {
                timer += Time.deltaTime;

                if (timer >= 1.0f)
                {
                    m_indestrutible = false;
                    timer = 0;
                }
            }

        }

        protected override void OnDeath()
        {
            if (m_size != Size.Small) SpawnChildDebris();

            var destruction = Instantiate(m_destructionPrefab, transform.position, Quaternion.identity);

            destruction.transform.localScale = transform.localScale;

            Destroy(destruction, 1);

            Destroy(gameObject);
        }

        private void SpawnChildDebris()
        {
            spawnOffsetZ += 0.0001f;

            int count = Random.Range(0, m_maxChildDebris+1);

            for (int i = 0; i < count; i++)
            {
                GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity);
                child.GetComponent<Debris>().SetSize(m_size - 1);
                child.GetComponent<Debris>().m_indestrutible = true;
                child.transform.position += new Vector3(0, 0, spawnOffsetZ);

                Rigidbody2D rb = child.GetComponent<Rigidbody2D>();

                float randomSpeed = Random.Range(m_minChildRandomSpeed, m_maxChildRandomSpeed);

                if (rb != null && randomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * randomSpeed;
                }

                spawnOffsetZ += 0.0001f;
            }
        }

        public void SetSize(Size size)
        {
            if (size < 0) return;

            transform.localScale = GetVectorFromSize(size);
            m_size = size;

            if (m_size != Size.Original)
            {
                if (m_size == Size.Normal) m_hitPoints = (int)(m_hitPoints * 0.6f);
                if (m_size == Size.Small) m_hitPoints = (int)(m_hitPoints * 0.4f);
            }
        }

        private Vector3 GetVectorFromSize(Size size)
        {
            if (size == Size.Normal) return new Vector3(0.6f, 0.6f, 0.6f);
            if (size == Size.Small) return new Vector3(0.4f, 0.4f, 0.4f);

            return Vector3.one;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Planet")
            {
                m_size = Size.Small;

                OnDeath();
            }
        }

        


    }
}
