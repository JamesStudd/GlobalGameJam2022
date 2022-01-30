using UnityEngine;

namespace _Scripts
{
    public class KillBlock : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void Collide(GameObject go)
        {
            Destroy(go);
            _particleSystem.Play();

            Invoke(nameof(GameEnd), 2f);
        }

        private void GameEnd()
        {
            GameEvents.GameEnd(false);
        }
    }
}