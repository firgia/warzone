using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
public class GiantRockController : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField] private ParticleSystem particleDestroyPrefab;

    private int destroyWhenHit = 3;

    private int totalHit = 0;

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
