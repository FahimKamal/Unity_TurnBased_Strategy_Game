using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Actions{
    public class MoveAction : BaseAction
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int maxMoveDistance = 4;
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
        private Vector3 _targetPosition;

        protected override void Awake(){
            base.Awake();
            _targetPosition = transform.position;
        }

        public override string GetActionName(){
            return "Move";
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete){
            OnActionComplete = onActionComplete;
            _targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
            IsActive = true;
        }

        private void Update(){
            if (!IsActive) return;
        
            const float stoppingDistance = 0.1f;

            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
                var moveDirection = (_targetPosition - transform.position).normalized;
                var moveSpeed = 4.0f;
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);

                var rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
            
                animator.SetBool(IsWalking, true);
            }
            else{
                animator.SetBool(IsWalking, false);
                IsActive = false;
                OnActionComplete();
            }
        }

        /// <summary>
        /// Move to a given world location.
        /// </summary>
        /// <param name="targetPos"></param>
        public void Move(Vector3 targetPos){
            _targetPosition = targetPos;
            IsActive = true;
        }

        /// <summary>
        /// Move the a given valid gridPosition.
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <param name="onActionComplete"></param>
        // public void Move(GridPosition gridPosition, Action onActionComplete){
        //     
        // }

        /// <summary>
        /// Get the list of valid gridPosition where unit can move to.
        /// </summary>
        /// <returns>List of valid GridPosition</returns>
        public override List<GridPosition> GetValidActionGridPositionList(){
            var validGridPositionList = new List<GridPosition>();

            var unitGridPosition = ParentUnit.GetGridPosition();
        
            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++){
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++){
                    var offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                
                    // Checks if the gridPosition is valid. If not valid then skip it.
                    if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    // Skip present gridPosition of the unit.
                    if (unitGridPosition == testGridPosition){
                        continue;
                    }
                
                    // Skip if the target grid already has any unit on it.
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)){
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
