using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Libgame
{
    public class LibTime
    {
        static float savedTimeScale;

        public static bool isPaused = false;

        public static void ChangeTimeScale(float timeScale = 1)
        {
            if (timeScale < 0)
            {
                return;
            }
            if (timeScale == 0)
            {
                isPaused = true;
            }
            else
            {
                isPaused = false;
            }
            UnityEngine.Time.timeScale = timeScale;
        }

        public static void Pause()
        {
            if (!isPaused)
            {
                savedTimeScale = UnityEngine.Time.timeScale;
                UnityEngine.Time.timeScale = 0;
                isPaused = true;
            }
        }

        public static void Resume()
        {
            if (isPaused)
            {
                UnityEngine.Time.timeScale = savedTimeScale;
                isPaused = false;
            }
        }
    }
}
