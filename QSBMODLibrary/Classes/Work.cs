using System;
using System.Collections.Generic;
using System.Text;

namespace QSBMODLibrary.Classes
{
    public class Work
    {
        // Early start - ES.
        // Late start - LS.
        // Early ending - EE.
        // Late end - LE.
        // Exp - Expectation - мат ожидание.
        // Dis - dispersion - дисперсия.
        public bool IsCritical = false;
        public uint CPId = 0;
        public uint Id = 0;
        public readonly float DurationMin, DurationMax, ResourcesMin, ResourcesMax;
        public readonly string Title;
        public float Duration = 0, Resources = 0,
            ES = 0, LS = 0, EE = 0, LE = 0, FR = 0, PR = 0, FreeR = 0, IR = 0, K = 0, tgA = 0, tgB = 0,
            Exp = 0, Dis = 0;
        public string FirstEventTitle, SecondEventTitle;
        public ProjectEvent FirstEvent, SecondEvent;    
        public Work(string title, float durationMin, float durationMax,
                    float resourcesMin, float resourcesMax,
                    string startEventTitle, string secondEventTitle)
        {
            if (title.Contains(';') || startEventTitle.Contains(';') || secondEventTitle.Contains(';'))
            {
                Loger.Msg("InvalidOperationException(\"Title can't contain \";\"\")");
                throw new InvalidOperationException("Title can't contain \";\"");
            }
                
            Title = title;
            FirstEventTitle = startEventTitle;
            SecondEventTitle = secondEventTitle;
            DurationMax = durationMax;
            DurationMin = durationMin;
            Duration = durationMax;
            ResourcesMax = resourcesMax;
            ResourcesMin = resourcesMin;
            Resources = resourcesMin;
            tgA = (ResourcesMax - ResourcesMin) / (DurationMax - DurationMin);
            tgB = (DurationMax - DurationMin) / (ResourcesMax - ResourcesMin);
            Exp = (2 * durationMin + 3 * durationMax)/ 5;
        }
        public Work(string title, float duration, float durationMin, float durationMax,
            float resources, float resourcesMin, float resourcesMax,
            string startEventTitle, string secondEventTitle)
        {
            Title = title;
            FirstEventTitle = startEventTitle;
            SecondEventTitle = secondEventTitle;
            DurationMax = durationMax;
            DurationMin = durationMin;
            Duration = duration;
            ResourcesMax = resourcesMax;
            ResourcesMin = resourcesMin;
            Resources = resources;
            tgA = (ResourcesMax - ResourcesMin) / (DurationMax - DurationMin);
            tgB = (DurationMax - DurationMin) / (ResourcesMax - ResourcesMin);
            Exp = (2 * durationMin + 3 * durationMax) / 5;
        }
    }
}
