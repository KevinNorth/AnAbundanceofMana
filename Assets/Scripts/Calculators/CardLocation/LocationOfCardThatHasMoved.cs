using System;
using KitTraden.AnAbundanceOfMana.State.Entities;

namespace KitTraden.AnAbundanceOfMana.Calculators.CardLocation
{
    public struct LocationOfCardThatHasMoved : IEquatable<LocationOfCardThatHasMoved>
    {
        public Card cardInPreviousState;
        public Card cardInCurrentState;
        public CardLocation locationInPreviousState;
        public CardLocation locationInCurrentState;

        public string CardUUID()
        {
            return cardInCurrentState.UUID;
        }

        public bool Equals(LocationOfCardThatHasMoved other)
        {
            return AllValuesCardEqualityComparer.INSTANCE.Equals(cardInPreviousState, other.cardInPreviousState)
                && AllValuesCardEqualityComparer.INSTANCE.Equals(cardInCurrentState, other.cardInCurrentState)
                && locationInPreviousState.Equals(other.locationInPreviousState)
                && locationInCurrentState.Equals(other.locationInCurrentState);
        }

        public override string ToString()
        {
            return $"Previous: {cardInPreviousState?.Name ?? " null"} {locationInPreviousState}, Current: {cardInCurrentState?.Name ?? " null"} {locationInCurrentState}";
        }
    }
}