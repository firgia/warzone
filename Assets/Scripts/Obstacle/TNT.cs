using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Explosion))]
public class TNT : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private ParticleSystem particleExplosionPrefab;

    private Explosion explosion;

    private void Start()
    {
        explosion = GetComponent<Explosion>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagUtils.Bullet))
        {
            Instantiate(particleExplosionPrefab.gameObject,transform.position,Quaternion.identity);
            explosion.Launch();
        }
    }
}
