using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour _currentAction;

        public void StartAction(MonoBehaviour action)
        {
            if (_currentAction == action)
            {
                return;
            }

            if (_currentAction != null)
            {
                print("Canceling" + _currentAction);
            }

            _currentAction = action;
        }


        void Update()
        {

        }
    }
}
