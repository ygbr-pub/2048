namespace PH.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using Input;
    using UnityEngine;

    public class TileBoard : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private TileState[] tileStates;

        private TileGrid _grid;
        private List<Tile> _tiles;
        private bool _waiting;

        private void Awake()
        {
            _grid = GetComponentInChildren<TileGrid>();
            _tiles = new(16);
        }

        private void OnEnable()
        {
            InputManager.OnSwipeUp += OnSwipeUp;
            InputManager.OnSwipeDown += OnSwipeDown;
            InputManager.OnSwipeRight += OnSwipeRight;
            InputManager.OnSwipeLeft += OnSwipeLeft;
        }
        
        private void OnDisable()
        {
            InputManager.OnSwipeUp -= OnSwipeUp;
            InputManager.OnSwipeDown -= OnSwipeDown;
            InputManager.OnSwipeRight -= OnSwipeRight;
            InputManager.OnSwipeLeft -= OnSwipeLeft;
        }
        
        private void OnSwipeUp() => Move(Vector2Int.up, 0, 1, 1, 1);
        private void OnSwipeDown() => Move(Vector2Int.down, 0, 1, _grid.Height - 2, -1);
        private void OnSwipeRight() => Move(Vector2Int.right, _grid.Width - 2, -1, 0, 1);
        private void OnSwipeLeft() => Move(Vector2Int.left, 1, 1, 0, 1);
        
        public void ClearBoard()
        {
            foreach (var cell in _grid.Cells) 
                cell.Tile = null;

            foreach (var tile in _tiles) 
                Destroy(tile.gameObject);

            _tiles.Clear();
        }

        public void CreateTile()
        {
            var tile = Instantiate(tilePrefab, _grid.transform);
            tile.SetState(tileStates[0]);
            tile.Spawn(_grid.GetRandomEmptyCell());
            _tiles.Add(tile);
        }

        private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
        {
            if (_waiting)
                return;
            
            var changed = false;
            for (var x = startX; x >= 0 && x < _grid.Width; x += incrementX)
            {
                for (var y = startY; y >= 0 && y < _grid.Height; y += incrementY)
                {
                    var cell = _grid.GetCell(x, y);
                    if (cell.Occupied) 
                        changed |= MoveTile(cell.Tile, direction);
                }
            }

            if (changed) 
                StartCoroutine(WaitForChanges());
        }

        private bool MoveTile(Tile tile, Vector2Int direction)
        {
            TileCell newCell = null;
            var adjacent = _grid.GetAdjacentCell(tile.Cell, direction);

            while (adjacent != null)
            {
                if (adjacent.Occupied)
                {
                    if (CanMerge(tile, adjacent.Tile))
                    {
                        MergeTiles(tile, adjacent.Tile);
                        return true;
                    }
                    break;
                }

                newCell = adjacent;
                adjacent = _grid.GetAdjacentCell(adjacent, direction);
            }

            if (newCell == null) 
                return false;
            
            tile.MoveTo(newCell);
            return true;
        }

        private static bool CanMerge(Tile a, Tile b) => a.State == b.State && !b.Locked;

        private void MergeTiles(Tile a, Tile b)
        {
            _tiles.Remove(a);
            a.Merge(b.Cell, OnMerge);

            void OnMerge()
            {
                const int minIndex = 0;
                var maxIndex = tileStates.Length - 1;
                var index = Mathf.Clamp(IndexOf(b.State) + 1, minIndex, maxIndex);
                var newState = tileStates[index];

                b.SetState(newState);
                GameManager.Instance.IncreaseScore(newState.number);
            }
        }

        private int IndexOf(TileState state)
        {
            for (var i = 0; i < tileStates.Length; i++)
                if (state == tileStates[i])
                    return i;
            return -1;
        }

        private IEnumerator WaitForChanges()
        {
            _waiting = true;
            yield return new WaitForSeconds(0.1f);
            _waiting = false;

            foreach (var tile in _tiles) 
                tile.Locked = false;

            if (_tiles.Count != _grid.Size) 
                CreateTile();

            if (CheckForGameOver()) 
                GameManager.Instance.ShowGameOver();
        }

        private bool CheckForGameOver()
        {
            if (_tiles.Count != _grid.Size)
                return false;

            foreach (var tile in _tiles)
            {
                var up = _grid.GetAdjacentCell(tile.Cell, Vector2Int.up);
                var down = _grid.GetAdjacentCell(tile.Cell, Vector2Int.down);
                var left = _grid.GetAdjacentCell(tile.Cell, Vector2Int.left);
                var right = _grid.GetAdjacentCell(tile.Cell, Vector2Int.right);

                if (up != null && CanMerge(tile, up.Tile))
                    return false;

                if (down != null && CanMerge(tile, down.Tile))
                    return false;

                if (left != null && CanMerge(tile, left.Tile))
                    return false;

                if (right != null && CanMerge(tile, right.Tile))
                    return false;
            }

            return true;
        }

    }
    
}