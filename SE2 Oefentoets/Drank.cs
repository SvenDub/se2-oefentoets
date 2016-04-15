namespace SE2_Oefentoets
{
    public abstract class Drank : IVoorraad
    {
        protected Drank(string naam, decimal prijs, int milliliter, bool warmeDrank)
        {
            Naam = naam;
            Prijs = prijs;
            Milliliter = milliliter;
            WarmeDrank = warmeDrank;
        }

        public decimal Prijs { get; set; }
        public int Milliliter { get; set; }
        public bool WarmeDrank { get; set; }
        public string Naam { get; set; }
        public int Voorraad { get; set; }

        public override string ToString()
            => $"Naam: {Naam}, Voorraad: {Voorraad}, Prijs: {Prijs}, Milliliter: {Milliliter}, WarmeDrank: {WarmeDrank}";
    }
}