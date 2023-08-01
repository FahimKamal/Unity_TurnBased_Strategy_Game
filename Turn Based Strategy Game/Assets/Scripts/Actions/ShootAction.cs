using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions{
    public class ShootAction : BaseAction
    {
        private enum State{
            Aiming, Shooting, Cooloff
        }

        private State _state;
        private int _maxShootDistance = 7;
        private float _stateTimer;
        private Unit _targetUnit;
    
        private void Update(){
            if (!IsActive) return;

            _stateTimer -= Time.deltaTime;

            if (_stateTimer <= 0f){
                NextState();
            }
        }

        private void NextState(){
            switch (_state){
                case State.Aiming:
                    _state = State.Shooting;
                    const float shootingStateTime = 0.1f;
                    _stateTimer = shootingStateTime;
                    break;
                case State.Shooting:
                    _state = State.Cooloff;
                    var collOffStateTime = 0.5f;
                    _stateTimer = collOffStateTime;
                    break;
                case State.Cooloff:
                    IsActive = false;
                    OnActionComplete();
                    break;
            }
            Debug.Log(_state);
        }
        public override string GetActionName(){ 
            return "Shoot";
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete){
            OnActionComplete = onActionComplete;
            IsActive = true;
            Debug.Log("Talking Shoot action.");

            _state = State.Aiming;
            var aimingStateTime = 1f;
            _stateTimer = aimingStateTime;
        }

        /// <summary>
        /// Get the list of valid gridPosition where unit can shoot to.
        /// </summary>
        /// <returns>List of valid GridPosition</returns>
        public override List<GridPosition> GetValidActionGridPositionList(){
            var validGridPositionList = new List<GridPosition>();

            var unitGridPosition = ParentUnit.GetGridPosition();
        
            for (int x = -_maxShootDistance; x <= _maxShootDistance; x++){
                for (int z = -_maxShootDistance; z <= _maxShootDistance; z++){
                    var offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                
                    // Checks if the gridPosition is valid. If not valid then skip it.
                    if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition)){
                        continue;
                    }
                    
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                    if (testDistance > _maxShootDistance){
                        continue;
                    }
                    
                    // validGridPositionList.Add(testGridPosition);
                    // continue;

                    // Skip if the target grid doesn't has any unit on it.
                    if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)){
                        continue;
                    }
                    
                    // Skip if the target unit is same as the unit. Both of them is in the same team.
                    var targetUnit = LevelGrid.Instance.GetUnitOnGridPosition(testGridPosition);
                    if(targetUnit.IsEnemy == ParentUnit.IsEnemy){
                        continue;
                    }
                
                    // Grid position is valid put it in list.
                    validGridPositionList.Add(testGridPosition);
                    // Debug.Log(testGridPosition);
                }
            
            }
        
            return validGridPositionList;
        }
    }
}
