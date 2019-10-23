using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float _chaseDistance = 5f;
        [SerializeField] float _suspicionTime = 5f;
        [SerializeField] PatrolPath _patrolPath;
        [SerializeField] float _waypointTolerance = 1f;

        Fighter _fighter;
        Health _health;
        GameObject _player;
        Mover _mover;
        Vector3 _guardPosition;

        float _timeSinceLastSawPlayer = Mathf.Infinity;
        int _currentWaypointIndex = 0;


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
                PatrolBehavior();
            }
            _timeSinceLastSawPlayer += Time.deltaTime;

        }

        private void PatrolBehavior()
        {
            Vector3 nextPostion = _guardPosition;
            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPostion = GetCurrentWaypoint();
            }
            _mover.StartMoveAction(nextPostion);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < _waypointTolerance;
        }

        private void CycleWaypoint()
        {
            _currentWaypointIndex = _patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_currentWaypointIndex);
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
