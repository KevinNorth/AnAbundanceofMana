using System;
using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class PlayerView
    {
        public int MulligansRemaining;
        public int MaxMulligans;
        public int EnergyRemaining;
        public int EnergyPerTurn;
        public int StartOfTurnHandSize;
        public int MaxHandSize;
        public int StartOfTurnBaseMana;
        public decimal StartofTurnMultiplier;
        public int CurrentTurnBaseManaBonus;
        public decimal CurrentTurnMultiplierBonusToAdd;
        public decimal CurrentTurnMultiplierBonusToMultiply;

        public PlayerView(
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
            StartofTurnMultiplier = startOfTurnMultiplier;
            CurrentTurnBaseManaBonus = currentTurnBaseManaBonus;
            CurrentTurnMultiplierBonusToAdd = currentTurnMultiplierBonusToAdd;
            CurrentTurnMultiplierBonusToMultiply = currentTurnMultiplierBonusToMultiply;
        }

        public PlayerView DeepCopy()
        {
            return new PlayerView(
                MulligansRemaining,
                MaxMulligans,
                EnergyRemaining,
                EnergyPerTurn,
                StartOfTurnHandSize,
                MaxHandSize,
                StartOfTurnBaseMana,
                StartofTurnMultiplier,
                CurrentTurnBaseManaBonus,
                CurrentTurnMultiplierBonusToAdd,
                CurrentTurnMultiplierBonusToMultiply
            );
        }
    }
}
