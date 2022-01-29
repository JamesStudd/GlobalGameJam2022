using UnityEngine;

namespace _Scripts
{
    public class PlayerLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private float _recordInterval;

        private int _index = 0;
        private float _nextRecord;
        private MovementPlayback _movementPlayback;

        private bool _canRecord = false;
        
        private void Awake()
        {
            _movementPlayback = GetComponent<MovementPlayback>();
            
            _movementPlayback.OnStartRecording +=  () => _canRecord = true;
            _movementPlayback.OnEndRecording +=  () => _canRecord = false;
            _movementPlayback.OnReplayed += () => _lineRenderer.enabled = true;

            _lineRenderer.enabled = false;
            _lineRenderer.SetPositions(new Vector3[0]);
        }

        private void Update()
        {
            if (_canRecord && Time.realtimeSinceStartup > _nextRecord)
            {
                _nextRecord = Time.realtimeSinceStartup + _recordInterval;
                RecordPoint();
            }
        }

        private void RecordPoint()
        {
            _lineRenderer.positionCount = _index + 1;
            _lineRenderer.SetPosition(_index, transform.position);
            _index++;
        }
    }
}