using System;

namespace SE2_Oefentoets
{
    public class Verkoop : IComparable<Verkoop>
    {
        public Verkoop(Drank drank)
        {
            Tijdstip = DateTime.Now;
            Drank = drank;
        }

        public DateTime Tijdstip { get; set; }
        public Drank Drank { get; set; }

        public int CompareTo(Verkoop other) => Tijdstip.CompareTo(other.Tijdstip);

        public override string ToString() => $"Tijdstip: {Tijdstip}, Drank: {Drank}";
    }
}