using TMPro;
using UnityEngine;

namespace Grid{
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
}
