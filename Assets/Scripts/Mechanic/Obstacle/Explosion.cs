using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacle
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private float radius = 2;
        [SerializeField] private float power = 200;

        // collider hanya digunakan agar semua objek lain dapat mengetahui sedang terkena ledakan atau tidak melalui onCollisionEnter2D nya masing masing
        private CircleCollider2D cCollider;

        private void Awake()
        {
            cCollider = GetComponent<CircleCollider2D>();

            // collider di matikan agar tidak ada interaksi objek ketika belum terjadi ledakan
            cCollider.enabled = false;
            cCollider.radius = radius;
        }

        /// <summary>
        /// digunakan untuk menjalankan ledakan
        /// </summary>
        public void Launch()
        {
            // semua method OnCollision2D objek yang berada di area ledakan akan di panggil
            cCollider.enabled = true;

            // semua objek yang berada di area ledakan
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            // menerbangkan semua objek yang berada di area ledakan
            foreach (Collider2D hit in colliders)
            {   
                Vector2 direction = hit.transform.position - transform.position;
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

                if(rb != null) rb.AddForce(direction * power);
            }

            Destroy(gameObject,0.1f);
        }


        /// <summary>
        /// di gunakan untuk menampilkan area ledakan di editor
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

}