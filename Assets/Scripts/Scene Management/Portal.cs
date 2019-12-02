using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{

    public class Portal : MonoBehaviour
    {
        [SerializeField] int _sceneToLoad = -1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(_sceneToLoad);
            }
            print("Portal Triggered");
        }

    }
}