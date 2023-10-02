using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class InspectionForm
    {

        [Key]
        public int FormId { get; set; }
        public string SerieNr { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public string Signature { get; set; }
        public DateTime Date { get; set; }

        public List<Checkpoints> Checkpoints { get; set; }
        public PressureSettings PressureSetting { get; set; }
    }

 
}
