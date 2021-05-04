using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace AI
{
    [RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D))]
    public abstract class Enemy : MonoBehaviour
    {   
        [Header("Ragdoll")]
        [SerializeField] private GameObject[] bodyPart;

        [Header("Particle")]
        [SerializeField] private Transform particlePos;
        [SerializeField] private ParticleSystem particleDeathPrefab;

        private CapsuleCollider2D cCollider;
        private Rigidbody2D rb;

        private List<CapsuleCollider2D> listCCollider;
        private List<Rigidbody2D> listRb;

        private void Awake()
        {
            cCollider = GetComponent<CapsuleCollider2D>();
            rb = GetComponent<Rigidbody2D>();

            listCCollider = new List<CapsuleCollider2D>();
            listRb = new List<Rigidbody2D>();

            // list collider dan list rigidbody di gunakan untuk mematikan dan menjalankan ragdoll
            foreach(GameObject obj in bodyPart)
            {
                CapsuleCollider2D cc = obj.GetComponent<CapsuleCollider2D>();
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

                if(cc != null) listCCollider.Add(cc);
                if(rb != null) listRb.Add(rb);
            }
        }

        private void Start() => DisableRagdoll();
        
        #region Ragdoll
        /// <summary>
        /// mematikan effect ragdoll 
        /// </summary>
        public void DisableRagdoll()
        {
            rb.gravityScale = 1;

            cCollider.enabled = true;

            foreach(CapsuleCollider2D cc in listCCollider) cc.isTrigger = true;
            foreach (Rigidbody2D rb in listRb) rb.isKinematic = true;
        }

        /// <summary>
        /// menjalankan effect ragdoll
        /// </summary>
        public void EnableRagdoll()
        {
            rb.gravityScale = 0;
            cCollider.enabled = false;
            foreach (CapsuleCollider2D cc in listCCollider) cc.isTrigger = false;
            foreach (Rigidbody2D rb in listRb)  rb.isKinematic = false; 
        }
        #endregion

        #region Death
        /// <summary>
        /// di gunakan jika dibutuhkan efek physic sebelum musuh mati
        /// </summary>
        public IEnumerator DeathUsingRagdoll()
        {
            EnableRagdoll();
            yield return new WaitForSeconds(1.5f);
            Death();
        }

        /// <summary>
        /// digunakan ketika musuh mati
        /// </summary>
        public void Death()
        {
            Instantiate(particleDeathPrefab.gameObject, particlePos.position, Quaternion.identity);
            Destroy(gameObject);
        }
        #endregion

        #region Hit
        /// <summary>
        /// musuh hanya akan mati jika dia kena hit object dengan tag 'Bullet', 'SmasherObstacle', dan 'ExplosionObstacle'
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet) ||
                collision.gameObject.CompareTag(TagUtils.SmasherObstacle) ||
                collision.gameObject.CompareTag(TagUtils.Helicopter)
                )
            {
                StartCoroutine(DeathUsingRagdoll());
            }
            else if ( collision.gameObject.CompareTag(TagUtils.ExplosionObstacle))
            {
                Death();
            }
        }
        #endregion
    }
}