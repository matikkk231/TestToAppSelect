using Core.ECS.Component;

namespace DefaultNamespace.Area.Logic.Component
{
    public class DeckCardComponent : IComponent
    {
        // where 0 means card on the top of deck
        public int PositionInDeck;
    }
}