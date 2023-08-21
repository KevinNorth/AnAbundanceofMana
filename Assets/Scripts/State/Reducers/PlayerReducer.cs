using KitTraden.AnAbundanceOfMana.State.Actions;
using KitTraden.AnAbundanceOfMana.State.State;
using UnityEngine;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public class PlayerReducer : Reducer<PlayerState>
    {
        public override PlayerState Reduce<TAction>(TAction action, PlayerState state)
        {
            return action switch
            {
                ApplyBonusMutliplierToMultiplyAction => ApplyBonusMutliplierToMultiply(action as ApplyBonusMutliplierToMultiplyAction, state),
                GainBonusBaseManaAction => GainBonusBaseMana(action as GainBonusBaseManaAction, state),
                GainBonusMutliplierToAddAction => GainBonusMutliplierToAdd(action as GainBonusMutliplierToAddAction, state),
                GainEnergyAction => GainEnergy(action as GainEnergyAction, state),
                GainEnergyPerTurnAction => GainEnergyPerTurn(action as GainEnergyPerTurnAction, state),
                GainMulliganAction => GainMulligan(state),
                GainMultipleMulligansAction => GainMultipleMulligans(action as GainMultipleMulligansAction, state),
                LoseBonusBaseManaAction => LoseBonusBaseMana(action as LoseBonusBaseManaAction, state),
                LoseBonusMutliplierToAddAction => LoseBonusMutliplierToAdd(action as LoseBonusMutliplierToAddAction, state),
                LoseEnergyAction => LoseEnergy(action as LoseEnergyAction, state),
                LoseEnergyPerTurnAction => LoseEnergyPerTurn(action as LoseEnergyPerTurnAction, state),
                LoseMulliganAction => LoseMulligan(state),
                LoseMultipleMulligansAction => LoseMultipleMulligans(action as LoseMultipleMulligansAction, state),
                ResetCurrentBonusesAction => ResetCurrentBonuses(state),
                SetBonusBaseManaAction => SetBonusBaseMana(action as SetBonusBaseManaAction, state),
                SetBonusMutliplierToAddAction => SetBonusMutliplierToAdd(action as SetBonusMutliplierToAddAction, state),
                SetBonusMutliplierToMultiplyAction => SetBonusMutliplierToMultiply(action as SetBonusMutliplierToMultiplyAction, state),
                SetEnergyAction => SetEnergy(action as SetEnergyAction, state),
                SetEnergyPerTurnAction => SetEnergyPerTurn(action as SetEnergyPerTurnAction, state),
                SetMaxMulligansAction => SetMaxMulligans(action as SetMaxMulligansAction, state),
                SetMaxHandSizeAction => SetMaxHandSize(action as SetMaxHandSizeAction, state),
                SetStartofTurnBaseManaAction => SetStartofTurnBaseMana(action as SetStartofTurnBaseManaAction, state),
                SetStartOfTurnHandSizeAction => SetStartOfTurnHandSize(action as SetStartOfTurnHandSizeAction, state),
                SetStartofTurnMultiplierAction => SetStartofTurnMultiplier(action as SetStartofTurnMultiplierAction, state),
                _ => state,// Deliberately do nothing
            };
        }

        private PlayerState ApplyBonusMutliplierToMultiply(ApplyBonusMutliplierToMultiplyAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnMultiplierBonusToMultiply *= action.BonusMutiplierToMultiplyToApply;
            return newState;
        }

        private PlayerState GainBonusBaseMana(GainBonusBaseManaAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnBaseManaBonus += action.BonusBaseManaToGain;
            return newState;
        }

        private PlayerState GainBonusMutliplierToAdd(GainBonusMutliplierToAddAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnMultiplierBonusToAdd += action.BonusMutiplierToAddToGain;
            return newState;
        }

        private PlayerState GainEnergy(GainEnergyAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyRemaining += action.EnergyToGain;
            return newState;
        }

        private PlayerState GainEnergyPerTurn(GainEnergyPerTurnAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyPerTurn += action.EnergyPerTurnToGain;
            return newState;
        }

        private PlayerState GainMulligan(PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MulligansRemaining = Mathf.Min(newState.MulligansRemaining + 1, newState.MaxMulligans);
            return newState;
        }

        private PlayerState GainMultipleMulligans(GainMultipleMulligansAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MulligansRemaining = Mathf.Min(
                newState.MulligansRemaining + action.MulligansToGain,
                newState.MaxMulligans
            );
            return newState;
        }

        private PlayerState LoseBonusBaseMana(LoseBonusBaseManaAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnBaseManaBonus -= action.BonusBaseManaToLose;
            return newState;
        }

        private PlayerState LoseBonusMutliplierToAdd(LoseBonusMutliplierToAddAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnMultiplierBonusToAdd -= action.BonusMutiplierToAddToLose;
            return newState;
        }

        private PlayerState LoseEnergy(LoseEnergyAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyRemaining = Mathf.Max(
                newState.EnergyRemaining - action.EnergyToLose,
                0
            );
            return newState;
        }

        private PlayerState LoseEnergyPerTurn(LoseEnergyPerTurnAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyPerTurn = Mathf.Max(
                newState.EnergyPerTurn - action.EnergyPerTurnToLose,
                0
            );
            return newState;
        }

        private PlayerState LoseMulligan(PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MulligansRemaining = Mathf.Max(newState.MulligansRemaining - 1, 0);
            return newState;
        }

        private PlayerState LoseMultipleMulligans(LoseMultipleMulligansAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MulligansRemaining = Mathf.Max(
                newState.MulligansRemaining - action.MulligansToLose,
                0
            );
            return newState;
        }

        private PlayerState ResetCurrentBonuses(PlayerState state)
        {
            var newState = state.DeepCopy();

            newState.CurrentTurnBaseManaBonus = newState.StartOfTurnBaseMana;
            newState.CurrentTurnMultiplierBonusToAdd = newState.StartOfTurnMultiplier;
            newState.CurrentTurnMultiplierBonusToMultiply = 1;

            return newState;
        }

        private PlayerState SetBonusBaseMana(SetBonusBaseManaAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnBaseManaBonus = action.BonusBaseMana;
            return newState;
        }

        private PlayerState SetBonusMutliplierToAdd(SetBonusMutliplierToAddAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnMultiplierBonusToAdd = action.BonusMutiplierToAdd;
            return newState;
        }


        private PlayerState SetBonusMutliplierToMultiply(SetBonusMutliplierToMultiplyAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.CurrentTurnMultiplierBonusToMultiply = action.BonusMutiplierToMultiply;
            return newState;
        }

        private PlayerState SetEnergy(SetEnergyAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyRemaining = action.Energy;
            return newState;
        }

        private PlayerState SetEnergyPerTurn(SetEnergyPerTurnAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.EnergyPerTurn = action.EnergyPerTurn;
            return newState;
        }

        private PlayerState SetMaxMulligans(SetMaxMulligansAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MaxMulligans = action.MaxMulligans;
            return newState;
        }

        private PlayerState SetMaxHandSize(SetMaxHandSizeAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.MaxHandSize = action.MaxHandSize;
            return newState;
        }

        private PlayerState SetStartofTurnBaseMana(SetStartofTurnBaseManaAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.StartOfTurnBaseMana = action.StartOfTurnBaseMana;
            return newState;
        }

        private PlayerState SetStartOfTurnHandSize(SetStartOfTurnHandSizeAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.StartOfTurnHandSize = action.StartOfTurnHandSize;
            return newState;
        }

        private PlayerState SetStartofTurnMultiplier(SetStartofTurnMultiplierAction action, PlayerState state)
        {
            var newState = state.DeepCopy();
            newState.StartOfTurnMultiplier = action.StartOfTurnMultiplier;
            return newState;
        }
    }
}