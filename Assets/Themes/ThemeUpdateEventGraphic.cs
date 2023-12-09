namespace PH.Theme
{
    using System;
    using Application;
    using UnityEngine;
    using UnityEngine.UI;

    // [RequireComponent(typeof(Graphic))]
    public class ThemeUpdateEventGraphic : MonoBehaviour
    {
        [SerializeField] private Graphic _graphic;
        [SerializeField] private ThemeElements _element = ThemeElements.None;
            
        private void Awake()
        {
            SettingsManager.OnThemeChanged += OnThemeChanged;
        }

        private void OnEnable()
        {
            
        }

        private void OnDestroy()
        {
            SettingsManager.OnThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(ThemeConfig theme)
        {
            if (_graphic == null)
                return;
            if (_element == ThemeElements.None)
                return;

            _graphic.color = theme.GetColorForElement(_element);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _graphic = GetComponent<Graphic>();
        }
#endif
    }
}
