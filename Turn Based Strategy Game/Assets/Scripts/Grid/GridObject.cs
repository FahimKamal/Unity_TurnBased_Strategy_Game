namespace Grid{
    public class GridObject{
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;
        private Unit _unit;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition){
            _gridSystem = gridSystem;
            _gridPosition = gridPosition;
        }

        public override string ToString(){
            return _gridPosition.ToString() + "\n" + _unit;
        }
    
        public void SetUnit(Unit  unit){
            _unit = unit;
        }

        public Unit GetUnit(){
            return _unit;
        }
    }
}