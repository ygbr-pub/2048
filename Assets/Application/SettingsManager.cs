namespace PH.Application
{
    using System;
    using Theme;
    using UnityEngine;

    public class SettingsManager : MonoBehaviour
    {
        public static Action<ThemeConfig> OnThemeChanged;
        
        [SerializeField] private ThemeConfig _defaultConfig;
        private ThemeConfig _activeTheme;
        
        private void Start()
        {
            SetActiveTheme(_defaultConfig);
        }

        private void SetActiveTheme(ThemeConfig theme)
        {
            if (theme == null)
                return;
            _activeTheme = theme;
            OnThemeChanged?.Invoke(_activeTheme);
        }
    }
}