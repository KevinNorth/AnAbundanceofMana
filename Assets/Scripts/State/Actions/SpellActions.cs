namespace KitTraden.AnAbundanceOfMana.State.Actions
{
    public class AdvancePhaseAction : Action { }

    public class JumpToPhaseAction : Action
    {
        public int PhaseToJumpTo;
    }
}