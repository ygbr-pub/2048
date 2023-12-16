namespace PH.Game
{
    using UnityEngine;
    using uPalette.Generated;

    [CreateAssetMenu(menuName = "Tile State")]
    public class TileState : ScriptableObject
    {
        public int number;
        public ColorEntry backgroundColorId;
        public ColorEntry textColorId;
    }
}