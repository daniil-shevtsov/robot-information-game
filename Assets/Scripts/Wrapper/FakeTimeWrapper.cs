public class FakeTimeWrapper : TimeWrapper
{
    public float time = 1.0f;

    public float deltaTime()
    {
        return time;
    }
}
