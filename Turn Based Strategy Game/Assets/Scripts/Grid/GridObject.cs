public class GridObject{
    private GridSystem _gridSystem;
    private GridPosition _gridPosition;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition){
        _gridSystem = gridSystem;
        _gridPosition = gridPosition;
    }

    public override string ToString(){
        return _gridPosition.ToString();
    }
}