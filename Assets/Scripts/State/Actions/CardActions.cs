using KitTraden.AnAbundanceOfMana.State.Entities;

namespace KitTraden.AnAbundanceOfMana.State.Actions
{
    public class AddInstanceOfCardToBottomOfDeckAction : Action
    {
        public Card Card;
        public bool AddPermanently = false;
    }

    public class AddInstanceOfCardToDiscardPileAction : Action
    {
        public Card Card;
        public bool AddPermanently = false;
    }

    public class AddInstanceOfCardToEndOfComboAction : Action
    {
        public Card Card;
        public bool AddPermanently = false;
    }

    public class AddInstanceOfCardToHandAction : Action
    {
        public Card Card;
        public bool AddPermanently = false;
    }

    public class AddInstanceOfCardToTopOfDeckAction : Action
    {
        public Card Card;
        public bool AddPermanently = false;
    }

    public class DiscardComboAction : Action { }

    public class DiscardHandAction : Action { }

    public class DiscardFromComboAction : Action
    {
        public int comboIndexToDiscardFrom;
    }

    public class DiscardFromHandAction : Action
    {
        public int handIndexToDiscardFrom;
    }

    public class DrawAction : Action
    {
        public int CardsToDraw;

        public DrawAction()
        {
            CardsToDraw = 1;
        }

        public DrawAction(int cardsToDraw)
        {
            CardsToDraw = cardsToDraw;
        }
    }

    public class ExhaustFromComboAction : Action
    {
        public int comboIndexToExhaustFrom;
    }

    public class ExhaustFromDeckAction : Action
    {
        public int deckIndexToExhaustFrom;
    }

    public class ExhaustFromDiscardPileAction : Action
    {
        public int discardIndexToExhaustFrom;
    }

    public class ExhaustFromHandAction : Action
    {
        public int handIndexToExhaustFrom;
    }

    public class PlayCardAndDiscardAction : Action
    {
        public int handIndexToPlayFrom;
    }

    public class PlayCardIntoComboAction : Action
    {
        public int handIndexToPlayFrom;
        public int comboIndexToPlayTo;
    }

    public class PlayPowerCardAction : Action
    {
        public int handIndexToPlayFrom;
    }

    public class ReshuffleDiscardPileAction : Action { }

    public class RetrieveCardFromComboAction : Action
    {
        public int comboIndexToTakeFrom;
    }

    public class RetrieveCardFromDeckAction : Action
    {
        public int deckIndexToTakeFrom;
    }

    public class RetrieveCardFromDiscardPileAction : Action
    {
        public int discardIndexToTakeFrom;
    }

    public class RetrieveCardFromExhaustPileAction : Action
    {
        public int exhaustIndexToTakeFrom;
    }

    public class ShuffleDeckAction : Action { }
}