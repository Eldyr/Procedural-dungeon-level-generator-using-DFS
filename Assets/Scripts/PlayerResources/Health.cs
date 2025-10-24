using UnityEngine;
using InfiniteLabyrinth.Stats;
using InfiniteLabyrinth.Core;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace InfiniteLabyrinth.PlayerResources
{
    public class Health : MonoBehaviour
    {
    
    [SerializeField] float regenerationPercentage = 70;

    [SerializeField] TakeDamageEvent takeDamage;

    [System.Serializable]
    public class TakeDamageEvent : UnityEvent<float>
    {
    }

    

    float healthPoints = -1f;

    bool isDead = false;

    private void Start() 
    {
        if(healthPoints <0)
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        
    }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);

            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        healthPoints = Mathf.Max( healthPoints - damage, 0);
        if(healthPoints == 0)
            {
                Die(); 
                AwardExpirience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public void Heal(float healthToRestore)
        {
            healthPoints = Mathf.Min( healthPoints + healthToRestore, GetMaxHeathPoints());
        }

        public float GetMaxHeathPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }     

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
        }
    
    
        private void Die()
        {
            if(isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
            
        }

        
        

        private void AwardExpirience(GameObject instigator)
        {
            Expirience expirience = instigator.GetComponent<Expirience>();
            if(expirience == null) return;
            expirience.GainXP(GetComponent<BaseStats>().GetStat(Stat.XPReward));
        }
        
    }
}