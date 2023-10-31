using ourWinch.Models.Dashboard;
using System.ComponentModel.DataAnnotations.Schema;

public class ServiceSkjema
{

    public List<Mechanical> Mechanicals { get; set; }
    public List<Hydrolisk> Hydrolisks { get; set; }
    public List<Electro> Electros { get; set; }
    public List<FunksjonsTest> FunksjonsTests { get; set; }
    public List<Trykk> Trykks { get; set; }
 
}




