namespace PH.Game
{
    using System;
    using System.Collections;
    using System.Globalization;
    using Application;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using uPalette.Generated;
    using uPalette.Runtime.Core;

    public class Tile : MonoBehaviour
    {
        public TileState State { get; private set; }
        public TileState PreviousState { get; private set; }
        public TileCell Cell { get; private set; }
        public bool Locked { get; set; }
    
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Shadow _shadow;

        private Action _onStateChanged;

        private void Awake()
        {
            SettingsManager.OnThemeChanged += OnThemeChanged;
            _onStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            SettingsManager.OnThemeChanged -= OnThemeChanged;
            _onStateChanged -= OnStateChanged;
        }
        
        private void OnThemeChanged() => RefreshColors();

        private void OnStateChanged() => RefreshColors();

        public void Spawn(TileCell cell)
        {
            if (Cell != null)
                Cell.Tile = null;
    
            Cell = cell;
            Cell.Tile = this;
    
            transform.position = cell.transform.position;
        }

        [SerializeField] private AnimationCurve _punchScaleCurve;
        
        public void SetState(TileState state)
        {
            PreviousState = State;
            State = state;
            _onStateChanged?.Invoke();
            
            var logValue = Mathf.Log(state.number, 2);
            var animScale01 = Mathf.Clamp01(_punchScaleCurve.Evaluate(logValue));
            var punchMagnitude = Mathf.Lerp(0.05f, 0.1f, animScale01);
            var durationMagnitude = Mathf.Lerp(0.1f, 0.2f, animScale01);
            var vibratoMagnitude = Mathf.RoundToInt(Mathf.Lerp(1, 10, animScale01));
            transform.DOPunchScale(Vector3.one * punchMagnitude, durationMagnitude, vibratoMagnitude, 1f);
        }
    
        public void MoveTo(TileCell cell)
        {
            if (Cell != null) 
                Cell.Tile = null;

            Cell = cell;
            Cell.Tile = this;

            Animate(cell.transform.position, false);
        }
    
        public void Merge(TileCell cell, Action onMerge)
        {
            if (Cell != null) 
                Cell.Tile = null;
    
            Cell = null;
            cell.Tile.Locked = true;
    
            Animate(cell.transform.position, true, onMerge);
        }
    
        private void Animate(Vector3 to, bool merging, Action onMerge = null)
        {
            const float duration = 0.07f;
            transform.DOMove(to, duration).OnComplete(OnMoveComplete);

            void OnMoveComplete()
            {
                if (!merging)
                    return;
                
                onMerge?.Invoke();
                Destroy(gameObject);                
            }
        }
    
        private void RefreshColors()
        {
            var activeThemePalette = PaletteStore.Instance.ColorPalette;
            var backgroundId = State.backgroundColorId.ToEntryId();
            var textId = State.textColorId.ToEntryId();
            var shadowId = ColorEntry.Tile_Shadow.ToEntryId();
            
            _background.color = activeThemePalette.GetActiveValue(backgroundId).Value;
            _text.color = activeThemePalette.GetActiveValue(textId).Value;
            _shadow.effectColor = activeThemePalette.GetActiveValue(shadowId).Value;

            var hasPreviousState = PreviousState != null;
            var hasMinimumValueDistance = hasPreviousState && State.number - PreviousState.number >= 4;

            if (!hasMinimumValueDistance) 
                SetValueLabelImmediate();
            else 
                SetValueLabelAnimated();

            void SetValueLabelImmediate()
            {
                _text.text = State.number.ToString();
            }

            void SetValueLabelAnimated()
            {
                var logValue = Mathf.Log(State.number, 2);
                var animScale01 = Mathf.InverseLerp(1, 20, logValue);
                var duration = Mathf.Lerp(0.2f, 0.6f, animScale01);
                var counter = (float) PreviousState.number;
                var from = PreviousState.number;
                var to = State.number;
                
                DOTween.To(UpdateAnimCounter, from, to, duration)
                    .OnUpdate(UpdateLabelText)
                    .OnComplete(FinalizeLabelText);                
                
                void UpdateAnimCounter(float x) => counter = x;
                void UpdateLabelText() => _text.text = Mathf.RoundToInt(counter).ToString(CultureInfo.InvariantCulture);
                void FinalizeLabelText() => SetValueLabelImmediate();
            }
        }
        
    }
}
