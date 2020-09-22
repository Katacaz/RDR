using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AggroDetection aggroDetection;
    private Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        aggroDetection = GetComponent<AggroDetection>();
        aggroDetection.OnAgrro += AggroDetection_OnAgrro;
    }

    private void AggroDetection_OnAgrro(Transform target)
    {
        this.target = target;
        
    }

    private void Update()
    {
        if (target != null)
        {
            //Debug.Log("Moving towards target: " + target.name);
            navMeshAgent.SetDestination(target.position);
            float speed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", speed);
        }
        
    }
}
