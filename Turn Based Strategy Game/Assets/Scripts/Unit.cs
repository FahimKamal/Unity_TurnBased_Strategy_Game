using System;
using UnityEngine;

public class Unit : MonoBehaviour{
    private Vector3 _targetPosition;

    private void Update(){
        var stoppingDistance = 0.1f;
        
        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance){
            var moveDirection = (_targetPosition - transform.position).normalized;
            var moveSpeed = 4.0f;
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0)){
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPos){
        _targetPosition = targetPos;
    }
}
