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

        public void SetTarget(Health target)
        {
            this._target = target;
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
    }
}
