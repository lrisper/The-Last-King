using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{


    public class PersistentObjectSpwner : MonoBehaviour
    {

        [SerializeField] GameObject _persistentObjectPrefab;

        static bool _hasSpawned = false;

        // Start is called before the first frame update
        void Awake()
        {
            if (_hasSpawned)
            {
                return;
            }

            SpawnPersistentObject();
            _hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject PersistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(PersistentObject);
        }


    }
}
