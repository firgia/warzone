using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace AI
{
    public class CommanderEnemy : Enemy
    {
        [SerializeField] private Animator expressionAnim;

        private void Update()
        {
            expressionAnim.SetBool("Death", IsDead);
        }
    }
}