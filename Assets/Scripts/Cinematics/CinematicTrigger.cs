using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool _alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!_alreadyTriggered && other.tag == "Player")
            {
                _alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }

        }
    }
}
