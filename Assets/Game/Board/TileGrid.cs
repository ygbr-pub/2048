namespace PH.Game
{
    using UnityEngine;

    public class TileGrid : MonoBehaviour
    {
        public TileRow[] Rows { get; private set; }
        public TileCell[] Cells { get; private set; }

        public int Size => Cells.Length;
        public int Height => Rows.Length;
        public int Width => Size / Height;

        private void Awake()
        {
            Rows = GetComponentsInChildren<TileRow>();
            Cells = GetComponentsInChildren<TileCell>();

            for (var i = 0; i < Cells.Length; i++) 
                Cells[i].Coordinates = new(i % Width, i / Width);
        }

        private TileCell GetCell(Vector2Int coordinates) => GetCell(coordinates.x, coordinates.y);

        public TileCell GetCell(int x, int y)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
                return Rows[y].Cells[x];
            return null;
        }

        public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
        {
            var coordinates = cell.Coordinates;
            coordinates.x += direction.x;
            coordinates.y -= direction.y;

            return GetCell(coordinates);
        }

        public TileCell GetRandomEmptyCell()
        {
            var index = Random.Range(0, Cells.Length);
            var startingIndex = index;

            while (Cells[index].Occupied)
            {
                index++;

                if (index >= Cells.Length) 
                    index = 0;

                // all cells are occupied
                if (index == startingIndex)
                    return null;
            }

            return Cells[index];
        }
    }
}