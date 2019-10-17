using UnityEngine;
using UnityEngine.Serialization;

namespace Combat
{

    public class Health : MonoBehaviour
    {
        [FormerlySerializedAs("_health")] [SerializeField] private float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print(health);
        }
    }
}
