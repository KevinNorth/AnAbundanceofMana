using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.MVC;
using KitTraden.AnAbundanceOfMana.MVC.Controllers;
using KitTraden.AnAbundanceOfMana.MVC.Models.Cards;
using SpellModel = KitTraden.AnAbundanceOfMana.MVC.Models.Spells.Spell;
using KitTraden.AnAbundanceOfMana.MVC.Views;
using KitTraden.AnAbundanceOfMana.State;
using KitTraden.AnAbundanceOfMana.UI.Events;
using KitTraden.AnAbundanceOfMana.UI.Renderers;

namespace KitTraden.AnAbundanceOfMana.Managers
{
    public class SpellManager : Manager<OverallView>
    {
        public SpellModel Spell;
        public List<Card> PermanentDeck;

        public Controller Controller { private set; get; }
        public SpellRenderer Renderer;
        private Store store;
        private OverallView CachedView = null;

        public void Awake()
        {
            store = new Store(GetStartOfSpellPayload());
            Controller = new Controller(store);
            Controller.HandleUIEvent(new StartSpellEvent());
            CachedView = GetFreshView();
        }

        public override OverallView GetView()
        {
            return CachedView;
        }

        public override void HandleUIEvent(UIEvent uiEvent)
        {
            var feedback = Controller.HandleUIEvent(uiEvent);
            CachedView = GetFreshView();
            Renderer.HandleFeedback(feedback);
        }

        public override void RestoreState(int index)
        {
            Controller.RestoreState(index);
            CachedView = GetFreshView();
        }

        private StartOfSpellPayload GetStartOfSpellPayload()
        {
            var payload = new StartOfSpellPayload
            {
                EnergyPerTurn = 5,
                MaxHandSize = 10,
                StartOfTurnHandSize = 7,
                MaxMulligans = 5,
                MulligansRemaining = 5,
                Spell = Spell,
                PermanentDeck = PermanentDeck,
                StartOfTurnBaseMana = 0,
                StartofTurnMultiplier = 1
            };

            return payload;
        }

        private OverallView GetFreshView()
        {
            return StateToViewConverter.CovertStateToView(store.GetState());
        }
    }
}