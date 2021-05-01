using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Helper
{
    public class LimitFPS : MonoBehaviour
    {

        private int target = 60;

        void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = target;
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (Application.targetFrameRate != target)
                Application.targetFrameRate = target;
        }
    }
}
