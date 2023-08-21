using System;
using UnityEngine;

namespace KitTraden.AnAbundanceOfMana.Effects.CardEffects
{
    [Serializable]
    public abstract class CardEffect : IEquatable<CardEffect>
    {
        public override string ToString()
        {
            return $"{this.GetType().Name} {{{JsonUtility.ToJson(this)}}}";
        }

        public virtual bool Equals(CardEffect other)
        {
            return other.GetType().Equals(other.GetType());
        }
    }
}