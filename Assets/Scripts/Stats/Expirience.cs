using UnityEngine;
using System;

namespace InfiniteLabyrinth.Stats
{
    public class Expirience : MonoBehaviour {
        [SerializeField] float expiriencePoints = 0;

        public event Action  onExperieceGained;

        public void GainXP(float expirience)
        {
            expiriencePoints += expirience;
            onExperieceGained();
        }
        
        public float GetPoints()
        {
            return expiriencePoints;
        }

        

        
    }
}