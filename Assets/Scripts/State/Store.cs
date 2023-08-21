using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.Reducers;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State
{
    public class Store
    {
        private readonly CardsReducer cardsReducer;
        private readonly PlayerReducer playerReducer;
        private readonly SpellReducer spellReducer;
        private readonly StartOfTurnReducer startOfTurnReducer;

        private OverallState state;
        private readonly List<OverallState> previousStates;
        private readonly List<Action> history;

        public Store(StartOfSpellPayload startOfSpellPayload)
        {
            cardsReducer = new CardsReducer();
            playerReducer = new PlayerReducer();
            spellReducer = new SpellReducer();
            startOfTurnReducer = new StartOfTurnReducer();

            state = new OverallState(startOfSpellPayload);
            previousStates = new List<OverallState> { state };
            history = new List<Action>() { new InitialAction() };
        }

        public void Dispatch(Action action)
        {
            var newCardsState = cardsReducer.Reduce(action, state.cardsState);
            var newPlayerState = playerReducer.Reduce(action, state.playerState);
            var newSpellState = spellReducer.Reduce(action, state.spellState);
            var newStartOfTurnState = startOfTurnReducer.Reduce(action, state.startOfTurnState);

            var newState = new OverallState(newCardsState, newPlayerState, newSpellState, newStartOfTurnState);

            state = newState;
            previousStates.Add(newState);
            history.Add(action);
        }

        public void RestoreState(int index)
        {
            state = previousStates[index];

            if (index < history.Count - 1)
            {
                int itemsToRemove = (history.Count - index) - 1;
                previousStates.RemoveRange(index + 1, itemsToRemove);
                history.RemoveRange(index + 1, itemsToRemove);
            }
        }

        public OverallState GetState()
        {
            return state;
        }

        public IReadOnlyList<OverallState> GetPreviousStates()
        {
            return previousStates;
        }

        public IReadOnlyList<Action> GetHistory()
        {
            return history;
        }
    }
}