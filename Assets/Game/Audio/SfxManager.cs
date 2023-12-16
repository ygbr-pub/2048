namespace PH.Game
{
    using System;
    using UnityEngine;
    
    public class SfxManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _mergeSfx;

        private bool _doMergeSfx;
        
        private void Awake()
        {
            TileBoard.OnTileMerged += OnTileMerged;
        }

        private void OnTileMerged()
        {
            _doMergeSfx = true;
        }

        private void LateUpdate()
        {
            if (_doMergeSfx)
            {
                _doMergeSfx = false;
                var pitchMagnitude01 = Mathf.InverseLerp(1, 5, TileBoard.MergeStreakCounter);
                _mergeSfx.pitch = Mathf.Lerp(1f, 2f, pitchMagnitude01);
                _mergeSfx.Play();
            }
        }
    }
}
