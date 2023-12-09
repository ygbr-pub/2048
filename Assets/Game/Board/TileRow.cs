namespace PH.Game
{
    using UnityEngine;

    public class TileRow : MonoBehaviour
    {
        public TileCell[] Cells { get; private set; }

        private void Awake()
        {
            Cells = GetComponentsInChildren<TileCell>();
        }
    }
}