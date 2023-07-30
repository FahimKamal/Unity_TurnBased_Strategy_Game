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
    
        private void Update(){
            if (!IsActive) return;

            _stateTimer -= Time.deltaTime;

            switch (_state){
                case State.Aiming:
                    if (_stateTimer <= 0f){
                        _state = State.Shooting;
                        const float shootingStateTime = 0.1f;
                        _stateTimer = shootingStateTime;
                    }
                    break;
                case State.Shooting:
                    if (_stateTimer <= 0f){
                        _state = State.Cooloff;
                    }
                    break;
                case State.Cooloff:
                    break;
            }
        }
        public override string GetActionName(){
            return "Shoot";
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete){
            OnActionComplete = onActionComplete;
            IsActive = true;
            // _totalSpinAmount = 0f;
            Debug.Log("Talking Shoot action.");
        }

        /// <summary>
        /// Get the list of valid gridPosition where unit can shoot to.
        /// </summary>
        /// <returns>List of valid GridPosition</returns>
        public override List<GridPosition> GetValidActionGridPositionList(){
            var validGridPositionList = new List<GridPosition>();

            var unitGridPosition = Unit.GetGridPosition();
        
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
                    if(targetUnit.IsEnemy == Unit.IsEnemy){
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
