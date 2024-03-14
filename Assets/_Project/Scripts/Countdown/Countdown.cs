namespace CountdownTimer
{
    public class Countdown : ICountdown
    {
        private float _elapsedTime = 0;
        public bool Check(float dt, float interval)
        {
            _elapsedTime += dt;
            if (_elapsedTime >= interval)
            {
                Reset();
                return true;
            }
            return false;
        }

        private void Reset()
        {
            _elapsedTime = 0;
        }
    }
}