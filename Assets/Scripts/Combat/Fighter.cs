using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float _weaponRange = 2f;
        [SerializeField] float _timeBetweenAttack = 1f;
        [SerializeField] float _weaponDamage = 5f;

        Health _target;
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

            if (_target.IsDead())
            {
                return;
            }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }


        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);

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

            _target.TakeDamage(_weaponDamage);
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < _weaponRange;
        }


        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }


        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            _target = null;
        }

    }


}
