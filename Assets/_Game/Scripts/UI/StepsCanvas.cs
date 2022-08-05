using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi.UI
{
    public class StepsCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _stepsParent;
        [SerializeField] private List<StepUI> _stepsUI;

        private int _currentStep = 0;
        private bool _passedFirstStep;

        private void OnEnable()
        {
            EventManager.StartListening(GameEvents.StepFinished, FinishStep);
        }
        private void OnDisable() => EventManager.StartListening(GameEvents.StepFinished, FinishStep);

        private void FinishStep(Dictionary<string, object> message)
        {
            _stepsParent.SetActive(true);

            if (!_passedFirstStep)
            {
                _passedFirstStep = true;
                return;
            }
            if (_currentStep == _stepsUI.Count - 1)
            {
                _stepsUI[_currentStep].CompleteStep();
                gameObject.SetActive(false);
                return;
            }

            _stepsUI[_currentStep++].CompleteStep();
            _stepsUI[_currentStep].CurrentStep();
        }
    }
}
