using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace KitTraden.AnAbundanceOfMana.MVC.Models.Cards
{
    public class EnergyCost
    {
        public enum CostType
        {
            CONSTANT,
            X
        }

        public int ConstantCost;
        public CostType Type;
    }
}