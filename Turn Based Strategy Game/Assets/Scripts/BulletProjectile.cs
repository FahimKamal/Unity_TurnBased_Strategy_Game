using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVFXPrefab;
    private Vector3 _targetPosition;
    public void Setup(Vector3 targetPos){
        _targetPosition = targetPos;
    }

    private void Update(){
        var moveDir = (_targetPosition - transform.position).normalized;
        
        // Speed of bullet trail is very fast. Because of that to make the trail go accurate path.
        // Calculate distance before and after moving. Them destroy the game object. 
        var distanceBeforeMoving = Vector3.Distance(transform.position, _targetPosition);
        var moveSpeed = 100f;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);
        var distanceAfterMoving = Vector3.Distance(transform.position, _targetPosition);
        
        if (distanceBeforeMoving < distanceAfterMoving){
            // So trail wouldn't go more then target. 
            transform.position = _targetPosition;
            trailRenderer.transform.parent = null;

            Instantiate(bulletHitVFXPrefab, _targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
