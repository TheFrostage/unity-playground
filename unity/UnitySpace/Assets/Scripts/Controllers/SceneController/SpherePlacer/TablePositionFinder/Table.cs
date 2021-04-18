using System;
using UnityEngine;

namespace Controllers.SceneController.SpherePlacer.TablePositionFinder
{
    public class Table<T>
    {
        public readonly int RowsCount;
        public readonly int ColumnsCount;
        private readonly TableCell<T>[,] _cells;

        public Table(int rowsCount, int columnsCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            _cells = new TableCell<T>[rowsCount, columnsCount];

            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    _cells[i, j] = new TableCell<T>(i, j, default);
                }
            }
        }

        public void SetCell(int rowIndex, int columnIndex, TableCell<T> tableCell)
        {
            CheckRowAndIndexIndex(rowIndex, columnIndex);
            _cells[rowIndex, columnIndex] = tableCell;
        }

        public void ClearCell(int rowIndex, int columnIndex)
        {
            CheckRowAndIndexIndex(rowIndex, columnIndex);
            _cells[rowIndex, columnIndex] = null;
        }

        public TableCell<T> GetCell(int rowIndex, int columnIndex)
        {
            CheckRowAndIndexIndex(rowIndex, columnIndex);
            return _cells[rowIndex, columnIndex];
        }

        public TableCell<T> GetCellCopy(int rowIndex, int columnIndex)
        {
            CheckRowAndIndexIndex(rowIndex, columnIndex);
            return (TableCell<T>) _cells[rowIndex, columnIndex].Clone();
        }

        private void CheckRowAndIndexIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex >= RowsCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "RowIndex is more than rows count");
            }

            if (columnIndex >= ColumnsCount)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "RowIndex is more than rows count");
            }
        }
    }
}