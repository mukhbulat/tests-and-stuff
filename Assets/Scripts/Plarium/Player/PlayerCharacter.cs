using System;
using System.Collections.Generic;
using Plarium.SelectionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Plarium.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCharacter : MonoBehaviour
    {
        public Team Affinity => affinity;
        public SelectionController SelectionController { get; private set; }

        [SerializeField] private Team affinity;
        private Camera _mainCamera;
        private int _selectablesAndTerrainLayerMask = (1 << 7) + (1 << 8);
        private int _selectablesLayerMask = 1 << 7;
        private bool _isMultipleSelectionActive;

        private PlayerInput _playerInput;
        private InputAction _selection;
        private InputAction _multipleSelection;

        private Ray _startingRay;
        private Ray _endingRay;

        private void Awake()
        {
            _mainCamera = Camera.main;

            _playerInput = GetComponent<PlayerInput>();
            _selection = _playerInput.actions["Selection"];
            _multipleSelection = _playerInput.actions["MultipleSelection"];
        }

        private void OnEnable()
        {
            SelectionController = new SelectionController(affinity);
            
            _selection.started += SelectionOnStarted;
            _selection.canceled += SelectionOnCanceled;
            _multipleSelection.started += MultipleSelectionOnStarted;
            _multipleSelection.canceled += MultipleSelectionOnCanceled;
        }

        private void OnDisable()
        {
            _selection.started -= SelectionOnStarted;
            _selection.canceled -= SelectionOnCanceled;
            _multipleSelection.started -= MultipleSelectionOnStarted;
            _multipleSelection.canceled -= MultipleSelectionOnCanceled;
        }

        private void MultipleSelectionOnCanceled(InputAction.CallbackContext obj)
        {
            _isMultipleSelectionActive = false;
        }

        private void MultipleSelectionOnStarted(InputAction.CallbackContext obj)
        {
            _isMultipleSelectionActive = true;
        }

        private void SelectionOnCanceled(InputAction.CallbackContext obj)
        {
            _endingRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastToSelect();
        }

        private void SelectionOnStarted(InputAction.CallbackContext obj)
        {
            _startingRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        }

        private void RaycastToSelect()
        {
            if (Physics.Raycast(_startingRay, out var startHit, _selectablesAndTerrainLayerMask) &&
                Physics.Raycast(_endingRay, out var endHit, _selectablesAndTerrainLayerMask))
            {
                var hitBoxStartCorner = startHit.point;
                var hitBoxEndCorner = endHit.point;
                hitBoxStartCorner.y = hitBoxStartCorner.y > hitBoxEndCorner.y
                    ? hitBoxEndCorner.y - 1
                    : hitBoxStartCorner.y - 1;
                CornersSwap(ref hitBoxStartCorner,ref hitBoxEndCorner);
                hitBoxEndCorner.y = hitBoxStartCorner.y;
                var hitBoxCenter = (hitBoxEndCorner + hitBoxStartCorner) / 2;

                var selectables = Physics.BoxCastAll(hitBoxCenter, hitBoxEndCorner, Vector3.up, Quaternion.identity,
                    Mathf.Infinity, _selectablesLayerMask);

                if (selectables.Length == 0)
                {
                    Debug.Log("No selectables found.");
                    SelectionController.AddCharactersToSelected(new List<ISelectable>(0), _isMultipleSelectionActive);
                    return;
                }

                var listForSelector = new List<ISelectable>();
                foreach (var hit in selectables)
                {
                    var selectable = hit.collider.gameObject.GetComponent<ISelectable>();
                    if (selectable != null)
                    {
                        listForSelector.Add(selectable);
                    }
                }
                SelectionController.AddCharactersToSelected(listForSelector, _isMultipleSelectionActive);
            }
        }
        
        private void CornersSwap(ref Vector3 firstCorner,ref Vector3 secondCorner)
        {
            if (firstCorner.x > secondCorner.x)
            {
                (secondCorner.x, firstCorner.x) = (firstCorner.x, secondCorner.x);
            }

            if (firstCorner.z > secondCorner.z)
            {
                (secondCorner.z, firstCorner.z) = (firstCorner.z, secondCorner.z);
            }
        }
    }
}