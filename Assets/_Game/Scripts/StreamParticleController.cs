using UnityEngine;

namespace Aezakmi
{
    public class StreamParticleController : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void OnEnable() => EventManager.StartListening(GameEvents.GlassFilled, delegate { _particleSystem.Clear(); });
        private void OnDisable() => EventManager.StopListening(GameEvents.GlassFilled, delegate { _particleSystem.Clear(); });
        private void Start() => _particleSystem = GetComponent<ParticleSystem>();
    }
}

