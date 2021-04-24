using System.Collections.Generic;

namespace WFApp.Classes
{
    public class EventGraph
    {
        private ProjectEvent PrimaryEvent, FinalEvent;
        private List<ProjectEvent> CriticalPath;
        private Dictionary<string, Work> WorksByTitle;
        private Dictionary<string, ProjectEvent> EventsById;
        private ProjectEvent[] projectEvents;
        public EventGraph()
        {
            CriticalPath = new List<ProjectEvent>();
            WorksByTitle = new Dictionary<string, Work>();
            EventsById = new Dictionary<string, ProjectEvent>();
        }
        public void AddWork(Work work)
        {
            ProjectEvent eventTemp = null;
            if (!EventsById.TryGetValue(work.PreviousEventId, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.PreviousEventId);
                EventsById.Add(work.PreviousEventId, eventTemp);
            }
            work.PreviousEvent = eventTemp;
            eventTemp.FollowingWorks.Add(work);
            if (!EventsById.TryGetValue(work.FollowingEventId, out eventTemp))
            {
                eventTemp = new ProjectEvent(work.FollowingEventId);
                EventsById.Add(work.FollowingEventId, eventTemp);
            }
            work.FollowingEvent = eventTemp;
            WorksByTitle.Add(work.Title, work);
            eventTemp.PreviousWorks.Add(work);
        }
        private void SetIds() { 
        
        }
        public void Init()
        {
            projectEvents = new ProjectEvent[EventsById.Count + 1];
            foreach (var eventKV in EventsById)
            {
                projectEvents[eventKV.Key] = eventKV.Value;
            }
            //PrimaryEvent = EventsById.FirstOrDefault(e => e.Value.PreviousWorks.Count == 0).Value;
            //FinalEvent = EventsById.FirstOrDefault(e => e.Value.FollowingWorks.Count == 0).Value;
        }
        public void FindCriticalPath()
        {
            Init();
            var eventTemp = PrimaryEvent;
            eventTemp.ES =
            foreach (Work workF in eventTemp.PreviousWorks)
            {

            }
        }
    }
}
