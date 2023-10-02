using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class PressureSettings
    {

        [Key]
        public int SettingId { get; set; }
        public int FormId { get; set; }
        public float PressureBar { get; set; }

        public InspectionForm InspectionForm { get; set; }
    }

}
