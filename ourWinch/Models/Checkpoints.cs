using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class Checkpoints
    {
        [Key]
        public int CheckpointId { get; set; }
        public int FormId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

        public InspectionForm InspectionForm { get; set; }
    }
}
