using UnityEngine;
using InfiniteLabyrinth.Movement;
using InfiniteLabyrinth.Core;
using InfiniteLabyrinth.PlayerResources;
using InfiniteLabyrinth.Stats;
using UnityEngine.Events;

namespace InfiniteLabyrinth.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 0.75f;

        Health target;

        [SerializeField] UnityEvent doDamage;


        float timeSinceLastAttack = Mathf.Infinity;

        
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if(target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<Mover>().Moveto(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour(); 
            }
        }

        public Health GetTarget()
        {
            return target;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;

            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        

        //Animation Event
        void Hit()
        { 
            if(target == null) return;

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);

            doDamage.Invoke();


            
            target.TakeDamage(gameObject, damage); 
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject enemyTarget)
        {
            if(enemyTarget == null)
            {
                return false; 
            }
            Health targetToTest = enemyTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack(); 
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

    }
}
