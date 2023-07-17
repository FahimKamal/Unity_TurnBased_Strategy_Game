using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;

public class Testing : MonoBehaviour{

    [SerializeField] private Unit unit;
    private void Start(){
        
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.T)){
            unit.GetMoveAction().GetValidActionGridPositionList();
        }

    }
}
