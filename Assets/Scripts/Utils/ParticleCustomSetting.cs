using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ParticleCustomSetting : MonoBehaviour
    {
        [Header("Destroy")]
        [SerializeField] private bool autoDestroy;
        [SerializeField] private float delayAutoDestroy;
       

        void Start()
        {
            if (autoDestroy) Destroy(gameObject, delayAutoDestroy);
        }
    }
}

