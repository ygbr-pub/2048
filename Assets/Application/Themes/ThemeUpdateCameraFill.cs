using UnityEngine;

namespace PH
{
    using Application;
    using uPalette.Generated;
    using uPalette.Runtime.Core;

    // TODO: ThemUpdaters should have a BaseClass<T> to cut back on duplication of code.
    public class ThemeUpdateCameraFill : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
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
            if (_camera == null)
                return;
            
            var activeThemePalette = PaletteStore.Instance.ColorPalette;
            var elementId = _paletteElement.ToEntryId();
            _camera.backgroundColor = activeThemePalette.GetActiveValue(elementId).Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _camera = GetComponent<Camera>();
        }
#endif
    }
}
