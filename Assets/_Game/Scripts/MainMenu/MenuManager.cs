using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Aezakmi.Tweens;

namespace Aezakmi.MainMenu
{
    public class MenuManager : GloballyAccessibleBase<MenuManager>
    {
        [SerializeField] private List<MenuSceneData> MenuSceneDatas;

        [Header("Bubble")]
        [SerializeField] private Scale BubbleParent;
        [SerializeField] private TextMeshPro BubbleText;

        private int _selectedMenuSceneData;

        private void Start()
        {
            _selectedMenuSceneData = PlayerPrefs.GetInt("CurrentMenuSceneData", 0) % MenuSceneDatas.Count;
            OrganizeScene();
        }

        private void OrganizeScene()
        {
            BubbleText.text = $"I need a flower for {MenuSceneDatas[_selectedMenuSceneData].FlowerNeededFor}.";
            Instantiate(MenuSceneDatas[_selectedMenuSceneData].CustomerPrefab);
        }

        public void ShowBubble() => BubbleParent.PlayTween();

        private void Update()
        {
            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && InputManager.Instance.Touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("02_GameScene");
            }
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt("CurrentMenuSceneData", ++_selectedMenuSceneData);
        }
    }
}
