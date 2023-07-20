using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions{
    public abstract class BaseAction : MonoBehaviour{
        protected Unit Unit;
        protected bool IsActive;
        protected Action OnActionComplete;

        protected virtual void Awake(){
            Unit = GetComponent<Unit>();
        }

        public abstract string GetActionName();
        
        public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
        
        /// <summary>
        /// Check if given gridPosition is a valid moveAction gridPosition or not.
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <returns></returns>
        public virtual bool IsValidActionGridPosition(GridPosition gridPosition){
            List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition); 
        }

        public abstract List<GridPosition> GetValidActionGridPositionList();
    }
}