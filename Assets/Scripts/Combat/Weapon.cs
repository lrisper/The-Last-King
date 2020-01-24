using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] private AnimatorOverrideController _weaponOverrideController = null;
        [SerializeField] private GameObject _equipedPrefab = null;
        [SerializeField] private float _weaponRange = 2f;
        [SerializeField] private float _weaponDamage = 5f;


        public void Spawn(Transform handTransform, Animator animator)
        {
            if (_equipedPrefab != null)
            {
                Instantiate(_equipedPrefab, handTransform);
            }

            if (animator != null)
            {
                animator.runtimeAnimatorController = _weaponOverrideController;
            }

        }

        public float GetDamage()
        {
            return _weaponDamage;
        }

        public float GetRange()
        {
            return _weaponRange;
        }
    }
}











