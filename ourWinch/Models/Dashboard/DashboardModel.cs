using System.Collections.Generic;  

namespace ourWinch.Models.Dashboard
{
    public class DashboardModel
    {
        public string? Title { get; set; }
        public List<string> SideMenuItems { get; set; } = new List<string>();
    }
}
