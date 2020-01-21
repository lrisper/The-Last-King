using UnityEngine;
using UnityEngine.Serialization;
using RPG.Saving;

namespace RPG.Core
{

    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float _healthPoints = 100f;

        bool _isDead = false;

        public bool IsDead() { return _isDead; }

        public void TakeDamage(float damage)
        {
            _healthPoints = Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints < Mathf.Epsilon)
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
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float)state;
            if (_healthPoints < Mathf.Epsilon)
            {
                Die();
            }
        }
    }
}
