using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;

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
        [SerializeField] private Projectile _projectile = null;

        private const string _weaponName = "Weapon";

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if (_equipedPrefab != null)
            {
                Transform _handTransform = GetTransform(rightHandTransform, leftHandTransform);
                GameObject weapon = Instantiate(_equipedPrefab, _handTransform);
                weapon.name = _weaponName;
            }

            if (animator != null)
            {
                animator.runtimeAnimatorController = _weaponOverrideController;
            }
        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldweapon = rightHandTransform.Find(_weaponName);
            if (oldweapon == null)
            {
                oldweapon = leftHandTransform.Find(_weaponName);
            }

            if (oldweapon == null)
            {
                return;
            }
            oldweapon.name = "DESTROYING";
            Console.WriteLine("Destroying " + oldweapon.name);
            Destroy(oldweapon.gameObject);
        }

        private Transform GetTransform(Transform rightHandTransform, Transform leftHandTransform)
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

            return _handTransform;
        }

        public bool HasProjectile()
        {
            return _projectile != null;
        }

        public void LunchProjectile(Transform rightHandTransform, Transform leftHandTransform, Health target)
        {
            Projectile projectileInstance = Instantiate(_projectile, GetTransform(rightHandTransform, leftHandTransform).position, Quaternion.identity);
            projectileInstance.SetTarget(target, _weaponDamage);
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











