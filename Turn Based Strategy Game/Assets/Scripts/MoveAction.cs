using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int maxMoveDistance = 4;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    private Vector3 _targetPosition;
    private Unit _unit;
    
    private void Awake(){
        _unit = GetComponent<Unit>();
        _targetPosition = transform.position;
    }

    private void Update(){
        var stoppingDistance = 0.1f;
        
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
        }
    }

    public void Move(Vector3 targetPos){
        _targetPosition = targetPos;
    }

    public List<GridPosition> GetValidActionGridPositionList(){
        var validGridPositionList = new List<GridPosition>();

        var unitGridPosition = _unit.GetGridPosition();
        
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++){
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++){
                var offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
            }
            
        }
        
        return validGridPositionList;
    }
}
