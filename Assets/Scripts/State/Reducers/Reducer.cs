using KitTraden.AnAbundanceOfMana.State.Actions;

namespace KitTraden.AnAbundanceOfMana.State.Reducers
{
    public abstract class Reducer<TState>
    {
        public Reducer() { }
        public abstract TState Reduce<TAction>(TAction action, TState state) where TAction : Action;
    }
}