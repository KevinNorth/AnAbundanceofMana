using System;

namespace KitTraden.AnAbundanceOfMana.Calculators.CardLocation
{
    public struct CardLocation : IEquatable<CardLocation>
    {
        public enum LocationType
        {
            DECK,
            DISCARD_PILE,
            HAND,
            COMBO,
            EXHAUST_PILE,
            SPENT_POWERS_PILE,
            NONE
        }

        public LocationType location;
        public int index;

        public bool Equals(CardLocation other)
        {
            return (location == other.location) && (index == other.index);
        }

        public override string ToString()
        {
            return $"{location} {index:0}";
        }
    }
}