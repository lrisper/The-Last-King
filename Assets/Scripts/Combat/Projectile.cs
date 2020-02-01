using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {

        [SerializeField] float _speed = 1f;
        [SerializeField] float _aimHeight = 2f;
        [SerializeField] bool _isHoming = true;
        Health _target = null;
        float _damage = 0;

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
            Destroy(gameObject);
        }
    }
}
