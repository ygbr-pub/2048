namespace PH.Application
{
    using UnityEngine;

    public class ApplicationManager : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}