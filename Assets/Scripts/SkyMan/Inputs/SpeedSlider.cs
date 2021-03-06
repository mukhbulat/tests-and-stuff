using SkyMan.Interpreters;
using UnityEngine;

namespace SkyMan.Inputs
{
    public class SpeedSlider : MonoBehaviour
    {
        [SerializeField] private float speedMultiplier;
        
        public ISpeedInterpreter SpeedInterpreter { get; } = new SpeedInterpreter();

        public void SpeedChange(float sliderValue)
        {
            sliderValue -= 0.5f;
            SpeedInterpreter.ChangeSpeed(sliderValue * speedMultiplier);
        }
    }
}