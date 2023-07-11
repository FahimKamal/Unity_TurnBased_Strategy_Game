using UnityEngine;

namespace Grid{
    public class GridSystem{
        private int _width;
        private int _height;
        private float _cellSize;
        private GridObject[,] _gridObjectArray;
    
        public GridSystem(int width, int height, float cellSize){
            _width = width;
            _height = height;
            _cellSize = cellSize;
        
            _gridObjectArray = new GridObject[width, height];

            for (int x = 0; x < width; x++){
                for (int z = 0; z < height; z++){
                    // Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000f);
                    var gridPosition = new GridPosition(x, z);
                    _gridObjectArray[x, z] = new GridObject(this, gridPosition);
                }
            }
        
        
        }

        public Vector3 GetWorldPosition(GridPosition gridPosition){
            return new Vector3(gridPosition.X, 0, gridPosition.Z) * _cellSize;
        }

        public GridPosition GetGridPosition(Vector3 worldPosition){
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize), 
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        }

        public void CreateDebugObjects(Transform debugPrefab, Transform parent){
            for (int x = 0; x < _width; x++){
                for (int z = 0; z < _height; z++){
                    var gridPosition = new GridPosition(x, z);
                    var debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity,  parent);
                    var gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }

        public GridObject GetGridObject(GridPosition  gridPosition){
            return _gridObjectArray[gridPosition.X, gridPosition.Z];
        }
    }
}
