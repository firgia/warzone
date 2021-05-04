using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{
    [RequireComponent(typeof(Explosion))]
    public class TNT : MonoBehaviour
    {
        [Header("Particle")]
        [SerializeField] private ParticleSystem particleExplosionPrefab;

        private Explosion explosion;

        private void Awake()
        {
            explosion = GetComponent<Explosion>();
        }

        /// <summary>
        /// TNT di ledakan ketika terkena hit oleh objek yang mempunyai tag bullet
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                Instantiate(particleExplosionPrefab.gameObject, transform.position, Quaternion.identity);
                explosion.Launch();
            }
        }
    }
}