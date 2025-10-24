using System.Collections;
using System.Collections.Generic;

using InfiniteLabyrinth.Core;
using UnityEngine;
using UnityEngine.AI;
using InfiniteLabyrinth.PlayerResources;

namespace InfiniteLabyrinth.Movement
{   
 public class Mover : MonoBehaviour, IAction
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;

    Health health;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        navMeshAgent.enabled = !health.IsDead();
        
        UpdateAnimator();
    }
    
    public void StartMoveAction(Vector3 destination)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        Moveto(destination);
    }

    public void Moveto(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;

    }
    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }

    
    private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localvelocity = transform.InverseTransformDirection(velocity);
            float speed = localvelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        
}   
}
 