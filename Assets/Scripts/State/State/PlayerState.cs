using System;
using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.State.State
{
    [Serializable]
    public class PlayerState
    {
        public int MulligansRemaining;
        public int MaxMulligans;
        public int EnergyRemaining;
        public int EnergyPerTurn;
        public int StartOfTurnHandSize;
        public int MaxHandSize;
        public int StartOfTurnBaseMana;
        public decimal StartOfTurnMultiplier;
        public int CurrentTurnBaseManaBonus;
        public decimal CurrentTurnMultiplierBonusToAdd;
        public decimal CurrentTurnMultiplierBonusToMultiply;

        public PlayerState(
            int mulligansRemaining,
            int maxMulligans,
            int energyRemaining,
            int energyPerTurn,
            int startOfTurnHandSize,
            int maxHandSize,
            int startOfTurnBaseMana,
            decimal startOfTurnMultiplier,
            int currentTurnBaseManaBonus,
            decimal currentTurnMultiplierBonusToAdd,
            decimal currentTurnMultiplierBonusToMultiply
        )
        {
            MulligansRemaining = mulligansRemaining;
            MaxMulligans = maxMulligans;
            EnergyRemaining = energyRemaining;
            EnergyPerTurn = energyPerTurn;
            StartOfTurnHandSize = startOfTurnHandSize;
            MaxHandSize = maxHandSize;
            StartOfTurnBaseMana = startOfTurnBaseMana;
            StartOfTurnMultiplier = startOfTurnMultiplier;
            CurrentTurnBaseManaBonus = currentTurnBaseManaBonus;
            CurrentTurnMultiplierBonusToAdd = currentTurnMultiplierBonusToAdd;
            CurrentTurnMultiplierBonusToMultiply = currentTurnMultiplierBonusToMultiply;
        }

        public PlayerState DeepCopy()
        {
            return new PlayerState(
                MulligansRemaining,
                MaxMulligans,
                EnergyRemaining,
                EnergyPerTurn,
                StartOfTurnHandSize,
                MaxHandSize,
                StartOfTurnBaseMana,
                StartOfTurnMultiplier,
                CurrentTurnBaseManaBonus,
                CurrentTurnMultiplierBonusToAdd,
                CurrentTurnMultiplierBonusToMultiply
            );
        }
    }
}
