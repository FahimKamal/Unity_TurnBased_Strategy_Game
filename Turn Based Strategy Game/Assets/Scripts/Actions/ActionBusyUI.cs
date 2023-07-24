using System;
using UnityEngine;

namespace Actions{
    public class ActionBusyUI : MonoBehaviour
    {

        private void Start(){
            gameObject.SetActive(false);
        }

        private void OnEnable(){
            UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
        }

        private void UnitActionSystem_OnBusyChanged(object sender, bool e){
            Debug.Log("I'm here");
            gameObject.SetActive(e);
        }
    }
}
