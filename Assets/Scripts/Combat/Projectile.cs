﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _aimHeight = 2f;
        [SerializeField] private bool _isHoming = true;
        [SerializeField] private GameObject _hitEffect = null;
        [SerializeField] private float _maxLifeTime = 10f;
        [SerializeField] private GameObject[] _destroyOnHit = null;
        [SerializeField] private float _lifeAfterImpact = 2f;

        private Health _target = null;
        private float _damage = 0;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update is called once per frame
        void Update()
        {
            if (_target == null)
            {
                return;
            }
            if (_isHoming && !_target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damge)
        {
            this._target = target;
            this._damage = damge;

            Destroy(gameObject, _maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetcapsule = _target.GetComponent<CapsuleCollider>();
            if (targetcapsule == null)
            {
                return _target.transform.position;
            }
            return _target.transform.position + Vector3.up * targetcapsule.height / _aimHeight;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target)
            {
                return;
            }

            if (_target.IsDead())
            {
                return;
            }
            _target.TakeDamage(_damage);

            _speed = 0;

            // todo fix fire ball prefab
            // todo make hit effect for all weapons 
            if (_hitEffect != null)
            {
                Instantiate(_hitEffect, GetAimLocation(), transform.rotation);
            }
            foreach (GameObject toDestroy in _destroyOnHit)
            {
                Destroy(toDestroy);
            }
            Destroy(gameObject, _lifeAfterImpact);
        }
    }
}
