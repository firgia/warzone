using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacle
{
    public class RopeBox : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D triggerPoint;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(TagUtils.Bullet))
            {
                triggerPoint.enabled = false;
            }
        }
    }
}
