using System.Collections.Generic;
using UnityEngine;
using Aezakmi.Tweens;

namespace Aezakmi
{
    public class GlassesSetupManager : MonoBehaviour
    {
        [SerializeField] private LayerMask LayerMask;
        [SerializeField] private List<GameObject> Glasses;
        [SerializeField] private List<GameObject> Coasters;

        private int _totalGlassesSetup = 0;

        private const float GLASS_OFFSET_Y = 1.959f;

        private void Start()
        {
            Coasters[0].GetComponent<CoasterController>().SetToActive();
        }

        public void GlassPutOnCoaster()
        {
            Coasters[_totalGlassesSetup].GetComponent<CoasterController>().SetToInactive();

            if (++_totalGlassesSetup == Glasses.Count)
            {
                // finished
                return;
            }

            Coasters[_totalGlassesSetup].GetComponent<CoasterController>().SetToActive();
        }

        private void Update()
        {
            if (InputManager.Instance.IsTouching && !InputManager.Instance.IsClickingUI && InputManager.Instance.Touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.Touch.position);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
                {
                    hit.transform.gameObject.GetComponent<MoveGlassToCoaster>().MoveGlass(Coasters[_totalGlassesSetup].transform.position + Vector3.up * GLASS_OFFSET_Y);
                    GlassPutOnCoaster();
                }
            }
        }
    }
}
