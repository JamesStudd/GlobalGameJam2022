using Sirenix.Utilities;
using UnityEngine;

namespace _Scripts
{
    public class PlayerParticleEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particleSystems;

        private MovementPlayback _movementPlayback;

        private void Awake()
        {
            _movementPlayback = GetComponent<MovementPlayback>();

            _movementPlayback.OnEndRecording += () => _particleSystems.ForEach(e => e.Play());
            _movementPlayback.OnReplayed += () => _particleSystems.ForEach(e => e.Stop());
        }
    }
}