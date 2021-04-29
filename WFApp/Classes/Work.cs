using System;
using System.Collections.Generic;
using System.Text;

namespace WFApp.Classes
{
    public class Work
    {
        // Early start - ES.
        // Late start - LS.
        // Early ending - EE.
        // Late end - LE.
        public bool IsCritical = false;
        public uint CPId = 0;
        public uint Id = 0;
        public readonly float DurationMin, DurationMax, ResourcesMin, ResourcesMax;
        public readonly string Title;
        public float Duration = 0f, Resources = 0f, ES = 0f, LS = 0f, EE = 0f, LE = 0f, FR = 0f, PR = 0f, FreeR = 0f, IR = 0f, K = 0f;
        public string FirstEventTitle, SecondEventTitle;
        public ProjectEvent FirstEvent, SecondEvent;    
        public Work(string title, float durationMax, float durationMin, 
                    float resourcesMin, float resourcesMax,
                    string startEventTitle, string secondEventTitle)
        {
            Title = title;
            FirstEventTitle = startEventTitle;
            SecondEventTitle = secondEventTitle;
            DurationMax = durationMax;
            DurationMin = durationMin;
            Duration = durationMax;
            ResourcesMax = resourcesMax;
            ResourcesMin = resourcesMin;
            Resources = resourcesMin;
        }
    }
}
