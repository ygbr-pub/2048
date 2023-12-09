namespace PH.Theme
{
    using System;
    using UnityEngine;
    using Game;

    [CreateAssetMenu(menuName = "Configs/ColourPaletteConfig", fileName = "NewColorPalette")]
    public class ThemeConfig : ScriptableObject
    {
        public string Name => _name;
        public Palette Palette => _palette;
        
        [SerializeField] private string _name;
        [SerializeField] private Palette _palette;

        public Color GetColorForElement(ThemeElements element)
        {
            return element switch
            {
                ThemeElements.BackgroundFill => _palette.BackgroundFill,
                ThemeElements.LabelText => _palette.LabelText,
                ThemeElements.OverlayFill => _palette.OverlayFill,
                ThemeElements.BoardFill => _palette.BoardFill,
                ThemeElements.CellFill => _palette.CellFill,
                ThemeElements.ScoreLabelText => _palette.ScoreLabelText,
                ThemeElements.ScoreValueText => _palette.ScoreValueText,
                ThemeElements.ScoreFill => _palette.ScoreFill,
                ThemeElements.ButtonText => _palette.ButtonText,
                ThemeElements.ButtonFill => _palette.ButtonFill,
                ThemeElements.None => Color.magenta,
                _ => Color.magenta
            };
        }
    }
    
    [Serializable]
    public struct Palette
    {
        [Header("General")]
        public Color BackgroundFill;
        public Color LabelText;
        public Color OverlayFill;

        [Header("Board")]
        public Color BoardFill;
        public Color CellFill;

        [Header("Score")]
        public Color ScoreLabelText;
        public Color ScoreValueText;
        public Color ScoreFill;
        
        [Header("Buttons")]
        public Color ButtonText;
        public Color ButtonFill;

        [Space]
        public TileState[] Tiles;
    }

    public enum ThemeElements
    {
        None,
        BackgroundFill,
        LabelText,
        OverlayFill,
        BoardFill,
        CellFill,
        ScoreLabelText,
        ScoreValueText,
        ScoreFill,
        ButtonText,
        ButtonFill,
    }
}