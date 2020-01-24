using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {

        [SerializeField] private AnimatorOverrideController _weaponOverrideController = null;
        [SerializeField] private GameObject _weaponPrefab = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(_weaponPrefab, handTransform);
            //Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = _weaponOverrideController;
        }
    }
}











