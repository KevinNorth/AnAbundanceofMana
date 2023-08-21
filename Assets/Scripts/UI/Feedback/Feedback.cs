namespace KitTraden.AnAbundanceOfMana.UI.Feedback
{
    public struct Feedback
    {
        public static Feedback OK = new()
        {
            HasPlayBeenCancelled = false,
            ToastText = null
        };

        public static Feedback CANNOT_PLAY_UNPLAYABLE_CARDS = new()
        {
            HasPlayBeenCancelled = true,
            ToastText = "This card cannot be played directly."
        };

        public static Feedback NOT_ENOUGH_ENERGY = new()
        {
            HasPlayBeenCancelled = true,
            ToastText = "Out of Energy."
        };

        public bool HasPlayBeenCancelled;
        public string ToastText;
    }
}