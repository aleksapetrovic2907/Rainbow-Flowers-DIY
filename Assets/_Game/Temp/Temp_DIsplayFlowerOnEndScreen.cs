using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Aezakmi
{
    public class Temp_DIsplayFlowerOnEndScreen : MonoBehaviour
    {
        [SerializeField] private GameObject Flower;
        [SerializeField] private Vector3 FlowerPos;
        [SerializeField] private Vector3 FlowerRot;
        [SerializeField] private Vector3 FlowerScale;

        [SerializeField] private GameObject RTCamera;

        [SerializeField] private TextMeshProUGUI CashText;
        [SerializeField] private TextMeshProUGUI LikesText;

        private void Awake()
        {
            var go = Instantiate(Flower, FlowerPos, Quaternion.Euler(FlowerRot));
            RTCamera.SetActive(true);

            CashText.text = CoinCalculator.Instance.CashEarned.ToString();
            LikesText.text = Random.Range(1793, 8978).ToString();
        }

        public void GoBackToMainMenu()
        {
            SceneManager.LoadScene("01_MainMenu");
        }
    }
}
