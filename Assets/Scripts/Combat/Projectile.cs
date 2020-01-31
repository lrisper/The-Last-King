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
        Health _target = null;
        float _damage = 0;

        // Update is called once per frame
        void Update()
        {
            if (_target == null)
            {
                return;
            }
            transform.LookAt(GetAimLocation());
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
            _target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
