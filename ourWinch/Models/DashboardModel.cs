using System.Collections.Generic;  // Bu satırı ekleyin

namespace OurWinch.Models
{
    public class DashboardModel
    {
        public string? Title { get; set; }
        public List<string> SideMenuItems { get; set; } = new List<string>();
    }
}
