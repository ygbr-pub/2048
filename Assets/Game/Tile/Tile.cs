namespace PH.Game
{
    using System;
    using System.Collections;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    
    public class Tile : MonoBehaviour
    {
        public TileState State { get; private set; }
        public TileCell Cell { get; private set; }
        public bool Locked { get; set; }
    
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _text;
    
        public void SetState(TileState state)
        {
            State = state;
    
            _background.color = state.backgroundColor;
            _text.color = state.textColor;
            _text.text = state.number.ToString();
        }
    
        public void Spawn(TileCell cell)
        {
            if (Cell != null)
                Cell.Tile = null;
    
            Cell = cell;
            Cell.Tile = this;
    
            transform.position = cell.transform.position;
        }
    
        public void MoveTo(TileCell cell)
        {
            if (Cell != null) 
                Cell.Tile = null;

            Cell = cell;
            Cell.Tile = this;
    
            StartCoroutine(Animate(cell.transform.position, false));
        }
    
        public void Merge(TileCell cell, Action onMerge)
        {
            if (Cell != null) 
                Cell.Tile = null;
    
            Cell = null;
            cell.Tile.Locked = true;
    
            StartCoroutine(Animate(cell.transform.position, true, onMerge));
        }
    
        private IEnumerator Animate(Vector3 to, bool merging, Action onMerge = null)
        {
            var elapsed = 0f;
            var duration = 0.07f;
    
            var from = transform.position;
    
            while (elapsed < duration)
            {
                transform.position = Vector3.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
    
            transform.position = to;

            if (!merging) 
                yield break;
            
            onMerge?.Invoke();
            Destroy(gameObject);
        }
    
    }
}
