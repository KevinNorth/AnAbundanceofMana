using KitTraden.AnAbundanceOfMana.MVC.Models.Cards;

namespace KitTraden.AnAbundanceOfMana.Effects.CardEffects
{
    public class AddEnergyEffect : CardEffect
    {
        public int EnergyToGain = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return EnergyToGain == (other as AddEnergyEffect).EnergyToGain;
        }
    }

    public class AddEnergyPerTurnEffect : CardEffect
    {
        public int EnergyPerTurnToGain = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return EnergyPerTurnToGain == (other as AddEnergyPerTurnEffect).EnergyPerTurnToGain;
        }
    }

    public class AddInstanceOfCardToBottomOfDeckEffect : CardEffect
    {
        public Card Card;
        public bool AddPermanently = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as AddInstanceOfCardToBottomOfDeckEffect;

            return Card.Equals(typedOther.Card) && AddPermanently == typedOther.AddPermanently;
        }
    }

    public class AddInstanceOfCardToDiscardPileEffect : CardEffect
    {
        public Card Card;
        public bool AddPermanently = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as AddInstanceOfCardToDiscardPileEffect;

            return Card.Equals(typedOther.Card) && AddPermanently == typedOther.AddPermanently;
        }
    }

    public class AddInstanceOfCardToEndOfComboEffect : CardEffect
    {
        public Card Card;
        public bool AddPermanently = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as AddInstanceOfCardToEndOfComboEffect;

            return Card.Equals(typedOther.Card) && AddPermanently == typedOther.AddPermanently;
        }
    }

    public class AddInstanceOfCardToHandEffect : CardEffect
    {
        public Card Card;
        public bool AddPermanently = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as AddInstanceOfCardToHandEffect;

            return Card.Equals(typedOther.Card) && AddPermanently == typedOther.AddPermanently;
        }
    }

    public class AddInstanceOfCardToTopOfDeckEffect : CardEffect
    {
        public Card Card;
        public bool AddPermanently = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as AddInstanceOfCardToTopOfDeckEffect;

            return Card.Equals(typedOther.Card) && AddPermanently == typedOther.AddPermanently;
        }
    }

    public class AddMulligansEffect : CardEffect
    {
        public int MulligansToAdd = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return MulligansToAdd == (other as AddMulligansEffect).MulligansToAdd;
        }
    }

    public class ChangeBaseManaEffect : CardEffect
    {
        public int DeltaBaseMana = 0;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return DeltaBaseMana == (other as ChangeBaseManaEffect).DeltaBaseMana;
        }
    }

    public class ChangeMaximumHandSizeEffect : CardEffect
    {
        public int DeltaMaximumHandSize = 0;
        public int MinimumMaxHandSize = 3;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as ChangeMaximumHandSizeEffect;

            return DeltaMaximumHandSize == typedOther.DeltaMaximumHandSize && MinimumMaxHandSize == typedOther.MinimumMaxHandSize;
        }
    }

    public class ChangeMaximumMulligansEffect : CardEffect
    {
        public int DeltaMaximumMulligans = 0;
        public int MinimumMaxMulligans = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as ChangeMaximumMulligansEffect;

            return DeltaMaximumMulligans == typedOther.DeltaMaximumMulligans && MinimumMaxMulligans == typedOther.MinimumMaxMulligans;
        }
    }

    public class ChangeMultiplierLinearlyEffect : CardEffect
    {
        public decimal DeltaMultiplier = 0;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return DeltaMultiplier == (other as ChangeMultiplierLinearlyEffect).DeltaMultiplier;
        }
    }

    public class ChangeMultiplierMultiplicitavelyEffect : CardEffect
    {
        public decimal MultiplierFactor = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return MultiplierFactor == (other as ChangeMultiplierMultiplicitavelyEffect).MultiplierFactor;
        }
    }

    public class ChangeStartOfTurnBaseManaEffect : CardEffect
    {
        public int DeltaStartOfTurnBaseMana = 0;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return DeltaStartOfTurnBaseMana == (other as ChangeStartOfTurnBaseManaEffect).DeltaStartOfTurnBaseMana;
        }
    }

    public class ChangeStartOfTurnBaseMultiplierEffect : CardEffect
    {
        public decimal DeltaStartOfTurnMultiplier;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return DeltaStartOfTurnMultiplier == (other as ChangeStartOfTurnBaseMultiplierEffect).DeltaStartOfTurnMultiplier;
        }
    }

    public class ChangeStartOfTurnHandSizeEffect : CardEffect
    {
        public int DeltaStartOfTurnHandSize = 0;
        public int MinimumMaxHandSize = 3;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as ChangeStartOfTurnHandSizeEffect;

            return DeltaStartOfTurnHandSize == typedOther.DeltaStartOfTurnHandSize && MinimumMaxHandSize == typedOther.MinimumMaxHandSize;
        }
    }

    public class DiscardCardEffect : CardEffect
    {
        public int IndexToDiscard = 1;
        public bool CountFromEndOfHand = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as DiscardCardEffect;

            return IndexToDiscard == typedOther.IndexToDiscard && CountFromEndOfHand == typedOther.CountFromEndOfHand;
        }
    }

    public class DiscardComboEffect : CardEffect { }

    public class DiscardFromComboEffect : CardEffect
    {
        public int CardIndex;
        public bool CountFromEndOfCombo = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as DiscardFromComboEffect;

            return CardIndex == typedOther.CardIndex && CountFromEndOfCombo == typedOther.CountFromEndOfCombo;
        }
    }

    public class DiscardHandAndRedrawEffect : CardEffect
    {
        public int NumCardsToDraw = 5;
        public bool UseStartOfTurnHandSize = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as DiscardHandAndRedrawEffect;

            return NumCardsToDraw == typedOther.NumCardsToDraw && UseStartOfTurnHandSize == typedOther.UseStartOfTurnHandSize;
        }
    }

    public class DrawCardsEffect : CardEffect
    {
        public int NumCardsToDraw = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return NumCardsToDraw == (other as DrawCardsEffect).NumCardsToDraw;
        }
    }

    public class ExhaustEffect : CardEffect { }

    public class LoseMulligansEffect : CardEffect
    {
        public int MulligansToLose = 1;
        public bool CanPlayWithoutEnoughMulligans = true;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            var typedOther = other as LoseMulligansEffect;

            return MulligansToLose == typedOther.MulligansToLose && CanPlayWithoutEnoughMulligans == typedOther.CanPlayWithoutEnoughMulligans;
        }
    }

    public class ReshuffleDiscardPileEffect : CardEffect
    {
        public bool AndShuffleDeckAfterwards = false;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return AndShuffleDeckAfterwards == (other as ReshuffleDiscardPileEffect).AndShuffleDeckAfterwards;
        }
    }

    public class SetMaximumMulligansEffect : CardEffect
    {
        public int MaximumMulligans = 3;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return MaximumMulligans == (other as SetMaximumMulligansEffect).MaximumMulligans;
        }
    }

    public class SetStartofTurnBaseManaEffect : CardEffect
    {
        public int StartOfTurnBaseMana = 0;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return StartOfTurnBaseMana == (other as SetStartofTurnBaseManaEffect).StartOfTurnBaseMana;
        }
    }

    public class SetStartOfTurnHandSizeEffect : CardEffect
    {
        public int StartOfTurnHandSize;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return StartOfTurnHandSize == (other as SetStartOfTurnHandSizeEffect).StartOfTurnHandSize;
        }
    }

    public class SetStartofTurnMultiplierEffect : CardEffect
    {
        public decimal StartOfTurnMultiplier = 1;

        public override bool Equals(CardEffect other)
        {
            if (!base.Equals(other))
            {
                return false;
            }

            return StartOfTurnMultiplier == (other as SetStartofTurnMultiplierEffect).StartOfTurnMultiplier;
        }
    }

    public class ShuffleDeckEffect : CardEffect { }
}