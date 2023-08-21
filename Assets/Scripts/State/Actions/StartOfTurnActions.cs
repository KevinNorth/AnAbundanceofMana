using KitTraden.AnAbundanceOfMana.State.State;

namespace KitTraden.AnAbundanceOfMana.State.Actions
{
    public class StartNewTurnAction : Action
    {
        public CardsState CardsStateAtStartOfTurn;
        public PlayerState PlayerStateAtStartOfTurn;
        public SpellState SpellStateAtStartOfTurn;
    }
}