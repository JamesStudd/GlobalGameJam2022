using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayback : MonoBehaviour
{
    [SerializeField] private float _pauseTime;

    private Vector3 _startPosition;
    
    private bool _isRec = false;
    private float _tempX;
    private float _tempY;
    private float _tempZ;
    
    private readonly List<float> _nums = new List<float>();

    private PlayerInput _playerInput;

    public event Action OnReplayed;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _playerInput.OnReplay += Replay;
    }

    [ContextMenu("Record")]
    public void Record()
    {
        _startPosition = transform.position;
        _isRec = true;
    }

    [ContextMenu("StopRecording")]
    public void StopRecording()
    {
        _isRec = false;
        transform.position = _startPosition;
    }
    
    [ContextMenu("Replay")]
    public void Replay()
    {
        _isRec = false;
        StartCoroutine(Playback());
    }
    
    [ContextMenu("Reset")]
    public void Reset()
    {
        _nums.Clear();
    }
    
    private void FixedUpdate()
    {
        if (!_isRec) return;
        
        var position = transform.position;
            
        _tempX = position.x;
        _tempY = position.y;
        _tempZ = position.z;
            
        _nums.Add(_tempX);
        _nums.Add(_tempY);
        _nums.Add(_tempZ);
    }

    private IEnumerator Playback()
    {
        var initial = true;
        
        while (true)
        {
            if (initial)
            {
                for (int i = _nums.Count - 1; i >= 0; i -= 3)
                {
                    transform.position = new Vector3(_nums[i - 2], _nums[i - 1], _nums[i - 0]); 
                    yield return new WaitForFixedUpdate();
                }
                
                OnReplayed?.Invoke();
            }
            
            for (int i = 0; i < _nums.Count; i += 3)
            {
                transform.position = new Vector3(_nums[i], _nums[i + 1], _nums[i + 2]);
                yield return new WaitForFixedUpdate();
            }

            transform.position = _startPosition;

            initial = false;

            yield return null;
        }
    }
}
