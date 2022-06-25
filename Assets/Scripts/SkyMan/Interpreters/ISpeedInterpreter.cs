using System;

namespace SkyMan.Interpreters
{
    public interface ISpeedInterpreter
    {
        public event Action<float> SpeedChanged;

        public void ChangeSpeed(float speedAddition);
    }
}