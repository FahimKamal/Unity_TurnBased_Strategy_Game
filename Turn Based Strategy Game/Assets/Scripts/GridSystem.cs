
using UnityEngine;

public class GridSystem{
    private int _width;
    private int _height;
    private float _cellSize;
    private GridObject[,] _gridObjectArray;
    
    public GridSystem(int width, int height, float cellSize){
        _width = width;
        _height = height;
        _cellSize = cellSize;

        for (int x = 0; x < width; x++){
            for (int z = 0; z < height; z++){
                // Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000f);
                var gridPosition = new GridPosition(x, z);
                new GridObject(this, gridPosition);
            }
        }
        
        
    }

    public Vector3 GetWorldPosition(int x, int z){
        return  new Vector3(x, 0, z) * _cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition){
            return new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize), 
                Mathf.RoundToInt(worldPosition.z / _cellSize)
                );
    }
}
