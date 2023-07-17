using System;
using UnityEngine;

namespace Actions{
    public class SpinAction : BaseAction{
        
        private float _totalSpinAmount;

        private void Update(){
            if (!IsActive) return;
        
            var spinAddAmount = 360f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            _totalSpinAmount += spinAddAmount;
            if (_totalSpinAmount >= 360f){
                IsActive = false;
                OnActionComplete();
            }
        }

        public void Spin(Action onSpinComplete){
            OnActionComplete = onSpinComplete;
            IsActive = true;
            _totalSpinAmount = 0f;
        }
    }
}
