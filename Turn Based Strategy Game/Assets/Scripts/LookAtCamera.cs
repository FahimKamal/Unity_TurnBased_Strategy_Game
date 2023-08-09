using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour{
    [SerializeField] private bool invert;
    private Transform _cameraTransform;
    private void Awake(){
        _cameraTransform = Camera.main.transform;
    }

    private void LateUpdate(){
        if (invert){
            Vector3 dirToCamera = (_cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + dirToCamera * -1);
        }
        else{
            transform.LookAt(_cameraTransform);
        }
    }
}
