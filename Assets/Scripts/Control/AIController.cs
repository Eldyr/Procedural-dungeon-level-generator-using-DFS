using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InfiniteLabyrinth.Combat;
using InfiniteLabyrinth.Core;
using InfiniteLabyrinth.Movement;
using InfiniteLabyrinth.PlayerResources;


namespace InfiniteLabyrinth.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 15f;
        [SerializeField] float suspicionTime = 5f;

        Fighter fighter;
        GameObject player;
        Health health;

        Mover mover;

        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity; 

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardPosition = transform.position;

        }

        private void Update()
        {
            if(health.IsDead()) return;

           if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaiour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaiour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }
                  
        
    }
}
