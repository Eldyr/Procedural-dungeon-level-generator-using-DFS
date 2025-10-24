using UnityEngine;
using UnityEngine.UI;
using System;

namespace InfiniteLabyrinth.Stats
{
    public class PlayerLevelDisplay : MonoBehaviour 
    {
        
        BaseStats baseStats;
        private void Awake() {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();

           
        }

        private void Update() {
             
             
             GetComponent<Text>().text = String.Format("{0:0}", baseStats.GetLevel());  
        }

        

    }
}