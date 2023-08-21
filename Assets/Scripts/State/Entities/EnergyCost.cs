using System;
using UnityEngine;
using ModelCost = KitTraden.AnAbundanceOfMana.MVC.Models.Cards.EnergyCost;


namespace KitTraden.AnAbundanceOfMana.State.Entities
{
    [Serializable]
    public class EnergyCost
    {
        public enum CostType
        {
            CONSTANT,
            X
        }

        public ModelCost Model;
        public int ConstantCost;
        public CostType Type;

        public EnergyCost(ModelCost model)
        {
            Model = model;
            ConstantCost = model.ConstantCost;

            switch (model.Type)
            {
                case ModelCost.CostType.CONSTANT:
                    Type = CostType.CONSTANT;
                    break;
                case ModelCost.CostType.X:
                    Type = CostType.X;
                    break;
                default:
                    throw new Exception($"Unknown CostType of {Type}");
            }
        }

        public EnergyCost(ModelCost model, int constantCost, CostType type)
        {
            Model = model;
            ConstantCost = constantCost;
            Type = type;
        }

        public int CostToApply(int currentEnergy)
        {
            switch (Type)
            {
                case CostType.CONSTANT:
                    return ConstantCost;
                case CostType.X:
                    return currentEnergy;
                default:
                    throw new Exception($"Unknown CostType of {Type}");
            }
        }
    }
}