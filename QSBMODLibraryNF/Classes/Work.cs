using System;
using System.Linq;

namespace QSBMODLibraryNF.Classes
{
    public class Work
    {
        // ES - Early start - Раннее начало.
        // LS - Late start - Позднее начало.
        // EE - Early ending - Раннее окончание.
        // LE - Late end - Позднее окончание.
        // FR - Full reserve - Полный резерв.
        // PR - Privat reserve - Частный резерв первого вида.
        // FreeR - Free reserve - Свободный резерв.
        // IR - Independent reserve - Независимый резерв.
        // K - Коэффициент напряженности.
        // Exp - Expectation - Мат. ожидание.
        // Dis - Dispersion - Дисперсия.
        public bool IsCritical = false;
        public uint CPId = 0;
        public uint Id = 0;
        public readonly float DurationMin, DurationMax, ResourcesMin, ResourcesMax;
        public readonly string Title;
        public float Duration {get; set;}
        public float Resources = 0,
            ES = 0, LS = 0, EE = 0, LE = 0, FR = 0, PR = 0, FreeR = 0, IR = 0, K = 0, tgA = 0, exp = 0, dis = 0;
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
            dis = (float)Math.Pow((DurationMax - DurationMin)/6, 2);
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
            dis = (float)Math.Pow((DurationMax - DurationMin) / 6, 2);
        }
        public Work()
        {

        }
    }
}
