using System;
using System.Linq;

namespace QSBMODLibraryNF.Classes
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
        public float Duration {get; set;} 
        public float Resources = 0,
            ES = 0, LS = 0, EE = 0, LE = 0, FR = 0, PR = 0, FreeR = 0, IR = 0, K = 0, tgA = 0;
        public string FirstEventTitle, SecondEventTitle;
        public ProjectEvent FirstEvent, SecondEvent;    
        public Work(string title, float durationMin, float durationMax,
                    float resourcesMin, float resourcesMax,
                    string startEventTitle, string secondEventTitle)
        {
            
            if (title.Contains(';') || startEventTitle.Contains(';') || secondEventTitle.Contains(';'))
                throw new InvalidOperationException("Названия не могут содержать \";\"");
            if (durationMax < 0 || durationMin < 0 || resourcesMax < 0 || resourcesMin < 0)
                throw new InvalidOperationException("Значения должны быть положительными");
            if (durationMax < durationMin || resourcesMax < resourcesMin)
                throw new InvalidOperationException("Tmax должно быть больше чем Tmin или Cmax чем Cmin");
            if (startEventTitle == secondEventTitle)
                throw new InvalidOperationException("Начальное событие и конечное, должны быть разными");

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
        }
        public Work(string title, float duration, float durationMin, float durationMax,
            float resources, float resourcesMin, float resourcesMax,
            string startEventTitle, string secondEventTitle)
        {
            if (title.Contains(';') || startEventTitle.Contains(';') || secondEventTitle.Contains(';'))
                throw new InvalidOperationException("Названия не могут содержать \";\"");
            if (durationMax < 0 || durationMin < 0 || duration < 0 || resourcesMax < 0 || resourcesMin < 0 || resources < 0)
                throw new InvalidOperationException("Значения должны быть положительными");
            if (durationMax < durationMin || resourcesMax < resourcesMin)
                throw new InvalidOperationException("Tmax должно быть больше чем Tmin или Cmax чем Cmin");
            if (startEventTitle == secondEventTitle)
                throw new InvalidOperationException("Начальное событие и конечное, должны быть разными");
            if (duration > durationMax || duration < durationMin)
                throw new InvalidOperationException("t должно быть между Tmin и Tmax");
            if (resources > resourcesMax || resources < resourcesMin)
                throw new InvalidOperationException("c должно быть между Cmin и Cmax");
            
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
        }
        public Work()
        {
            tgA = 100;
        }
    }
}
