using System.Collections.Generic;
using System.Linq;

namespace WFApp.Classes
{
    public class EventGraph
    {
        private ProjectEvent PrimaryEvent, FinalEvent;
        private HashSet<ProjectEvent> PrimaryEvents, FinalEvents;
        private List<Work> CriticalPath;
        private List<ProjectEvent> OrderedProjectPath;
        private Dictionary<string, Work> WorksByTitle;
        private Dictionary<string, ProjectEvent> EventsByTitle;
        public EventGraph()
        {
            PrimaryEvents = new HashSet<ProjectEvent>();
            FinalEvents = new HashSet<ProjectEvent>();
            CriticalPath = new List<Work>();
            WorksByTitle = new Dictionary<string, Work>();
            EventsByTitle = new Dictionary<string, ProjectEvent>();
        }
        public void AddWork(Work work)
        {
            ProjectEvent eventTemp = null;
            if (!EventsByTitle.TryGetValue(work.PreviousEventTitle, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.PreviousEventTitle);
                EventsByTitle.Add(work.PreviousEventTitle, eventTemp);
                PrimaryEvents.Add(eventTemp);
            }
            work.PreviousEvent = eventTemp;
            eventTemp.FollowingWorks.Add(work);
            FinalEvents.Remove(eventTemp);
            if (!EventsByTitle.TryGetValue(work.FollowingEventTitle, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.FollowingEventTitle);
                EventsByTitle.Add(work.FollowingEventTitle, eventTemp);
                FinalEvents.Add(eventTemp);
            }
            work.FollowingEvent = eventTemp;
            WorksByTitle.Add(work.Title, work);
            eventTemp.PreviousWorks.Add(work);
            PrimaryEvents.Remove(eventTemp);
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
                        fw.PreviousEventTitle = PrimaryEvent.Title;
                        fw.PreviousEvent = PrimaryEvent;
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
                        fw.FollowingEventTitle = FinalEvent.Title;
                        fw.FollowingEvent = FinalEvent;
                        FinalEvent.PreviousWorks.Add(fw);
                    }
                }
        }
        private void Init() 
        {
            OutsideEventsUnion();
            
            var id = 0u;
            OrderedProjectPath = new List<ProjectEvent>(EventsByTitle.Count);
            var visitideEvents = new HashSet<ProjectEvent>();
            var events = new List<ProjectEvent>();

            events.Add(PrimaryEvent);
            
            while (events.Count != 0)
            {
                List<ProjectEvent> newEvents = new List<ProjectEvent>();
                foreach (var currEvent in events)
                {                                  
                    if (visitideEvents.Add(currEvent))
                    {
                        currEvent.Id = ++id;
                        OrderedProjectPath.Add(currEvent);
                    }                        
                    currEvent.FollowingWorks.Where(w => w.FollowingEvent.PreviousWorks
                                                .All(v => visitideEvents
                                                    .Contains(v.PreviousEvent)))
                                                .ToList()
                                             .ForEach(v => newEvents
                                                .Add(v.FollowingEvent));
                }
                events = newEvents;
            }
        }
        private void ESCalc()
        {
            for (int i = 1; i < OrderedProjectPath.Count; i++)
                OrderedProjectPath[i].ES = OrderedProjectPath[i].PreviousWorks.Max(fw => fw.Duration + fw.PreviousEvent.ES);
        }
        private void LCCalc()
        {
            FinalEvent.LC = FinalEvent.ES;
            for (int i = OrderedProjectPath.Count - 2; i >= 0; i--)
                OrderedProjectPath[i].LC = OrderedProjectPath[i].FollowingWorks.Min(fw => fw.FollowingEvent.LC - fw.Duration);
        }
        public void FindCriticalPath()
        {
            Init();
            ESCalc();
            LCCalc();
        }
    }
}
