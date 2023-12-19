namespace PH.Application
{
    using UnityEngine;

    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 120;

        private void Awake()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}