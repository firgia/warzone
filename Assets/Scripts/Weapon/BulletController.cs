using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Weapon
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private int destroyWhenHitWall = 5;
        private Rigidbody2D rb;

        private bool isShoot;
        private Vector2 dir;
        private Vector2 lastVelocity;

        int totalHitWall;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();    
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
            if (collision.gameObject.CompareTag(TagUtils.Wall))
            {
                if (totalHitWall >= destroyWhenHitWall) OnBulletEnded();
                else
                {
                    Vector2 wallNormal = collision.contacts[0].normal;
                    dir = Vector2.Reflect(lastVelocity.normalized, wallNormal);

                    rb.velocity = dir * speed;
                    totalHitWall++;
                } 
            }
        }

        /// <summary>
        /// di panggil ketika peluru sudah melebihi total maksimal pantulan ke dinding
        /// </summary>
        private void OnBulletEnded()
        {
            Destroy(gameObject);
        }
    }
}
