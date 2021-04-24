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
        public string Title, PreviousEventId, FollowingEventId;
        public Work(string title, float duration, float durationMin, float durationMax, 
                    float resourcesMin, float resourcesMax,
                    string prevEventId, string followEventId)
        {
            Title = title;
            PreviousEventId = prevEventId;
            FollowingEventId = followEventId;
            DurationMax = durationMax;
            Duration = duration;
            DurationMin = durationMin;
            ResourcesMax = resourcesMax;
            ResourcesMin = resourcesMin;
        }
    }
}
