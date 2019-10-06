using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float _weaponRange = 2f;
        [SerializeField] float _timeBetweenAttack = 1f;
        [SerializeField] float _weaponDamage = 5f;

        Transform _target;
        float _timeSinceLastAttack;

        // Start is called before the first frame update
        void Start()
        {

        }


        void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null)
            {
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }


        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack > _timeBetweenAttack)
            {
                // This will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }

        // Animation event
        void Hit()
        {
            Health healthComponent = _target.GetComponent<Health>();
            healthComponent.TakeDamage(_weaponDamage);
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < _weaponRange;
        }


        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.transform;
        }


        public void Cancel()
        {
            _target = null;
        }

    }


}
