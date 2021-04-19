using UnityEngine;

namespace Controllers.SceneController.SpherePlacer.TablePositionFinder
{
    public class TablePositionFinder : IPositionFinder
    {
        private readonly Table<Vector2> _table;
        private readonly TablePositionFinderSettings _settings;

        public TablePositionFinder(int elementsCount, Vector2 leftTopPosition)
        {
            _settings = Resources.Load<TablePositionFinderSettings>("TablePositionFinderSettings");
            int rowsCount = elementsCount / _settings.ElementsInARow;
            int columnsCount = rowsCount > 0 ? _settings.ElementsInARow : elementsCount;
            rowsCount++;
            _table = new Table<Vector2>(rowsCount, columnsCount);
            Vector2 currentPosition = leftTopPosition;

            for (int i = 0; i < _table.RowsCount; i++)
            {
                for (int j = 0; j < _table.ColumnsCount; j++)
                {
                    _table.GetCell(i, j).Value = currentPosition;
                    currentPosition += new Vector2(_settings.Spacing.x, 0);
                }

                currentPosition = new Vector2(leftTopPosition.x, currentPosition.y + _settings.Spacing.y);
            }
        }

        public Vector2 GetPosition(int sphereNumber)
        {
            int rowIndex = sphereNumber / _table.ColumnsCount;
            int columnIndex = sphereNumber % _table.ColumnsCount;
            return _table.GetCell(rowIndex, columnIndex).Value;
        }
    }
}