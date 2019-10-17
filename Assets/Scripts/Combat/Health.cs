using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{

    public class Health : MonoBehaviour
    {
        [SerializeField] private float _healthPoints = 100f;

        bool _isDead = false;
        public bool IsDead() { return _isDead; }

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead)
            {
                return;
            }

            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
