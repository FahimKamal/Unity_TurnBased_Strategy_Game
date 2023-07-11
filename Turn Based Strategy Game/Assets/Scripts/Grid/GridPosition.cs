
using System;

namespace Grid{
    public struct GridPosition : IEquatable<GridPosition>
    {
        public int X;
        public int Z;

        public GridPosition(int x, int z){
            X = x;
            Z = z;
        }

        public override string ToString(){
            return "X: " + X + "; Z: " + Z;
        }
        
        public bool Equals(GridPosition other){
            return X == other.X && Z == other.Z;
        }

        public override bool Equals(object obj){
            return obj is GridPosition other && Equals(other);
        }

        public override int GetHashCode(){
            return HashCode.Combine(X, Z);
        }

        public static bool operator ==(GridPosition a, GridPosition b){
            return  a.X == b.X && a.Z == b.Z;
        }

        public static bool operator !=(GridPosition a, GridPosition b){
            return !(a == b);
        }
    }
}