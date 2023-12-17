namespace PH.Game
{
    using UnityEngine;
    
    public class SfxManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _mergeSfx;

        private bool _doMergeSfx;
        
        private void Awake()
        {
            TileBoard.OnTileMerged += OnTileMerged;
        }

        private void OnDestroy()
        {
            TileBoard.OnTileMerged -= OnTileMerged;
        }

        private void OnTileMerged() => _doMergeSfx = true;

        private void LateUpdate()
        {
            TryPerformMergeSfx();

            void TryPerformMergeSfx()
            {
                if (!_doMergeSfx) 
                    return;
                
                _doMergeSfx = false;
                
                var pitchMagnitude01 = Mathf.InverseLerp(1, 10, TileBoard.MergeStreakCounter);
                _mergeSfx.pitch = Mathf.Lerp(1f, 3f, pitchMagnitude01);
                _mergeSfx.Play();
            }
        }
    }
}
