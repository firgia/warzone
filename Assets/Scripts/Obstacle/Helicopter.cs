using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{


    [RequireComponent(typeof(Explosion),typeof(Rigidbody2D))]
    public class Helicopter : MonoBehaviour
    {
        [Header("Particle")]
        [SerializeField] private ParticleSystem particleExplosionPrefab;

        private Explosion explosion;
        private Rigidbody2D rb;

        private bool isHitBullet;
        private bool isDestroy;

        private void Awake()
        {
            explosion = GetComponent<Explosion>();
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        /// <summary>
        /// Helicopter di ledakan ketika sudah jatuh dan terkena hit oleh objek apa pun.
        /// Helicopter di jatuhkan ketika terkena peluru
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                isHitBullet = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else if(isHitBullet)
            {
                if(!isDestroy) StartCoroutine(DestroyHelicopter());
            }
        }


        /// <summary>
        /// Hellicopter di hancurkan dalam waktu 1 detik
        /// </summary>
        /// <returns></returns>
        IEnumerator DestroyHelicopter()
        {
            isDestroy = true;
            yield return new WaitForSeconds(1);

            Instantiate(particleExplosionPrefab.gameObject, transform.position, Quaternion.identity);
            explosion.Launch();
        }
    }

}