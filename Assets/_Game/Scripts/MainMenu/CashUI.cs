using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Aezakmi.MainMenu
{
    public class CashUI : MonoBehaviour
    {
        private TextMeshProUGUI _tmPro;

        private void Start()
        {
            _tmPro = GetComponent<TextMeshProUGUI>();
            _tmPro.text = PlayerPrefs.GetInt("TotalCash").ToString();
        }
    }
}
