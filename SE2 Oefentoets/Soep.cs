namespace SE2_Oefentoets
{
    public class Soep : Drank
    {
        public Soep(string naam, decimal prijs, int milliliter) : base(naam, prijs, milliliter, true)
        {
        }

        public override string ToString() => $"{base.ToString()}";
    }
}