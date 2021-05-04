using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{
    public class Rock : MonoBehaviour
    {
        [Header("Particle")]
        [SerializeField] private ParticleSystem particleDestroyPrefab;
        [SerializeField] private int destroyWhenHit = 3;

        private int totalHit = 1;

        /// <summary>
        /// Batu akan hancur jika sudah hit beberapa objek musuh dan peluru
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(TagUtils.Enemy)|| collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                if (totalHit < destroyWhenHit) totalHit++;
                else DestroyRock();
            }
        }

        /// <summary>
        /// menghancurkan batu
        /// </summary>
        private void DestroyRock()
        {
            Instantiate(particleDestroyPrefab.gameObject,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}