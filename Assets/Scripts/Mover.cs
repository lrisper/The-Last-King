using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] Transform _target;


    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = _target.position;
    }
}
