using System;
using System.Collections;
using UnityEngine;

namespace SkyMan
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 5;
        
        // Just a small number.
        [SerializeField] private float delta = 0.01f;
        private IPositionInterpreter _positionInterpreter;
        private ISpeedInterpreter _speedInterpreter;
        private Vector3 _currentTarget;
        private float _currentSpeed;

        private void Awake()
        {
            _currentSpeed = baseSpeed;
            _positionInterpreter = FindObjectOfType<Player>().PositionInterpreter;
            _speedInterpreter = FindObjectOfType<SpeedSlider>().SpeedInterpreter;
        }

        private void OnEnable()
        {
            _positionInterpreter.NewPositionAdded += OnNewPositionAdded;
            _speedInterpreter.SpeedChanged += OnSpeedChanged;
        }
        
        private void OnDisable()
        {
            _positionInterpreter.NewPositionAdded -= OnNewPositionAdded;
            _speedInterpreter.SpeedChanged -= OnSpeedChanged;
        }

        private void OnNewPositionAdded()
        {
            StartCoroutine(MovingToTarget());
        }

        private IEnumerator MovingToTarget()
        {
            _positionInterpreter.NewPositionAdded -= OnNewPositionAdded;
            
            // Skipping a frame, because we remove the point from buffer, but we need to work with it in LineDraw. 
            yield return null;
            
            while (_positionInterpreter.RemoveOldPosition(out _currentTarget))
            {
                while ((transform.position - _currentTarget).magnitude >= delta)
                {
                    var velocity = (_currentTarget - transform.position).normalized * _currentSpeed
                        * Time.deltaTime;
                    transform.Translate(velocity);
                    yield return null;
                }
            }

            _positionInterpreter.NewPositionAdded += OnNewPositionAdded;
        }
        
        
        private void OnSpeedChanged(float speedAddition)
        {
            _currentSpeed = baseSpeed + speedAddition;
            if (_currentSpeed < 0)
            {
                _currentSpeed = 0;
            }
        }
    }
}