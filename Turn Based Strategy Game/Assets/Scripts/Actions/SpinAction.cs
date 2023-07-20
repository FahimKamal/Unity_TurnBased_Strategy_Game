using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions{
    public class SpinAction : BaseAction{
        
        private float _totalSpinAmount;

        private void Update(){
            if (!IsActive) return;
        
            var spinAddAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            _totalSpinAmount += spinAddAmount;
            if (_totalSpinAmount >= 360f){
                IsActive = false;
                OnActionComplete();
            }
        }

        // public void Spin(Action onSpinComplete){
        //     
        // }

        public override string GetActionName(){
            return "Spin";
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete){
            OnActionComplete = onActionComplete;
            IsActive = true;
            _totalSpinAmount = 0f;
            Debug.Log("Talking spin action.");
        }

        public override List<GridPosition> GetValidActionGridPositionList(){
            var unitGridPosition = Unit.GetGridPosition();
            return new List<GridPosition>{
                unitGridPosition
            };
        }
    }
}
