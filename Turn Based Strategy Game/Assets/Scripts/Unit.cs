using System;
using Grid;
using UnityEngine;

public class Unit : MonoBehaviour{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    [SerializeField] private Animator animator;
    private Vector3 _targetPosition;

    private GridPosition _currentGridPosition;

    private void Awake(){
        _targetPosition = transform.position;
    }

    private void Start(){
        
        _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(_currentGridPosition, this);
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
        
        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != _currentGridPosition){
            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPos){
        _targetPosition = targetPos;
    }
}
