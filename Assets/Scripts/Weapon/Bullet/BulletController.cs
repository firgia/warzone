using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Weapon
{
   

    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [Header("Destroy")]
        [SerializeField] private int destroyWhenHitWall = 5;
        [SerializeField] private int delayAutoDestroy = 8;
        [Header("Particle")]
        [SerializeField] private ParticleSystem particleHitPrefab;
        [SerializeField] private ParticleSystem particleDestroyPrefab;

        private Rigidbody2D rb;

        private bool isShoot;
        private Vector2 dir;
        private Vector2 lastVelocity;

        int totalHitWall;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, delayAutoDestroy);
        }
     
        private void Update()
        {
            if (isShoot)
            {
                rb.velocity = dir * speed;
            }    

            lastVelocity = rb.velocity;
        }

        public void Shoot(Vector2 direction)
        {
            isShoot = true;
            this.dir = direction;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Enemy)) DestroyBullet();
            else if (totalHitWall >= destroyWhenHitWall) DestroyBullet();
            else
            {
                Vector2 wallNormal = collision.contacts[0].normal;
                BounchingBullet(wallNormal);
            }
        }

        /// <summary>
        /// digunakan untuk memantulkan bola 
        /// </summary>
        void BounchingBullet(Vector2 wallNormal)
        {
            dir = Vector2.Reflect(lastVelocity.normalized, wallNormal);
            rb.velocity = dir * speed;
            totalHitWall++;
            GameObject particle = Instantiate(particleHitPrefab.gameObject);
            particle.transform.position = transform.position;
        }

        /// <summary>
        /// di panggil ketika peluru sudah melebihi total maksimal pantulan ke objek lain
        /// </summary>
        private void DestroyBullet()
        { 
            GameObject particle = Instantiate(particleDestroyPrefab.gameObject);
            particle.transform.position = transform.position;
            Destroy(gameObject);
        }

    }
}
