namespace SE2_Oefentoets
{
    public class Koffie : Drank
    {
        public Koffie(string naam, decimal prijs, int milliliter, int milligramCafeine)
            : base(naam, prijs, milliliter, true)
        {
            MilligramCafeine = milligramCafeine;
        }

        public int MilligramCafeine { get; set; }

        public override string ToString() => $"{base.ToString()}, MilligramCafeine: {MilligramCafeine}";
    }
}