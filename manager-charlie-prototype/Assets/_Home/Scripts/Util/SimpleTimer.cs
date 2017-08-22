
namespace Util
{

    public class SimpleTimer
    {
        public static SimpleTimer Create()
        {
            return new SimpleTimer();
        }

        private float LatestTime = 0.0f;
        private bool IsPlaying = false;

        private SimpleTimer()
        {

        }
        public void Start()
        {
            IsPlaying = true;
        }
        public void Update()
        {
            if (IsPlaying)
            {
                LatestTime += UnityEngine.Time.deltaTime;
            }
        }
        public bool Check(float time)
        {
            bool isComplete = LatestTime >= time;
            if (isComplete)
            {
                this.Stop();
            }
            return isComplete;
        }
        public void Stop()
        {
            IsPlaying = false;
            LatestTime = 0.0f;
        }
    }
}
