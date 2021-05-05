using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QSBMODLibrary.Classes
{
    public class EventGraphAnalyzer
    {
        private readonly EventGraph eventGraph;        
        
        private List<Work> orderedProjectWorks, criticalWorks;
        private List<ProjectEvent> orderedProjectPath, criticalPath;
        private ProjectEvent primaryEvent, finalEvent;        
        private List<List<Work>> worksStages;
        private List<(float t, float c)> callback;
        private bool isOptimized = false;
        public bool IsOptimized
        {
            get
            {
                return isOptimized;
            }
        }
        public int WorksCount
        {
            get
            {
                return eventGraph.WorksByTitle.Count();
            }
        }
        public float Cost
        {
            get
            {
                try
                {
                    return orderedProjectWorks.Sum(g => g.Resources);
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
        }
        public float Duration
        {
            get
            {
                try
                {
                    return criticalWorks.Sum(g => g.Duration);
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
        }
        public IEnumerable<Work> Works
        {
            get
            {
                foreach (var w in eventGraph.WorksByTitle.Values)
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
        public EventGraphAnalyzer(EventGraph _eventGraph)
        {
            orderedProjectWorks = new List<Work>();
            orderedProjectPath = new List<ProjectEvent>();
            criticalWorks = new List<Work>();
            criticalPath = new List<ProjectEvent>();
            worksStages = new List<List<Work>>();
            callback = new List<(float t, float c)>();
            eventGraph = _eventGraph;
            
            Init();
            callback.Add((Cost, Duration));
        }
        private void CheckStartAndFin()
        {
            if (eventGraph.PrimaryEvents.Count == 0)
                throw new System.Exception("Нет начального события");
            if (eventGraph.FinalEvents.Count == 0)
                throw new System.Exception("Нет конечного события");
        }
        private void OutsideEventsUnioning()
        {
            var primaryList = eventGraph.PrimaryEvents.ToList();
            primaryEvent = primaryList[0];
            if (eventGraph.PrimaryEvents.Count > 1)
                for (int i = 1; i < primaryList.Count; i++)
                {
                    foreach (var fw in primaryList[i].FollowingWorks)
                    {
                        fw.FirstEventTitle = primaryEvent.Title;
                        fw.FirstEvent = primaryEvent;
                        primaryEvent.FollowingWorks.Add(fw);
                    }
                }
            var finalList = eventGraph.FinalEvents.ToList();
            finalEvent = finalList[0];
            if (eventGraph.FinalEvents.Count > 1)
                for (int i = 1; i < finalList.Count; i++)
                {
                    foreach (var fw in finalList[i].PreviousWorks)
                    {
                        fw.SecondEventTitle = finalEvent.Title;
                        fw.SecondEvent = finalEvent;
                        finalEvent.PreviousWorks.Add(fw);
                    }
                }
        }
        private void Init()
        {
            CheckStartAndFin();
            OutsideEventsUnioning();
            orderedProjectPath = new List<ProjectEvent>(eventGraph.EventsByTitle.Count);
            orderedProjectWorks = new List<Work>(eventGraph.WorksByTitle.Count);
            uint id = 0u, cpid = 0u;
            var visitideEvents = new HashSet<ProjectEvent>();
            var events = new List<ProjectEvent>();
            events.Add(primaryEvent);
            while (events.Count > 0)
            {
                var tempEvents = new List<ProjectEvent>();
                var tempWorks = new List<Work>();

                foreach (var currEvent in events)
                {
                    tempWorks.AddRange(currEvent.FollowingWorks);
                    currEvent.FollowingWorks.ForEach(w => w.CPId = cpid);

                    if (visitideEvents.Add(currEvent))
                    {
                        currEvent.Id = id++;
                        orderedProjectPath.Add(currEvent);
                        orderedProjectWorks.AddRange(currEvent.FollowingWorks);
                    }
                    currEvent.FollowingWorks.Where(w => w.SecondEvent.PreviousWorks
                                                .All(v => visitideEvents
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
            EventESLSCalc();
            FindCriticalPath();
            WorksCoefsCalc();
        }
        private void EventESLSCalc()
        {
            for (int i = 1; i < orderedProjectPath.Count; i++)
                orderedProjectPath[i].ES = orderedProjectPath[i].PreviousWorks.Max(fw => fw.Duration + fw.FirstEvent.ES);
            finalEvent.LS = finalEvent.ES;
            for (int i = orderedProjectPath.Count - 2; i >= 0; i--)
                orderedProjectPath[i].LS = orderedProjectPath[i].FollowingWorks.Min(fw => fw.SecondEvent.LS - fw.Duration);
        }
        private void FindCriticalPath()
        {
            var tempEvent = primaryEvent;
            Work tempWork = null;
            while (tempEvent != null)
            {
                criticalPath.Add(tempEvent);
                tempWork = tempEvent.FollowingWorks.FirstOrDefault(fw => fw.SecondEvent.ES == fw.SecondEvent.LS);
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
        private void WorksCoefsCalc()
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
                w.K = 1 - w.FR / (Cost - worksStages[(int)w.CPId].FirstOrDefault(w => w.IsCritical == true).Duration);
            }
        }
        public void OptimizeForOneDay()
        {
            Work minimalTgaWork = new Work();
            criticalWorks.ForEach(
                w => minimalTgaWork = (w.tgA < minimalTgaWork.tgA &&
                Math.Round(w.ResourcesMax, 2) >= Math.Round(w.Resources + (w.DurationMax - w.Duration - 1) * w.tgA, 2) &&
                Math.Round(w.DurationMin, 2) <= Math.Round(w.Duration - 1, 2)) ? w : minimalTgaWork);
            if (minimalTgaWork.IsCritical)
            {
                minimalTgaWork.Duration -= 1;
                minimalTgaWork.Resources += (minimalTgaWork.DurationMax - minimalTgaWork.Duration - 1) * minimalTgaWork.tgA;
                callback.Add((Cost, Duration));
            }
            else
                isOptimized = true;
        }
        public void FullOptimize()
        {
            while (!IsOptimized)
                OptimizeForOneDay();
        }
    }
}
