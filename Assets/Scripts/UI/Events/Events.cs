namespace KitTraden.AnAbundanceOfMana.UI.Events
{
    public class EndTurnEvent : UIEvent { }

    public class PlayCardEvent : UIEvent
    {
        public int CardIndex;

        public PlayCardEvent(int cardIndex)
        {
            CardIndex = cardIndex;
        }
    }

    public class StartSpellEvent : UIEvent { }
}