using System;
using UnityEngine;

public class Unit : MonoBehaviour{
    [SerializeField] private Animator animator;
    private Vector3 _targetPosition;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Awake(){
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
}
