using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.State.Entities
{
    public class UUIDCardEqualityComparer : IEqualityComparer<Card>
    {
        public static UUIDCardEqualityComparer INSTANCE = new UUIDCardEqualityComparer();

        public bool Equals(Card x, Card y)
        {
            return x.UUID == y.UUID;
        }

        public int GetHashCode(Card card)
        {
            return card.UUID.GetHashCode();
        }
    }
}