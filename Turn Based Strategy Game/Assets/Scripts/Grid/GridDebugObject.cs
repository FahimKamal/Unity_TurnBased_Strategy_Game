using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GridDebugObject : MonoBehaviour{
    private GridObject _gridObject;

    [SerializeField] private TextMeshPro textMeshPro;

    public void SetGridObject(GridObject gridObject){
        _gridObject = gridObject;
    }

    private void Update(){
        textMeshPro.text = _gridObject.ToString();
    }
}
