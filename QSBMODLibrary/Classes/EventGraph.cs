using System.Collections.Generic;
using System.Linq;

namespace QSBMODLibrary.Classes
{
    public class EventGraph
    {
        private ProjectEvent PrimaryEvent, FinalEvent;
        private HashSet<ProjectEvent> PrimaryEvents, FinalEvents;
        private List<List<Work>> WorksDuringsCP;
        public List<Work> OrderedProjectWorks, CriticalWorks;
        private List<ProjectEvent> OrderedProjectPath, CriticalPath;
        private Dictionary<string, Work> WorksByTitle;
        private Dictionary<string, ProjectEvent> EventsByTitle;
        public float Cost
        {
            get
            {
                try
                {
                    return OrderedProjectWorks.Sum(g => g.Resources);
                }
                catch (System.Exception ex)
                {
                    Loger.Msg(ex);
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
                    return CriticalWorks.Sum(g => g.Duration);
                }
                catch (System.Exception ex)
                {
                    Loger.Msg(ex);
                    return 0;
                }         
            }
        }
        public EventGraph()
        {
            WorksDuringsCP = new List<List<Work>>();
            CriticalWorks = new List<Work>();
            PrimaryEvents = new HashSet<ProjectEvent>();
            FinalEvents = new HashSet<ProjectEvent>();
            CriticalPath = new List<ProjectEvent>();
            WorksByTitle = new Dictionary<string, Work>();
            EventsByTitle = new Dictionary<string, ProjectEvent>();
        }
        public void AddWork(Work work)
        {
            WorksByTitle.Add(work.Title, work);
            ProjectEvent eventTemp = null;
            if (!EventsByTitle.TryGetValue(work.FirstEventTitle, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.FirstEventTitle);
                EventsByTitle.Add(work.FirstEventTitle, eventTemp);
                PrimaryEvents.Add(eventTemp);
            }
            work.FirstEvent = eventTemp;
            eventTemp.FollowingWorks.Add(work);
            FinalEvents.Remove(eventTemp);
            if (!EventsByTitle.TryGetValue(work.SecondEventTitle, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.SecondEventTitle);
                EventsByTitle.Add(work.SecondEventTitle, eventTemp);
                FinalEvents.Add(eventTemp);
            }
            work.SecondEvent = eventTemp;            
            eventTemp.PreviousWorks.Add(work);
            PrimaryEvents.Remove(eventTemp);
        }
        private void CheckStartAndFin()
        {
            if (PrimaryEvents.Count == 0)
                throw new System.Exception("Нет начального события");
            if (FinalEvents.Count == 0)
                throw new System.Exception("Нет конечного события");
        }
        private void OutsideEventsUnion()
        {           
            var primaryList = PrimaryEvents.ToList();
            PrimaryEvent = primaryList[0];
            if (PrimaryEvents.Count > 1)
                for (int i = 1; i < primaryList.Count; i++)
                {
                    foreach (var fw in primaryList[i].FollowingWorks)
                    {
                        fw.FirstEventTitle = PrimaryEvent.Title;
                        fw.FirstEvent = PrimaryEvent;
                        PrimaryEvent.FollowingWorks.Add(fw);
                    }
                }
            var finalList = FinalEvents.ToList();
            FinalEvent = finalList[0];
            if (FinalEvents.Count > 1)
                for (int i = 1; i < finalList.Count; i++)
                {
                    foreach (var fw in finalList[i].PreviousWorks)
                    {
                        fw.SecondEventTitle = FinalEvent.Title;
                        fw.SecondEvent = FinalEvent;
                        FinalEvent.PreviousWorks.Add(fw);
                    }
                }
        }
        private void Init() 
        {
            CheckStartAndFin();
            OutsideEventsUnion();
            OrderedProjectPath = new List<ProjectEvent>(EventsByTitle.Count);
            OrderedProjectWorks = new List<Work>(WorksByTitle.Count);
            uint id = 0u, cpid = 0u;
            var visitideEvents = new HashSet<ProjectEvent>();
            var events = new List<ProjectEvent>();
            events.Add(PrimaryEvent);      
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
                        OrderedProjectPath.Add(currEvent);
                        OrderedProjectWorks.AddRange(currEvent.FollowingWorks);
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
                    WorksDuringsCP.Add(tempWorks);
                    cpid++;
                }
            }
        }
        private void EventESLSCalc()
        {
            for (int i = 1; i < OrderedProjectPath.Count; i++)
                OrderedProjectPath[i].ES = OrderedProjectPath[i].PreviousWorks.Max(fw => fw.Duration + fw.FirstEvent.ES);
            FinalEvent.LS = FinalEvent.ES;
            for (int i = OrderedProjectPath.Count - 2; i >= 0; i--)
                OrderedProjectPath[i].LS = OrderedProjectPath[i].FollowingWorks.Min(fw => fw.SecondEvent.LS - fw.Duration);
        }
        private void WorksCoefsCalc()
        {
            foreach (var w in OrderedProjectWorks)
            {
                w.ES = w.FirstEvent.ES;
                w.EE = w.ES + w.Duration;
                w.LE = w.SecondEvent.LS;
                w.LS = w.LE - w.Duration;
                w.FR = w.SecondEvent.LS - w.FirstEvent.ES - w.Duration;
                w.PR = w.SecondEvent.LS - w.FirstEvent.LS - w.Duration;
                w.IR = w.SecondEvent.ES - w.FirstEvent.LS - w.Duration;
                w.FreeR = w.SecondEvent.ES - w.FirstEvent.ES - w.Duration;
                w.K = 1 - w.FR / (Cost - WorksDuringsCP[(int)w.CPId].FirstOrDefault(w => w.IsCritical == true).Duration);
            }
        }
        public List<ProjectEvent> FindCriticalPath()
        {
            Init();
            EventESLSCalc();
            var tempEvent = PrimaryEvent;
            Work tempWork = null;
            while (tempEvent != null)
            {
                CriticalPath.Add(tempEvent);
                tempWork = tempEvent.FollowingWorks.FirstOrDefault(fw => fw.SecondEvent.ES == fw.SecondEvent.LS);
                if (tempWork is null)
                    tempEvent = null;
                else
                {
                    CriticalWorks.Add(tempWork);
                    tempWork.IsCritical = true;
                    tempEvent = tempWork.SecondEvent;
                }
            }
            WorksCoefsCalc();
            return CriticalPath;
        }
        public void OptimizeCriticalPath
    }
}
