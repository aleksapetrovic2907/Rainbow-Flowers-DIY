using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class CoinCalculator : GloballyAccessibleBase<CoinCalculator>
    {
        [SerializeField] private int CashIncrement;

        public int CashEarned { get; private set; }

        private void OnEnable() => EventManager.StartListening(GameEvents.FinishedStemCutting, AddCash);
        private void OnDisable() => EventManager.StopListening(GameEvents.FinishedStemCutting, AddCash);
        
        private void AddCash(Dictionary<string, object> message)
        {
            CashEarned = Random.Range(1, 4) * CashIncrement;

            var currentCash = PlayerPrefs.GetInt("TotalCash");
            PlayerPrefs.SetInt("TotalCash", currentCash + CashEarned);
        }
    }
}
