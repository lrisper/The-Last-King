using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] Transform _target = null;
    [SerializeField] float _speed = 1f;

    [SerializeField] float _aimHeight = 2f;

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

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetcapsule = _target.GetComponent<CapsuleCollider>();
        if (targetcapsule == null)
        {
            return _target.position;
        }
        return _target.position + Vector3.up * targetcapsule.height / _aimHeight;
    }
}
