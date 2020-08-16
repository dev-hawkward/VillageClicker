using TMPro;
using UnityEngine;

namespace HW
{
    public class TimerText : HWBehaviour
    {
        [SerializeField] TextMeshProUGUI timerText;
        private int currTime;
        private int prevTime;
        private int hour;
        private int min;
        private int sec;
        private const int hourSec = 3600;
        private const int minSec = 60;
        private void Start()
        {
            currTime = 0;
            prevTime = 0;
            hour = 0;
            min = 0;
            sec = 0;
            timerText.text = $"{hour:00}:{min:00}:{sec:00}";
        }
        private void Update()
        {
            //Calculate the current time
            currTime = Mathf.FloorToInt(GameManager.Inst.currTime);

            if (currTime != prevTime)
            {
                hour = currTime / hourSec;
                min = currTime % hourSec / minSec;
                sec = currTime % hourSec % minSec;

                timerText.text = $"{hour:00}:{min:00}:{sec:00}";

                prevTime = currTime;
            }
        }
    }
}
