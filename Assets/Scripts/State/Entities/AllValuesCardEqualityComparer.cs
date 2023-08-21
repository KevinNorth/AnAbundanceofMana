using System.Collections.Generic;

namespace KitTraden.AnAbundanceOfMana.State.Entities
{
    public class AllValuesCardEqualityComparer : IEqualityComparer<Card>
    {
        public static AllValuesCardEqualityComparer INSTANCE = new AllValuesCardEqualityComparer();

        public bool Equals(Card x, Card y)
        {
            if ((x == null) && (y == null))
            {
                return true;
            }

            if ((x == null) || (y == null))
            {
                return false;
            }

            if ((x.Model == null) != (y.Model == null))
            {
                return false;
            }

            if ((x.Model != null) && !x.Model.Equals(y.Model))
            {
                return false;
            }

            if (x.Name != y.Name)
            {
                return false;
            }

            if (x.Text != y.Text)
            {
                return false;
            }

            if ((x.Sprite == null) != (y.Sprite == null))
            {
                return false;
            }

            if ((x.Sprite != null) && !x.Sprite.Equals(y.Sprite))
            {
                return false;
            }

            if (x.Type != y.Type)
            {
                return false;
            }

            if (x.Cost.Type != y.Cost.Type)
            {
                return false;
            }

            if (x.Cost.Type == EnergyCost.CostType.CONSTANT && (x.Cost.ConstantCost != y.Cost.ConstantCost))
            {
                return false;
            }

            if (x.BaseMana != y.BaseMana)
            {
                return false;
            }

            if (x.ToAddToMultiplier != y.ToAddToMultiplier)
            {
                return false;
            }

            if (x.ToMultiplyByMultiplier != y.ToMultiplyByMultiplier)
            {
                return false;
            }

            if (x.UUID != y.UUID)
            {
                return false;
            }

            return true;
        }

        public int GetHashCode(Card card)
        {
            return (11 * card.Model.Name?.GetHashCode() ?? 0)
                + (13 * card.Name?.GetHashCode() ?? 0)
                + (17 * card.Text?.GetHashCode() ?? 0)
                + (19 * card.Sprite?.GetHashCode() ?? 0)
                + (23 * (int)card.Type)
                + (29 * ((int?)card.Cost?.Type) ?? 0)
                + (31 * card.Cost?.CostToApply(0) ?? 0) // So that ConstantCost is ignored when Type is X
                + (37 * card.BaseMana)
                + (41 * card.ToAddToMultiplier.GetHashCode())
                + (43 * card.ToMultiplyByMultiplier.GetHashCode())
                + (47 * card.UUID.GetHashCode());
        }
    }
}