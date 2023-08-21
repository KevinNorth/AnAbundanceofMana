using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.MVC.Models.Cards;
using SpellModel = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.Spell;

namespace KitTraden.AnAbundanceOfMana.State
{
    public struct StartOfSpellPayload
    {
        public int MulligansRemaining;
        public int MaxMulligans;
        public int EnergyPerTurn;
        public int StartOfTurnHandSize;
        public int StartOfTurnBaseMana;
        public int StartofTurnMultiplier;
        public int MaxHandSize;
        public List<Card> PermanentDeck;
        public SpellModel Spell;
    }
}