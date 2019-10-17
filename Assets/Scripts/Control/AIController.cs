using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using RPG.Core;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;

        Fighter _fighter;
        Health _health;
        GameObject _player;

        public void Start()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (_health.IsDead())
            {
                return;
            }

            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }

        }

        private bool InAttackRangeOfPlayer()
        {

            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer < _chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }
    }
}
