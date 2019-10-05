using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction
    {

        [SerializeField] Transform _target;
        NavMeshAgent _navMeshAgent;

        public void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }


        void Update()
        {
            UpdateAnimator();
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }


        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("fowardspeed", speed);
        }


    }
}
