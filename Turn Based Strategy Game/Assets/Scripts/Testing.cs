using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;

public class Testing : MonoBehaviour{

    [SerializeField] private Transform gridDebugObjectPrefab;
    
    private GridSystem _gridSystem;
    private void Start(){
        _gridSystem = new GridSystem(10, 10, 2f);
        _gridSystem.CreateDebugObjects(gridDebugObjectPrefab, transform);
    }

    private void Update(){
        Debug.Log(_gridSystem.GetGridPosition(MouseWorld.GetPosition()));
        
    }
}
