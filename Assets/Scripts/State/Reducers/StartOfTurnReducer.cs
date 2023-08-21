using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public class StartOfTurnReducer : Reducer<StartOfTurnState>
    {
        public override StartOfTurnState Reduce<TAction>(TAction action, StartOfTurnState state)
        {
            return action switch
            {
                StartNewTurnAction => StartNewTurn(action as StartNewTurnAction),
                _ => state,// Deliberately do nothing
            };
        }

        private StartOfTurnState StartNewTurn(StartNewTurnAction action)
        {
            return new StartOfTurnState(
                action.CardsStateAtStartOfTurn,
                action.PlayerStateAtStartOfTurn,
                action.SpellStateAtStartOfTurn
            );
        }
    }
}