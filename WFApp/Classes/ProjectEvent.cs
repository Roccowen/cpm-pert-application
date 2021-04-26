using System;
using System.Collections.Generic;
using System.Text;

namespace WFApp.Classes
{
    public class ProjectEvent
    {
        public uint Id = 0;
        public float ES = 0, LC = 0;
        public string Title;
        public List<Work> PreviousWorks, FollowingWorks;
        public ProjectEvent(string title)
        {
            Title = title;
            PreviousWorks = new List<Work>();
            FollowingWorks = new List<Work>();
        }
    }
    
}
