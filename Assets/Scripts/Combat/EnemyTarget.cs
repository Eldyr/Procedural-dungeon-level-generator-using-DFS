using UnityEngine;
using InfiniteLabyrinth.PlayerResources;
using InfiniteLabyrinth.Control;

namespace InfiniteLabyrinth.Combat
{
    [RequireComponent(typeof(Health))]
    public class EnemyTarget : MonoBehaviour,  IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
                          
                if(!callingController.GetComponent<Fighter>().CanAttack(gameObject))
                {
                    return false;
                }

                if(Input.GetMouseButton(0))
                {
                    callingController.GetComponent<Fighter>().Attack(gameObject);
                   
                }
                return true;
        }
    }
}  