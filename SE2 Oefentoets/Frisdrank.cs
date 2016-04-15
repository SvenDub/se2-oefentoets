namespace SE2_Oefentoets
{
    public class Frisdrank : Drank
    {
        public Frisdrank(string naam, decimal prijs, int milliliter, int gramSuiker)
            : base(naam, prijs, milliliter, false)
        {
            GramSuiker = gramSuiker;
        }

        public int GramSuiker { get; set; }

        public override string ToString() => $"{base.ToString()}, GramSuiker: {GramSuiker}";
    }
}