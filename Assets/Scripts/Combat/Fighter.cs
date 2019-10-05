using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour
    {
        [SerializeField] float _weaponRange = 2f;
        Transform _target;

        // Start is called before the first frame update
        void Start()
        {

        }


        void Update()
        {
            bool isInRange = Vector3.Distance(transform.position, _target.position) < _weaponRange;
            if (_target != null && !isInRange)
            {
                GetComponent<Mover>().MoveTo(_target.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }


        public void Attack(CombatTarget combatTarget)
        {
            _target = combatTarget.transform;
        }
    }


}
