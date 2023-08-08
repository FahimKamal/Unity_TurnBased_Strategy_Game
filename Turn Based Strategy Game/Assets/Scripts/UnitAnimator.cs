using System;
using Actions;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;
    
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    private void Awake(){
        if (TryGetComponent<MoveAction>(out var moveAction)){
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out var shootAction)){
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e){
        animator.SetBool(IsWalking, true);
    }
    
    private void MoveAction_OnStopMoving(object sender, EventArgs e){
        animator.SetBool(IsWalking, false);
    }
    
    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e){
        animator.SetTrigger(Shoot);
        
        var bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity, this.transform);
        var bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        var targetUnitShootAtPosition = e.TargetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = shootPointTransform.position.y;
        bulletProjectile.Setup(targetUnitShootAtPosition);
    }
}
