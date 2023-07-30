using System.Collections.Generic;

namespace Grid{
    public class GridObject{
        private GridSystem _gridSystem;
        private GridPosition _gridPosition;
        private List<Unit> _unitList;

        public GridObject(GridSystem gridSystem, GridPosition gridPosition){
            _gridSystem = gridSystem;
            _gridPosition = gridPosition;
            _unitList = new List<Unit>();
        }

        public override string ToString(){
            var unitString = "";
            foreach (var unit in _unitList){ 
                unitString += unit + "\n"; 
            }

            return _gridPosition.ToString() + "\n" + unitString;
        }
    
        public void AddUnit(Unit  unit){
            _unitList.Add(unit);
        }

        public List<Unit> GetUnitList(){
            return _unitList;
        }

        public void RemoveUnit(Unit unit){
            _unitList.Remove(unit);
        }

        public bool HasAnyUnit() => _unitList.Count > 0;

        public Unit GetUnit() => HasAnyUnit() ? _unitList[0] : null;
    }
}