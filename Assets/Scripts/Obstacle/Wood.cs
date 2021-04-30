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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Enemy) || collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                if (totalHit < destroyWhenHit) totalHit++;
                else DestroyWood();
            }
        }

        private void DestroyWood()
        {
            GameObject particle = Instantiate(particleDestroyPrefab.gameObject);
            particle.transform.position = transform.position;

            Destroy(gameObject);
        }
    }
}