using System;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.Networking;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] private float _timeBetweenAttack = 1f;
        [SerializeField] private Transform _rightHandTransform;
        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Weapon _weapon = null;

        Health _target;
        float _timeSinceLastAttack = Mathf.Infinity;

        // Start is called before the first frame update
        private void Start()
        {
            EquipWeapon(_weapon);
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            if (_target == null)
            {
                return;
            }

            if (_target.IsDead())
            {
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }

        public void EquipWeapon(Weapon _weapon)
        {
            if (_weapon == null)
            {
                return;
            }
            Animator animator = GetComponent<Animator>();
            _weapon.Spawn(_rightHandTransform, _leftHandTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

            if (_timeSinceLastAttack > _timeBetweenAttack)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation event
        private void Hit()
        {
            if (_target == null)
            {
                return;
            }

            _target.TakeDamage(_weapon.GetDamage());
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weapon.GetRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Health targgetToTest = combatTarget.GetComponent<Health>();
            return targgetToTest != null && !targgetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
            GetComponent<Mover>().Cancel();

        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }

}
