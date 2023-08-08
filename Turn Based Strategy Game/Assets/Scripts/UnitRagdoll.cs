using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    public void Setup(Transform originalRootBone){
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);
    }

    private void MatchAllChildTransforms(Transform root, Transform clone){
        foreach (Transform child in root){
            var cloneChild = child.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;
                
                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    private void ApplyExplosionToRagdoll(Transform root){
        foreach (Transform child in root){
            if (child.TryGetComponent<Rigidbody>(out var rigidbody)){
                
            }
        }
    }
}
