using System.ComponentModel.DataAnnotations;

namespace ourWinch.Models
{
    public class SjekkListe
    {
        [Key]
        public int SjekkListeId { get; set; }

        public int OrderId { get; set; }

        // Mekanik Sjekk
        public int MekanikSjekkId { get; set; }
        public string MekanikSjekkListe { get; set; }
        public string MekanikSjekkStatus { get; set; }
        public string MekanikKommentar { get; set; }

        // Hidrolik Sjekk
        public int HydrolikSjekkId { get; set; }
        public string HydrolikSjekkListe { get; set; }
        public string HydrolikSjekkStatus { get; set; }
        public string HydrolikKommentar { get; set; }

        // Elektronik Sjekk
        public int ElektroSjekkId { get; set; }
        public string ElektroSjekkListe { get; set; }
        public string ElektroSjekkStatus { get; set; }
        public string ElektroKommentar { get; set; }

        // Fonksiyon Sjekk
        public int FonksjonSjekkId { get; set; }
        public string FonksjonSjekkListe { get; set; }
        public string FonksjonSjekkStatus { get; set; }
        public string FonksjonKommentar { get; set; }

        // Trykk Sjekk
        public int TrykkSjekkId { get; set; }
        public string TrykkSjekkListe { get; set; }
        public string TrykkSjekkStatus { get; set; }
        public string TrykkKommentar { get; set; }
    }
}