using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteLabyrinth.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 25)]
        [SerializeField] int startingLevel = 1;
        
        [SerializeField] CharacterClass characterClass;

        [SerializeField] Progression progression = null;
        
        [SerializeField] GameObject levelUpParticleEffect = null;

        public event Action onLevelUp;

        int currentLevel = 0;

        private void Start() {
            currentLevel = CalculateLevel();
            Expirience expirience = GetComponent<Expirience>();
            if(expirience != null)
            {
                expirience.onExperieceGained += UpdateLevel;
            }

        }

        private void UpdateLevel() {
            int newLevel = CalculateLevel ();
            if(newLevel >  currentLevel)
            {
                currentLevel = newLevel;
                print("Levelled Up!");
                LevelUpEffect();
                onLevelUp();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if(currentLevel <1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }
        private int CalculateLevel()
        {
            Expirience expirience = GetComponent<Expirience>();

            if(expirience == null) return startingLevel;
            float currentXP = expirience.GetPoints();
            
            int penultimateLevel = progression.GetLevels(Stat.PlayerXp, characterClass);
            
            for (int level = 1; level <= penultimateLevel; level++ )
            {
               float XPToLevelUp =  progression.GetStat(Stat.PlayerXp, characterClass, level);

                if(XPToLevelUp > currentXP ){
                    return level;
                }
            }
            return penultimateLevel + 1;
        }
    }

}