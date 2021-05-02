using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class RopeBox : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D triggerPoint;
        private CapsuleCollider2D cCollider;

        private void Awake()
        {
            cCollider = GetComponent<CapsuleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                triggerPoint.enabled = false;
                cCollider.enabled = false;
            }
        }
    }
}
