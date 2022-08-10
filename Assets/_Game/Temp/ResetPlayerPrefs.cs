using UnityEngine;

namespace Aezakmi
{
    public class ResetPlayerPrefs : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetInt("TotalCash", 0);
            PlayerPrefs.SetInt("CurrentMenuSceneData", 0);
        }
    }
}
