using System.Collections.Generic;

namespace QSBMODLibrary.Classes
{
    public class EventGraph
    {
        public readonly HashSet<ProjectEvent> PrimaryEvents, FinalEvents;
        public readonly Dictionary<string, Work> WorksByTitle;
        public readonly Dictionary<string, ProjectEvent> EventsByTitle;
        public EventGraph()
        {            
            PrimaryEvents = new HashSet<ProjectEvent>();
            FinalEvents = new HashSet<ProjectEvent>();
            WorksByTitle = new Dictionary<string, Work>();
            EventsByTitle = new Dictionary<string, ProjectEvent>();
        }
        public void AddWork(Work work)
        {
            ProjectEvent eventTemp;
            WorksByTitle.Add(work.Title, work);
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
    }
}
