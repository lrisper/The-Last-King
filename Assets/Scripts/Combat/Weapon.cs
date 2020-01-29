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

        [SerializeField] private bool _isrightHanded = true;


        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            Transform _handTransform;
            if (_isrightHanded)
            {
                _handTransform = rightHandTransform;
            }
            else
            {
                _handTransform = leftHandTransform;
            }
            Instantiate(_equipedPrefab, _handTransform);

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











