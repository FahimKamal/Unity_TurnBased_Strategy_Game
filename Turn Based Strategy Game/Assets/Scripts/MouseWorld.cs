using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MouseWorld : MonoBehaviour
{
    private static  MouseWorld instance;
    [SerializeField] private GameObject mouseCursor;
    [SerializeField] private LayerMask mouseCursorMask;

    private void Awake(){
        instance = this;
    }

    /// <summary>
    /// Returns the position of the place where the raycast is being hit.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetPosition(){
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var raycastHit,float.MaxValue, instance.mouseCursorMask );
        return raycastHit.point;
    }
}
