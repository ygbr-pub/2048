namespace PH.Application
{
    using System;
    using UnityEngine;
    using uPalette.Generated;
    using uPalette.Runtime.Core;

    public class SettingsManager : MonoBehaviour
    {
        private const ColorTheme DefaultTheme = ColorTheme.MintPastel;
        private ColorTheme _activeTheme;

        public static Action OnThemeChanged;
        
        private void Start()
        {
            SetActiveTheme(DefaultTheme);
        }

        private void SetActiveTheme(ColorTheme theme)
        {
            _activeTheme = theme;
            PaletteStore.Instance.ColorPalette.SetActiveTheme(_activeTheme.ToThemeId());
            OnThemeChanged?.Invoke();
        }
    }
}