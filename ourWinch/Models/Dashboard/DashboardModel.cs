using System.Collections.Generic;

namespace ourWinch.Models.Dashboard
{
    /// <summary>
    /// Represents the model for a dashboard view, containing title and side menu items.
    /// </summary>
    public class DashboardModel
    {
        /// <summary>
        /// Gets or sets the title of the dashboard.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the items to be displayed in the side menu of the dashboard.
        /// </summary>
        /// <value>
        /// The side menu items.
        /// </value>
        public List<string> SideMenuItems { get; set; } = new List<string>();
    }
}

