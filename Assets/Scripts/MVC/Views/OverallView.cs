using System.Collections.Generic;
using KitTraden.AnAbundanceOfMana.State;

namespace KitTraden.AnAbundanceOfMana.MVC.Views
{
    public class OverallView
    {
        public CardsView cardsView;
        public PlayerView playerView;
        public SpellView spellView;
        public ScoreView scoreView;

        public OverallView(CardsView cardsView, PlayerView playerView, SpellView spellView, ScoreView scoreView)
        {
            this.cardsView = cardsView;
            this.playerView = playerView;
            this.spellView = spellView;
            this.scoreView = scoreView;
        }
    }
}