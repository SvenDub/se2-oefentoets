using System;

namespace SE2_Oefentoets
{
    public class OnvoldoendeBekersException : Exception
    {
        public OnvoldoendeBekersException() : base("Niet genoeg bekers beschikbaar")
        {
        }
    }
}