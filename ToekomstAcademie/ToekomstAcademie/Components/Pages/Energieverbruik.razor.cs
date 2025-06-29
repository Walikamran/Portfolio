using Microsoft.AspNetCore.Components;

public class EnergieverbruikBase : ComponentBase
{
    protected double HuidigVerbruik = 0;
    protected double PrijsPerUur = 24;
    protected double TotaleKosten = 28;

    protected List<(DateTime Timestamp, double Verbruik)> VerbruikHistoriek = new();
    protected Random random = new();

    protected override void OnInitialized()
    {
        for (int i = 0; i < 10; i++)
        {
            VernieuwVerbruik();
        }
    }

    protected void VernieuwVerbruik()
    {
        HuidigVerbruik = Math.Round(random.NextDouble() * 500, 2);
        TotaleKosten += Math.Round(HuidigVerbruik * 0.0002, 2); // Simulatie
        VerbruikHistoriek.Insert(0, (DateTime.Now, HuidigVerbruik));
        if (VerbruikHistoriek.Count > 10)
            VerbruikHistoriek.RemoveAt(VerbruikHistoriek.Count - 1);
    }

    protected void OpenZaptecGo() => Console.WriteLine("Zaptec Go clicked.");
    protected void VerdienTegoed() => Console.WriteLine("Verdien Tegoed clicked.");
    protected void OpenStore() => Console.WriteLine("Store clicked.");
    protected void VoegToe() => Console.WriteLine("Voeg Toe clicked.");
}
