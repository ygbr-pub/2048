namespace PH.Game
{
    using System;
    using System.Globalization;
    using Application;
    using DG.Tweening;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using uPalette.Generated;
    using uPalette.Runtime.Core;
    using Random = UnityEngine.Random;

    public class Tile : MonoBehaviour
    {
        public TileCell Cell { get; private set; }
        public TileState State { get; private set; }
        private TileState PreviousState { get; set; }
        public bool Locked { get; set; }

        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Shadow _shadow;
        [SerializeField] private AnimationCurve _punchScaleCurve;
        
        private Action _onStateChanged;
        private bool _isAnimatingTap;
        private int _instanceId;

        private void Awake()
        {
            _instanceId = GetInstanceID();
            SettingsManager.OnThemeChanged += OnThemeChanged;
            _onStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            SettingsManager.OnThemeChanged -= OnThemeChanged;
            _onStateChanged -= OnStateChanged;
        }

        
        public void Event_OnButtonClicked()
        {
            if (_isAnimatingTap)
                return;
            
            TileTapAnimation();

            void TileTapAnimation()
            {
                var punchMagnitude = Random.Range(-0.1f, -0.2f);
                var durationMagnitude = Random.Range(0.05f, 0.15f);
                var vibratoMagnitude = Mathf.RoundToInt(Random.Range(1, 10));
                var punchPositionDir = new Vector3(0, -1, 0);
                var punchPositionMagnitude = Random.Range(5f, 10f);
                const Ease ease = Ease.InOutExpo;
                transform.DOPunchScale(Vector3.one * punchMagnitude, durationMagnitude, vibratoMagnitude)
                    .SetEase(ease)
                    .OnStart(OnTapAnimStarted)
                    .OnComplete(OnTapAnimCompleted)
                    .SetId(_instanceId);
                
                transform.DOPunchPosition(punchPositionDir * punchPositionMagnitude, durationMagnitude, vibratoMagnitude)
                    .SetEase(ease)
                    .SetId(_instanceId);

                _text.transform.DOPunchScale(Vector3.one * punchMagnitude, durationMagnitude, vibratoMagnitude)
                    .SetEase(ease)
                    .SetDelay(0.1f)
                    .SetId(_instanceId);
            }

            void OnTapAnimStarted() => _isAnimatingTap = true;
            void OnTapAnimCompleted() => _isAnimatingTap = false;
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

        
        public void SetState(TileState state)
        {
            PreviousState = State;
            State = state;
            _onStateChanged?.Invoke();
            TilePunchAnimation();

            void TilePunchAnimation()
            {
                var logValue = Mathf.Log(state.number, 2);
                var animScale01 = Mathf.Clamp01(_punchScaleCurve.Evaluate(logValue));
                var punchMagnitude = Mathf.Lerp(0.1f, 0.2f, animScale01);
                var durationMagnitude = Mathf.Lerp(0.2f, 0.4f, animScale01);
                var vibratoMagnitude = Mathf.RoundToInt(Mathf.Lerp(1, 10, animScale01));
                const Ease ease = Ease.InOutExpo;
                var punchV3 = Random.value >= 0.5 ? Vector3.up : Vector3.right;

                // Do a uniform punch is spawning rather than a merged state change.
                if (PreviousState == null)
                {
                    punchV3 = Vector3.one;
                }
                
                transform.DOPunchScale(punchV3 * punchMagnitude, durationMagnitude, vibratoMagnitude, 1f)
                    .SetEase(ease)
                    .SetId(_instanceId);
                _text.transform.DOPunchScale(Vector3.one * punchMagnitude, durationMagnitude, vibratoMagnitude, 1f)
                    .SetEase(ease)
                    .SetDelay(0.2f)
                    .SetId(_instanceId);
            }
        }
    
        public void MoveTo(TileCell cell)
        {
            if (Cell != null) 
                Cell.Tile = null;

            Cell = cell;
            Cell.Tile = this;

            AnimateMoveTo(cell.transform.position, false);
        }
    
        public void Merge(TileCell cell, Action onMerge)
        {
            if (Cell != null) 
                Cell.Tile = null;
    
            Cell = null;
            cell.Tile.Locked = true;
    
            AnimateMoveTo(cell.transform.position, true, onMerge);
        }
    
        private void AnimateMoveTo(Vector3 to, bool merging, Action onMerge = null)
        {
            var duration = merging ? 0.12f : 0.15f;
            var ease = merging ? Ease.OutExpo : Ease.OutBack;
            transform.DOMove(to, duration)
                .SetEase(ease)
                .OnComplete(OnMoveComplete)
                .SetId(_instanceId);

            void OnMoveComplete()
            {
                if (!merging)
                    return;
                
                onMerge?.Invoke();
                // TODO: Stand-in for object pooling
                var discardedTile = gameObject;
                discardedTile.SetActive(false);
                DOTween.Kill(discardedTile);
                Destroy(discardedTile, 1f);            
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
                    .OnComplete(FinalizeLabelText)
                    .SetId(_instanceId);            
                
                void UpdateAnimCounter(float x) => counter = x;
                void UpdateLabelText() => _text.text = Mathf.RoundToInt(counter).ToString(CultureInfo.InvariantCulture);
                void FinalizeLabelText() => SetValueLabelImmediate();
            }
        }
        
    }
}
