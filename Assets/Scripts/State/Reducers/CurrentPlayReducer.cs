using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.Entities;
using KitTraden.AnAbundanceOfMana.Effects.CardEffects;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public class CurrentPlayReducer : Reducer<CurrentPlayState>
    {
        public override CurrentPlayState Reduce<TAction>(TAction action, CurrentPlayState state)
        {
            return action switch
            {
                AddOnPlayCardEffectsToQueueAction => AddOnPlayCardEffectsToQueue(action as AddOnPlayCardEffectsToQueueAction, state),
                MarkCurrentCardEffectAsCompletedAction => MarkCurrentCardEffectAsCompleted(state),
                _ => state,// Deliberately do nothing
            };
        }

        private CurrentPlayState AddOnPlayCardEffectsToQueue(AddOnPlayCardEffectsToQueueAction action, CurrentPlayState state)
        {
            var newState = state.DeepCopy();

            foreach (var cardEffect in action.Card.OnPlayEffects)
            {
                newState.CardEffectsQueue.Add((action.Card, cardEffect));
            }

            return newState;
        }

        private CurrentPlayState MarkCurrentCardEffectAsCompleted(CurrentPlayState state)
        {
            var newState = state.DeepCopy();
            newState.CardEffectsQueue.RemoveAt(0);
            return newState;
        }
    }
}
