using System;

namespace Controllers.SceneController.SpherePlacer.TablePositionFinder
{
    public class TableCell<T> : ICloneable
    {
        public int RowIndex;
        public int ColumnIndex;
        public T Value;

        public TableCell(int rowIndex, int columnIndex, T value)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Value = value;
        }

        public object Clone()
        {
            return new TableCell<T>(RowIndex, ColumnIndex, Value);
        }
    }
}