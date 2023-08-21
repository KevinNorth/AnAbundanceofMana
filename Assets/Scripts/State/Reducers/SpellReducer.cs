using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public class SpellReducer : Reducer<SpellState>
    {
        public override SpellState Reduce<TAction>(TAction action, SpellState state)
        {
            return action switch
            {
                AdvancePhaseAction => AdvancePhase(state),
                JumpToPhaseAction => JumpToPhase(action as JumpToPhaseAction, state),
                _ => state,// Deliberately do nothing
            };
        }

        private SpellState AdvancePhase(SpellState state)
        {
            var newState = state.DeepCopy();
            newState.phaseIndex += 1;
            return newState;
        }

        private SpellState JumpToPhase(JumpToPhaseAction action, SpellState state)
        {
            var newState = state.DeepCopy();
            newState.phaseIndex = action.PhaseToJumpTo;
            return newState;
        }
    }
}