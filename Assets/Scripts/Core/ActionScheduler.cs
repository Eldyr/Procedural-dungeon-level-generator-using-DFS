using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteLabyrinth.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction curremtAction;
        public void StartAction(IAction action)
        {   
            if(curremtAction == action) return;
            if(curremtAction != null)
            {
                curremtAction.Cancel();

            }

            curremtAction = action;

        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}

