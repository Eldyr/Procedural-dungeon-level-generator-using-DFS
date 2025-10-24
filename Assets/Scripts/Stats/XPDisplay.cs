using UnityEngine;
using UnityEngine.UI;
using System;

namespace InfiniteLabyrinth.Stats
{
    public class XPDisplay : MonoBehaviour 
    {
        Expirience expirience;
        private void Awake() {

           
            expirience = GameObject.FindWithTag("Player").GetComponent<Expirience>();
        }

        private void Update() {
             
             
             GetComponent<Text>().text = String.Format("{0:0}", expirience.GetPoints());  
        }

        

    }
}