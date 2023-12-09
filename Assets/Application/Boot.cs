namespace PH.Application
{
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    
    public class Boot : MonoBehaviour
    {
        
        [SerializeField] private AssetReference _targetScene;
        
        private void Awake() => _targetScene?.LoadSceneAsync();
    }
}
