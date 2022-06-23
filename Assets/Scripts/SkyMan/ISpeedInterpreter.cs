using System;

namespace SkyMan
{
    public interface ISpeedInterpreter
    {
        public event Action<float> SpeedChanged;

        public void ChangeSpeed(float speedAddition);
    }
}