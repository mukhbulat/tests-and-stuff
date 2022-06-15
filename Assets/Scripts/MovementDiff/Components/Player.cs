using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementDiff.Components
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }
    }
}