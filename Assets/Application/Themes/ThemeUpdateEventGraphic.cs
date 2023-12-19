namespace PH.Theme
{
    using Application;
    using UnityEngine;
    using UnityEngine.UI;
    using uPalette.Generated;
    using uPalette.Runtime.Core;

    [RequireComponent(typeof(Graphic))]
    public class ThemeUpdateEventGraphic : MonoBehaviour
    {
        [SerializeField] private Graphic _graphic;
        [SerializeField] private ColorEntry _paletteElement;
            
        private void Awake()
        {
            SettingsManager.OnThemeChanged += OnThemeChanged;
        }

        private void OnDestroy()
        {
            SettingsManager.OnThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged()
        {
            if (_graphic == null)
                return;
            
            var activeThemePalette = PaletteStore.Instance.ColorPalette;
            var elementId = _paletteElement.ToEntryId();
            _graphic.color = activeThemePalette.GetActiveValue(elementId).Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _graphic = GetComponent<Graphic>();
        }
#endif
    }
}
