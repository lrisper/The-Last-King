using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;
        [SerializeField] float _suspicionTime = 5f;

        Fighter _fighter;
        Health _health;
        GameObject _player;
        Mover _mover;
        Vector3 _guardPosition;
        float _timeSinceLastSawPlayer = Mathf.Infinity;

        public void Start()
        {
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");

            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_health.IsDead())
            {
                return;
            }

            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (_timeSinceLastSawPlayer < _suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }
            _timeSinceLastSawPlayer += Time.deltaTime;

        }

        private void GuardBehavior()
        {
            _mover.StartMoveAction(_guardPosition);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            _fighter.Attack(_player);
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
