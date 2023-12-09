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
}