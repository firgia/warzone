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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag(TagUtils.Enemy)|| collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                if (totalHit < destroyWhenHit) totalHit++;
                else DestroyRock();
            }
        }

        private void DestroyRock()
        {
            GameObject particle = Instantiate(particleDestroyPrefab.gameObject);
            particle.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}