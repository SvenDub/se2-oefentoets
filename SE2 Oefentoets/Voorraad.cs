using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SE2_Oefentoets
{
    public class Voorraad
    {
        public readonly List<IVoorraad> HuidigeVoorraad = new List<IVoorraad>();
        public readonly List<Verkoop> Verkopen = new List<Verkoop>();

        /// <summary>
        ///     Voeg een nieuwe drank aan het systeem toe.
        /// </summary>
        /// <param name="drank">De toe te voegen drank.</param>
        /// <returns>True als het toevoegen gelukt is.</returns>
        public bool NieuwProduct(Drank drank)
        {
            if (HuidigeVoorraad.Contains(drank)) return false;
            HuidigeVoorraad.Add(drank);
            return true;
        }

        /// <summary>
        ///     Voeg een nieuwe beker aan het systeem toe.
        /// </summary>
        /// <param name="beker">De toe te voegen beker.</param>
        /// <returns>True als het toevoegen gelukt is.</returns>
        public bool NieuwProduct(Beker beker)
        {
            if (HuidigeVoorraad.Contains(beker)) return false;
            HuidigeVoorraad.Add(beker);
            return true;
        }

        /// <summary>
        ///     Een kopie van de lijst met beschikbare producten. Elementen die toegevoegd of verwijderd worden, worden niet
        ///     automatisch bijgewerkt in de originele lijst.
        /// </summary>
        /// <returns>Een kopie van de lijst met beschikbare producten.</returns>
        public List<IVoorraad> BeschikbareProducten()
        {
            return HuidigeVoorraad.ToList();
        }

        /// <summary>
        ///     Een kopie van de lijst met alle dranken die op voorraad zijn. Elementen die toegevoegd of verwijderd worden, worden
        ///     niet
        ///     automatisch bijgewerkt in de originele lijst.
        /// </summary>
        /// <returns>Een kopie van de lijst met alle dranken die op voorraad zijn.</returns>
        public List<Drank> VoorradigeDranken() => HuidigeVoorraad.OfType<Drank>()
            .Where(drank => drank.Voorraad > 0).ToList();

        /// <summary>
        ///     Voeg nieuwe voorraad toe aan een product.
        /// </summary>
        /// <param name="product">Het product om bij te vullen.</param>
        /// <param name="aantal">Het aantal bij te vullen producten.</param>
        public void VulBij(IVoorraad product, int aantal)
        {
            product.Voorraad += aantal;
        }

        /// <summary>
        ///     Serveer een drank en registreer dit in het logboek.
        ///     <para />
        ///     <para />
        ///     De drank wordt alleen geserveerd als:
        ///     <para />
        ///     - De inworp minstens gelijk is aan de prijs.
        ///     <para />
        ///     - Er een beker op voorraad is.
        ///     <para />
        ///     - De beker geschikt is voor het type drank.
        ///     <para />
        ///     - De inhoud van de beker groot genoeg is.
        /// </summary>
        /// <param name="drank"></param>
        /// <param name="inworp"></param>
        /// <returns></returns>
        public bool KoopDrank(Drank drank, decimal inworp)
        {
            if (!VoorradigeDranken().Contains(drank)) return false; // Drank niet beschikbaar
            if (drank.Prijs > inworp) return false; // Niet genoeg geld
            try
            {
                Beker gevondenBeker = BeschikbareProducten()
                    // Alle bekers...
                    .OfType<Beker>()
                    // ...die op voorraad zijn...
                    .Where(beker => beker.Voorraad > 0)
                    // ...en geschikt zijn voor de drank...
                    .Where(beker => !drank.WarmeDrank || beker.WarmeDrankMogelijk)
                    // ...en groot genoeg zijn...
                    .Where(beker => beker.Milliliter >= drank.Milliliter)
                    // ...gesorteerd van klein naar groot. Hierdoor worden eerst alle kleine bekers op gemaakt.
                    .OrderBy(beker => beker.Milliliter)
                    .First();

                gevondenBeker.Voorraad--;
                drank.Voorraad--;

                // Log
                Verkopen.Add(new Verkoop(drank));

                return true;
            }
            catch (InvalidOperationException)
            {
                throw new OnvoldoendeBekersException();
            }
        }

        /// <summary>
        ///     Schrijf alle verkopen op omgekeerd chronologische volgorde naar het aangegeven bestand.
        /// </summary>
        /// <param name="bestandsnaam">Het bestand waar de logs naar toe geschreven moeten worden.</param>
        public void ExporteerLogbestand(string bestandsnaam)
        {
            Verkopen.Sort();
            Verkopen.Reverse();

            File.WriteAllLines(bestandsnaam, Verkopen.Select(verkoop => verkoop.Tijdstip + " - " + verkoop.Drank.Naam));
        }

        public override string ToString() => $"HuidigeVoorraad: {HuidigeVoorraad.Count}, Verkopen: {Verkopen.Count}";
    }
}