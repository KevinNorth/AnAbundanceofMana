using System;
using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using KitTraden.AnAbundanceOfMana.State.Entities;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class CurrentPlayState
    {
        public enum PlayType
        {
            CARD
        }

        public PlayType Type;
        public List<(Card, CardEffect)> CardEffectsQueue;

        public CurrentPlayState(PlayType type, List<(Card, CardEffect)> cardEffectsQueue)
        {
            Type = type;
            CardEffectsQueue = cardEffectsQueue;
        }

        public CurrentPlayState DeepCopy()
        {
            var deepCopy = new CurrentPlayState(Type, new List<(Card, CardEffect)>());

            foreach (var (card, cardEffect) in CardEffectsQueue)
            {
                deepCopy.CardEffectsQueue.Add((new Card(card), cardEffect));
            }

            return deepCopy;
        }
    }
}