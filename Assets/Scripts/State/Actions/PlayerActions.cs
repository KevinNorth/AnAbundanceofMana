namespace KitTraden.AnAbundanceOfMana.State.Actions
{
    public class ApplyBonusMutliplierToMultiplyAction : Action
    {
        public decimal BonusMutiplierToMultiplyToApply;
    }

    public class GainBonusBaseManaAction : Action
    {
        public int BonusBaseManaToGain;
    }

    public class GainBonusMutliplierToAddAction : Action
    {
        public decimal BonusMutiplierToAddToGain;
    }

    public class GainEnergyAction : Action
    {
        public int EnergyToGain;
    }

    public class GainEnergyPerTurnAction : Action
    {
        public int EnergyPerTurnToGain;
    }

    public class GainMulliganAction : Action { }

    public class GainMultipleMulligansAction : Action
    {
        public int MulligansToGain;
    }

    public class LoseBonusBaseManaAction : Action
    {
        public int BonusBaseManaToLose;
    }

    public class LoseBonusMutliplierToAddAction : Action
    {
        public decimal BonusMutiplierToAddToLose;
    }

    public class LoseEnergyAction : Action
    {
        public int EnergyToLose;
    }

    public class LoseEnergyPerTurnAction : Action
    {
        public int EnergyPerTurnToLose;
    }

    public class LoseMulliganAction : Action { }

    public class LoseMultipleMulligansAction : Action
    {
        public int MulligansToLose;
    }

    public class ResetCurrentBonusesAction : Action { }

    public class SetBonusBaseManaAction : Action
    {
        public int BonusBaseMana;
    }

    public class SetBonusMutliplierToAddAction : Action
    {
        public decimal BonusMutiplierToAdd;
    }

    public class SetBonusMutliplierToMultiplyAction : Action
    {
        public decimal BonusMutiplierToMultiply;
    }


    public class SetEnergyAction : Action
    {
        public int Energy;
    }

    public class SetEnergyPerTurnAction : Action
    {
        public int EnergyPerTurn;
    }

    public class SetMaxMulligansAction : Action
    {
        public int MaxMulligans;
    }

    public class SetMaxHandSizeAction : Action
    {
        public int MaxHandSize;
    }

    public class SetStartofTurnBaseManaAction : Action
    {
        public int StartOfTurnBaseMana;
    }

    public class SetStartOfTurnHandSizeAction : Action
    {
        public int StartOfTurnHandSize;
    }

    public class SetStartofTurnMultiplierAction : Action
    {
        public decimal StartOfTurnMultiplier;
    }
}