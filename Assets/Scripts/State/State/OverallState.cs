using System;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class OverallState
    {
        public PlayerState playerState;
        public SpellState spellState;
        public CardsState cardsState; // Out of alphabetical order so it JSONifies better
        public StartOfTurnState startOfTurnState;

        public OverallState(StartOfSpellPayload payload)
        {
            cardsState = new CardsState(payload.PermanentDeck);
            playerState = new PlayerState(
                payload.MulligansRemaining,
                payload.MaxMulligans,
                payload.EnergyPerTurn,
                payload.EnergyPerTurn,
                payload.StartOfTurnHandSize,
                payload.MaxHandSize,
                payload.StartOfTurnBaseMana,
                payload.StartofTurnMultiplier,
                payload.StartOfTurnBaseMana,
                payload.StartofTurnMultiplier,
                1
            );
            spellState = new SpellState(payload.Spell);

            var cardsStateAtStartOfTurn = new CardsState(payload.PermanentDeck);
            var playerStateAtStartOfTurn = new PlayerState(
                payload.MulligansRemaining,
                payload.MaxMulligans,
                payload.EnergyPerTurn,
                payload.EnergyPerTurn,
                payload.StartOfTurnHandSize,
                payload.MaxHandSize,
                payload.StartOfTurnBaseMana,
                payload.StartofTurnMultiplier,
                payload.StartOfTurnBaseMana,
                payload.StartofTurnMultiplier,
                1
            );
            var spellStateAtStartOfTurn = new SpellState(payload.Spell);
            startOfTurnState = new StartOfTurnState(cardsStateAtStartOfTurn, playerStateAtStartOfTurn, spellStateAtStartOfTurn);
        }

        public OverallState(CardsState cardsState, PlayerState playerState, SpellState spellState, StartOfTurnState startOfTurnState)
        {
            this.cardsState = cardsState;
            this.playerState = playerState;
            this.spellState = spellState;
            this.startOfTurnState = startOfTurnState;
        }
    }
}