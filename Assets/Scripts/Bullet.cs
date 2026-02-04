using UnityEngine;
using Health;
using Singletons;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed = 1f;
        [SerializeField] private float _maxLifeTime = 3f;
        [SerializeField] private int _bulletDmg = 10;

        // Update is called once per frame

        private void Start()
        {
            Destroy(gameObject, _maxLifeTime);
        }
        private void Update()
        {
            transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                return;
            }   
            if (other.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Instance?.PlaySFX("Enemy Hit");
                HealthSystem health = other.GetComponent<HealthSystem>();
                if (health != null && health.CurrentHealth > 0)
                {
                    health.TakeDmg(_bulletDmg);
                }
                ParticleManager.Instance.PlayParticle("Bullet Hit", transform.position,Quaternion.identity);
            }
            else
            {
                //Put Audio trigger for terrain hit here
            }

            Destroy(gameObject);

        }
    }
}
