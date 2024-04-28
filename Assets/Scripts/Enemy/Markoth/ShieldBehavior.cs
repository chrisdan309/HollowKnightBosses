using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float scaleDistance = 1.5f;  // Máxima distancia a la que el escudo puede alejarse
    public float idleDuration = 5f; 
    private float _timer;
    public bool _increasing = true;
    private float _originalRotationSpeed;
    private Vector3 _originalPosition;
    
    private EnemyState _currenteState;
    
    
    public void SetState(EnemyState state)
    {
        _timer = 0f;
        _currenteState = state;
    }
    
    private void Start()
    {
        _originalPosition = transform.localPosition;
        _originalRotationSpeed = rotationSpeed;
        _currenteState = transform.parent.GetComponent<Enemy>().currentState;
    }

    void Update()
    {
        _currenteState = transform.parent.GetComponent<Enemy>().currentState;
        HandleState(_currenteState);
        
    }
    
    void HandleState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                
                HandleIdleState();
                break;
            case EnemyState.Attacking:
                transform.RotateAround(transform.parent.position, Vector3.forward, _originalRotationSpeed * Time.deltaTime);
                _timer = 0f;
                break;
            case EnemyState.Dead:
                Destroy(gameObject);
                break;
            
        }
    }
    
    private void HandleIdleState()
    {
        _timer += Time.deltaTime;
        float phaseDuration = idleDuration / 2;
        float normalizedTime = (_timer % phaseDuration) / phaseDuration;
        Vector3 direction = (transform.position - transform.parent.position).normalized;
        if (_timer >= idleDuration/2)
        {
            _timer = 0;
            _increasing = !_increasing; 
        }

        if (_increasing)
        {
            transform.localPosition += direction * (scaleDistance * Time.deltaTime); 
            rotationSpeed = Mathf.Lerp(_originalRotationSpeed, _originalRotationSpeed * 5, normalizedTime);
        }
        else
        {
            transform.localPosition -= direction * (scaleDistance * Time.deltaTime);
            rotationSpeed = Mathf.Lerp(_originalRotationSpeed * 5, _originalRotationSpeed, normalizedTime);
        }
        
        transform.RotateAround(transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);

    }
}
