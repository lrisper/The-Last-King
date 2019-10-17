using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{

    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead)
            {
                return;
            }

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
