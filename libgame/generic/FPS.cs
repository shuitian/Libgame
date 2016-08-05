using UnityEngine;

namespace Libgame
{
    public class FPS
    {
        static float timeleft = 0;
        static int frames = 0;
        static float accum = 0;
        static string strFPS = null;
        public static string GetFPS(float updateInterval = 1F)
        {
            timeleft -= UnityEngine.Time.deltaTime;
            accum += UnityEngine.Time.timeScale / UnityEngine.Time.deltaTime;
            ++frames;
            if (timeleft <= 0.0)
            {
                strFPS = (accum / frames).ToString("f2");
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
                
            }
            return strFPS;
        }
    }
}

namespace Libgame.Runtime
{
    public class ShowFPS : MonoBehaviour
    {

        public string FPS;
        public float updateInterval = 0.5F;

        void Update()
        {
            FPS = Libgame.FPS.GetFPS(updateInterval);
        }
    }

    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
