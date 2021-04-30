using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Explosion : MonoBehaviour
{
    public float radius = 2;
    public float power = 100;

    private CircleCollider2D cCollider;

    private void Start()
    {
        cCollider = GetComponent<CircleCollider2D>();
        cCollider.enabled = false;
        cCollider.radius = radius;
    }

    public void Launch()
    {
        cCollider.enabled = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in colliders)
        {
            Vector2 direction = hit.transform.position - transform.position;

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if(rb != null) rb.AddForce(direction * power);
        }

        Destroy(gameObject,0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
