namespace SE2_Oefentoets
{
    public class Beker : IVoorraad
    {
        public Beker(string naam, int milliliter, bool warmeDrankMogelijk)
        {
            Naam = naam;
            Milliliter = milliliter;
            WarmeDrankMogelijk = warmeDrankMogelijk;
        }

        public int Milliliter { get; set; }
        public bool WarmeDrankMogelijk { get; set; }
        public string Naam { get; set; }
        public int Voorraad { get; set; }

        public override string ToString()
            => $"Naam: {Naam}, Voorraad: {Voorraad}, Milliliter: {Milliliter}, WarmeDrankMogelijk: {WarmeDrankMogelijk}";
    }
}