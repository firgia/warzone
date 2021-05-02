using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 15;
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

        private void Awake()
        {
           rb = GetComponent<Rigidbody2D>();   
        }

        void Start()
        {
            // peluru akan otomatis di hancurkan dalam beberapa detik, agar peluru yang keluar arena tetap hancur dan tidak di hitung di memory
            Destroy(gameObject, delayAutoDestroy);
         
        }
     
        private void Update()
        {
            
            if (isShoot) rb.velocity = dir * speed * Time.timeScale;

            lastVelocity = rb.velocity;
            FixedRotation();
        }

        public void Shoot(Vector2 direction)
        {
            isShoot = true;
            dir = direction;
        }

        /// <summary>
        /// peluru akan di hancurkan ketika sudah terkena musuh, atau sudah melebihi batas pantulan
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Enemy)||
                collision.gameObject.CompareTag(TagUtils.Helicopter)) DestroyBullet();
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
            rb.velocity = dir * speed * Time.timeScale;
            totalHitWall++;
            GameObject particle = Instantiate(particleHitPrefab.gameObject);
            particle.transform.position = transform.position;
        }

        /// <summary>
        /// menyesuaikan rotasi berdasarkan arah tujuan peluru
        /// </summary>
        void FixedRotation()
        {
            var dirRot = rb.velocity;
            var angle = Mathf.Atan2(dirRot.y, dirRot.x) * Mathf.Rad2Deg;
            rb.MoveRotation(angle);
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
