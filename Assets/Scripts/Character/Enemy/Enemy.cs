using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace AI
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour
    {
        
        void Start()
        {
        
        }

        
        void Update()
        {
        
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet) || collision.gameObject.CompareTag(TagUtils.SmasherObstacle))
            {
                DeathUsingRagdoll();
            }
        }

  
        /// <summary>
        /// di gunakan jika dibutuhkan efect phisic ketika musuh mati
        /// </summary>
        private void DeathUsingRagdoll()
        {
            /// nanti di isi effect ragdoll
            Destroy(gameObject);
        }
    }

}