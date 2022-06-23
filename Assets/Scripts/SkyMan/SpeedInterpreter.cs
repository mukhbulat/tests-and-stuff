using System;

namespace SkyMan
{
    public class SpeedInterpreter : ISpeedInterpreter
    {
        public event Action<float> SpeedChanged;
        
        public void ChangeSpeed(float speedAddition)
        {
            SpeedChanged?.Invoke(speedAddition);
        }
    }
}