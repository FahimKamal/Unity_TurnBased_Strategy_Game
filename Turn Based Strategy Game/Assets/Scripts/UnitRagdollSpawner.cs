using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour{
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBone;
    private HealthSystem _healthSystem;

    private void Awake(){
        _healthSystem = GetComponent<HealthSystem>();
        
        _healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e){
        var ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        ragdollTransform.GetComponent<UnitRagdoll>().Setup(originalRootBone);
    }
}
