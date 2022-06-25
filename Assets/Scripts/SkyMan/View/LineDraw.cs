using SkyMan.Inputs;
using SkyMan.Interpreters;
using UnityEngine;

namespace SkyMan.View
{
    public class LineDraw : MonoBehaviour
    {
        [SerializeField] private Material material;
        [SerializeField] private float lineWidth = 0.2f;

        private IPositionInterpreter _positionInterpreter;
        private Vector3 _lastPoint;

        private void Awake()
        {
            _positionInterpreter = FindObjectOfType<Player>().PositionInterpreter;
            _lastPoint = Vector3.zero;
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
            // On rapid clicks it bugs and lines come out with the same position of starting and ending point.
            CreateLine(_lastPoint, _positionInterpreter.CurrentTargetPosition);
            _lastPoint = _positionInterpreter.CurrentTargetPosition;
        }

        private void CreateLine(Vector3 startingPoint, Vector3 endingPoint)
        {
            var line = new GameObject("Line").AddComponent<LineRenderer>();
            line.material = material;
            line.positionCount = 2;
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.useWorldSpace = true;
            line.SetPosition(0, startingPoint);
            line.SetPosition(1, endingPoint);
        }
    }
}