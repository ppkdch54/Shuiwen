using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;

namespace Shuiwen
{
    class SoundAlarm
    {
        public static void Alarm()
        {
            if (playProc == null)
            {
                playProc = new Thread(PlayProc);
                playProc.Start();
            }
            isPlaySound = true;
        }
        private static void PlayProc()
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = "alarm.wav";
            while(isWork)
            {
                if (isPlaySound)
                {
                    isPlaySound = false;
                    sp.PlaySync();
                }
            }
        }
        private static Thread playProc;
        private static bool isPlaySound = false;
        private static bool isWork = true;
    }
}
