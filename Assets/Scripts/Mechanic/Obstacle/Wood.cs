using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{
    public class Wood : MonoBehaviour
    {
        [Header("Particle")]
        [SerializeField] private ParticleSystem particleDestroyPrefab;
        
        private int destroyWhenHit = 2;
        private int totalHit = 1;

        /// <summary>
        /// Kayu akan hancur jika sudah hit beberapa objek musuh dan peluru
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Enemy) || collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                if (totalHit < destroyWhenHit) totalHit++;
                else DestroyWood();
            }
        }

        /// <summary>
        /// menghancurkan kayu
        /// </summary>
        private void DestroyWood()
        {
            Instantiate(particleDestroyPrefab.gameObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}