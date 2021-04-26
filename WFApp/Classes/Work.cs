using System;
using System.Collections.Generic;
using System.Text;

namespace WFApp.Classes
{
    public class Work
    {
        public uint Id = 0;
        public ProjectEvent PreviousEvent, FollowingEvent;
        public float Duration, DurationMin, DurationMax, ResourcesMin, ResourcesMax;
        public string Title, PreviousEventTitle, FollowingEventTitle;
        public Work(string title, float duration, float durationMin, float durationMax, 
                    float resourcesMin, float resourcesMax,
                    string prevEventTitle, string followEventTitle)
        {
            Title = title;
            PreviousEventTitle = prevEventTitle;
            FollowingEventTitle = followEventTitle;
            DurationMax = durationMax;
            Duration = duration;
            DurationMin = durationMin;
            ResourcesMax = resourcesMax;
            ResourcesMin = resourcesMin;
        }
    }
}
