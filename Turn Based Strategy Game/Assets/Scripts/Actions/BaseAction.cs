﻿using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions{
    public abstract class BaseAction : MonoBehaviour{
        protected Unit ParentUnit;
        protected bool IsActive;
        protected Action OnActionComplete;

        protected virtual void Awake(){
            ParentUnit = GetComponent<Unit>();
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

        /// <summary>
        /// Points Casts to execute each action. Can be different for each actions by
        /// Overwriting this method. 
        /// </summary>
        /// <returns></returns>
        public virtual int GetActionPointsCost(){
            return 1;
        }
        
        /// <summary>
        /// Call This method at the start of taking any action.
        /// </summary>
        /// <param name="onActionComplete"></param>
        protected void ActionStart(Action onActionComplete){
            IsActive = true;
            this.OnActionComplete = onActionComplete;
        }

        /// <summary>
        /// Call this method at the end of each action.
        /// </summary>
        protected void ActionComplete(){
            IsActive = false;
            OnActionComplete();
        }
    }
}