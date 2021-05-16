using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Distributions;
using System.Text;
using System.Web.UI.DataVisualization;
using System.Web.UI.DataVisualization.Charting;

namespace QSBMODLibraryNF.Classes
{
    public class EventGraphAnalyzer
    {
        private ProjectEvent primaryEvent, finalEvent;

        private float durationNomValue = 0;
        private readonly EventGraph currentEventGraph;                
        private readonly List<Work> orderedProjectWorks, criticalWorks;
        private readonly List<ProjectEvent> orderedProjectEvents, criticalPath;               
        private readonly List<List<Work>> worksStages;
        public readonly List<(float t, float c)> CallbackTC;
        public readonly List<(float t, float p)> CallbackTP;
        public int WorksCount
        {
            get
            {
                return currentEventGraph.WorksByTitle.Count();
            }
        }
        public float Cost
        {
            get
            {
                return orderedProjectWorks.Sum(g => g.Resources);
            }
        }
        public float CostMin
        {
            get
            {
                return orderedProjectWorks.Sum(g => g.ResourcesMin);
            }
        }
        public float Duration
        {
            get
            {
                return criticalWorks.Sum(g => g.Duration);
            }
        }
        public float DurationMax
        {
            get
            {
                return criticalWorks.Sum(g => g.DurationMax);
            }
        }
        public float DurationMin
        {
            get
            {
                return criticalWorks.Sum(g => g.DurationMin);
            }
        }      
        public float DurationNom
        {
            get
            {
                return durationNomValue;
            }
        }
        public float Dispersion
        {
            get
            {
                return criticalWorks.Sum(g => g.dis);
            }
        }
        public IEnumerable<ProjectEvent> ProjectEvents
        {
            get
            {
                foreach (var e in currentEventGraph.EventsByTitle.Values)
                    yield return e;
            }
        }
        public IEnumerable<Work> Works
        {
            get
            {
                foreach (var w in currentEventGraph.WorksByTitle.Values)
                    yield return w;
            }
        }
        public IEnumerable<Work> CriticalWorks
        {
            get
            {
                foreach (var w in criticalWorks)
                    yield return w;
            }
        }
        public EventGraphAnalyzer(EventGraph evGraph)
        {
            criticalWorks = new List<Work>();
            criticalPath = new List<ProjectEvent>();
            worksStages = new List<List<Work>>();
            CallbackTC = new List<(float t, float c)>();
            CallbackTP = new List<(float t, float c)>();
            
            currentEventGraph = evGraph;
            orderedProjectEvents = new List<ProjectEvent>(currentEventGraph.EventsByTitle.Count);
            orderedProjectWorks = new List<Work>(currentEventGraph.WorksByTitle.Count);
            
            Init();
            CallbackTC.Add((Cost, Duration));
        }
        private void CheckStartAndFin()
        {
            if (currentEventGraph.PrimaryEvents.Count == 0)
                throw new Exception("Нет начального события");
            if (currentEventGraph.FinalEvents.Count == 0)
                throw new Exception("Нет конечного события");
            if (currentEventGraph.PrimaryEvents.Count > 1)
                throw new Exception("Начальное событие должно быть только одно");
            if (currentEventGraph.FinalEvents.Count > 1)
                throw new Exception("Конечное событие должно быть только одно");
            primaryEvent = currentEventGraph.PrimaryEvents.First();
            finalEvent = currentEventGraph.FinalEvents.First();
        }
        private void Init()
        {
            CheckStartAndFin();          
            uint id = 0u, cpid = 0u;
            var visitedEvents = new HashSet<ProjectEvent>();
            var events = new List<ProjectEvent>
            {
                primaryEvent
            };
            while (events.Count > 0)
            {
                var tempEvents = new List<ProjectEvent>();
                var tempWorks = new List<Work>();

                foreach (var currEvent in events)
                {
                    tempWorks.AddRange(currEvent.FollowingWorks);
                    currEvent.FollowingWorks.ForEach(w => w.CPId = cpid);
                    if (visitedEvents.Add(currEvent))
                    {
                        currEvent.Id = ++id;
                        orderedProjectEvents.Add(currEvent);
                        orderedProjectWorks.AddRange(currEvent.FollowingWorks);
                    }
                    currEvent.FollowingWorks.Where(w => w.SecondEvent.PreviousWorks
                        .All(v => visitedEvents
                            .Contains(v.FirstEvent)))
                        .ToList()
                     .ForEach(v => tempEvents
                        .Add(v.SecondEvent));
                }
                events = tempEvents;

                if (tempWorks.Count > 0)
                {
                    worksStages.Add(tempWorks);
                    cpid++;
                }
            }
            CalcEventESLS();
            FindCriticalPath();
            durationNomValue = criticalWorks.Sum(g => g.Duration);
            CalcWorksCoefs();
            CalcProbabilityDuration();
        }
        private void CalcEventESLS()
        {
            for (int i = 1; i < orderedProjectEvents.Count; i++)
                orderedProjectEvents[i].ES = orderedProjectEvents[i].PreviousWorks.Max(fw => fw.Duration + fw.FirstEvent.ES);
            finalEvent.LS = finalEvent.ES;
            for (int i = 2; i < orderedProjectEvents.Count; i++)
                orderedProjectEvents[orderedProjectEvents.Count - i].LS = orderedProjectEvents[orderedProjectEvents.Count - i].FollowingWorks.Min(fw => fw.SecondEvent.LS - fw.Duration);

        }
        private void FindCriticalPath()
        {
            var tempEvent = primaryEvent;
            
            while (tempEvent != null)
            {
                criticalPath.Add(tempEvent);
                var tempWork = tempEvent.FollowingWorks.FirstOrDefault(
                    fw => Math.Round(fw.SecondEvent.ES, 2) == Math.Round(fw.SecondEvent.LS, 2));
                if (tempWork is null)
                    tempEvent = null;
                else
                {
                    criticalWorks.Add(tempWork);
                    tempWork.IsCritical = true;
                    tempEvent = tempWork.SecondEvent;
                }
            }
        }
        private void CalcWorksCoefs()
        {
            foreach (var w in orderedProjectWorks)
            {
                w.ES = w.FirstEvent.ES;
                w.EE = w.ES + w.Duration;
                w.LE = w.SecondEvent.LS;
                w.LS = w.LE - w.Duration;
                w.FR = w.SecondEvent.LS - w.FirstEvent.ES - w.Duration;
                w.PR = w.SecondEvent.LS - w.FirstEvent.LS - w.Duration;
                w.IR = w.SecondEvent.ES - w.FirstEvent.LS - w.Duration;
                w.FreeR = w.SecondEvent.ES - w.FirstEvent.ES - w.Duration;
                var wFromCP = worksStages[(int)w.CPId].FirstOrDefault(ws => ws.IsCritical == true);
                if (wFromCP is null)
                    w.K = 0;
                else
                    w.K = 1 - w.FR / (Cost - wFromCP.Duration);
            }
        }
        private float NormalDistribution(float x, float avg=0, float avgDev=1, float integ=1)
        {
            return (float)(1 / (avgDev * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow((x - avg), 2) / (2 * Math.Pow(avgDev, 2))));
        }
        private void CalcProbabilityDuration()
        {
            var callbackTemp = new List<(float t, float p)>();
            float DMIN = (float)Math.Floor(DurationMin), DMAX = (float)Math.Ceiling(DurationMax);
            for (float D = DMIN; D < DMAX; D += 0.01F)
                callbackTemp.Add((D, NormalDistribution(D, DurationNom, Dispersion)));
            float probTmp;
            for (int i = 0; i < callbackTemp.Count; i++)
            {
                probTmp = 0;
                for (int j = 0; j < i; j++)
                    probTmp += callbackTemp[j].p;
                CallbackTP.Add((callbackTemp[i].t, probTmp));
            }
        }
        public float Optimize(float t=1)
        {
            Work minimalTgaWork = new Work
            {
                tgA = float.MaxValue
            };
            //criticalWorks.ForEach(
            //    w =>
            //    minimalTgaWork = (
            //          w.tgA < minimalTgaWork.tgA &&
            //          w.DurationMin <= w.Duration - t &&
            //          Math.Round(w.ResourcesMax, 2) >= Math.Round(w.Resources + w.tgA * t, 2))
            //          ? w : minimalTgaWork);
            criticalWorks.ForEach(
                w =>
                minimalTgaWork = 
                      w.tgA < minimalTgaWork.tgA &&
                      w.DurationMin <= w.Duration - t
                      ? w : minimalTgaWork);
            if (minimalTgaWork.IsCritical)
            {
                minimalTgaWork.Duration -= t;
                minimalTgaWork.Resources += minimalTgaWork.tgA * t;
                CallbackTC.Add((Cost, Duration));
            }
            return Duration;
        }
        public void OptimizeFull()
        {
            float prevOpt, currOpt = 0;
            do
            {
                prevOpt = currOpt;
                currOpt = Optimize(0.1F);
            } 
            while (prevOpt != currOpt);              
        }
    }
}
