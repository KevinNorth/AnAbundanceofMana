using System;
using Sirenix.Serialization.Utilities;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class StartOfTurnState
    {
        public CardsState CardsStateAtStartOfTurn;
        public PlayerState PlayerStateAtStartOfTurn;
        public SpellState SpellStateAtStartOfTurn;

        public StartOfTurnState(
            CardsState cardsStateAtStartOfTurn,
            PlayerState playerStateAtStartOfTurn,
            SpellState spellStateAtStartofTurn
        )
        {
            CardsStateAtStartOfTurn = cardsStateAtStartOfTurn;
            PlayerStateAtStartOfTurn = playerStateAtStartOfTurn;
            SpellStateAtStartOfTurn = spellStateAtStartofTurn;
        }
    }
}