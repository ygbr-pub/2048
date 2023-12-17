namespace PH.Application
{
    using System;
    using UnityEngine;
    using uPalette.Generated;
    using uPalette.Runtime.Core;
    using uPalette.Runtime.Core.Model;

    public class SettingsManager : MonoBehaviour
    {
        public static Action OnThemeChanged;

        private Palette<Color> ColorPalette => PaletteStore.Instance.ColorPalette;
        private const ColorTheme DefaultTheme = ColorTheme.MintPastel;
        private ColorTheme _activeTheme;

        
        private void Start()
        {
            SetActiveTheme(DefaultTheme);
        }

        private void SetActiveTheme(ColorTheme theme)
        {
            _activeTheme = theme;
            ColorPalette.SetActiveTheme(_activeTheme.ToThemeId());
            OnThemeChanged?.Invoke();
        }
    }
}