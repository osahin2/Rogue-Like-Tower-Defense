namespace CountdownTimer
{
    public interface ICountdown
    {
        bool Check(float dt, float interval);
    }
}