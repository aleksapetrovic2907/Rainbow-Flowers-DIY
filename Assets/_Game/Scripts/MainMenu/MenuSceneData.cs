using UnityEngine;

namespace Aezakmi.MainMenu
{
    [CreateAssetMenu(fileName = "MenuSceneData", menuName = "RainbowColors/MenuSceneData", order = 1)]
    public class MenuSceneData : ScriptableObject
    {
        public string FlowerNeededFor;
        public GameObject CustomerPrefab;
    }
}
