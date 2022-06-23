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
        private Vector3 _currentTarget;
        private float _currentSpeed;

        private void Awake()
        {
            _positionInterpreter = FindObjectOfType<Player>().PositionInterpreter;
            _currentSpeed = baseSpeed;
        }

        private void OnEnable()
        {
            _positionInterpreter.NewPositionAdded += OnNewPositionAdded;
        }

        private void OnDisable()
        {
            _positionInterpreter.NewPositionAdded -= OnNewPositionAdded;
        }

        private void OnNewPositionAdded()
        {
            Debug.Log("New position Added");
            StartCoroutine(MovingToTarget());
        }

        private IEnumerator MovingToTarget()
        {
            _positionInterpreter.NewPositionAdded -= OnNewPositionAdded;
            
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
    }
}