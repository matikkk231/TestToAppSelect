using System;
using System.Collections.Generic;
using Core.ECS.Entity;
using Core.ECS.System;
using DefaultNamespace.Area.Logic.Component;
using UnityEngine;

namespace DefaultNamespace.Area.Logic.System
{
    public class CardDeckCreatorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _cardFilter;
        private readonly List<Type> _needDeckPositionCards;
        private const int _clanCardsAmount = 13;

        public CardDeckCreatorSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _needDeckPositionCards = new List<Type>();
            _needDeckPositionCards.Add(typeof(NeedDeckPositionComponent));
            _cardFilter = new List<Type>();
            _cardFilter.Add(typeof(CardComponent));
        }

        public void Execute()
        {
            CreateDeck();
            DeclareCardDeckPosition();
        }

        private void DeclareCardDeckPosition()
        {
            var UnhandledCards = _entityManager.GetEntitiesOfGroup(_needDeckPositionCards);
            var maxCardIndex = UnhandledCards.Count - 1;
            List<int> freeDeckPosition = new List<int>();

            for (int i = 0; i < UnhandledCards.Count; i++)
            {
                freeDeckPosition.Add(i);
            }


            for (int i = 0; i <= maxCardIndex; i++)
            {
                var cardIndex = UnityEngine.Random.Range(0, maxCardIndex - i);
                var card = UnhandledCards[cardIndex];
                card.RemoveComponent(typeof(NeedDeckPositionComponent));
                UnhandledCards = _entityManager.GetEntitiesOfGroup(_needDeckPositionCards);
                card.AddComponent(new DeckCardComponent());
                var deckPositionComponent = (DeckCardComponent) card.GetComponent(typeof(DeckCardComponent));
                var cardDeckPosition = UnityEngine.Random.Range(0, freeDeckPosition.Count);
                freeDeckPosition.Remove(cardDeckPosition);
                deckPositionComponent.PositionInDeck = cardDeckPosition;
            }
        }

        private void CreateDeck()
        {
            for (int clan = 1; clan <= 4; clan++)
            {
                for (int priority = 0; priority < _clanCardsAmount; priority++)
                {
                    var card = _entityManager.CreateEntity();
                    card.AddComponent(new CardComponent());
                    card.AddComponent(new NeedDeckPositionComponent());
                    var cardComponent = (CardComponent) card.GetComponent(typeof(CardComponent));
                    switch (clan)
                    {
                        case 1:
                            cardComponent.CardClan = CardClanType.Club;
                            break;
                        case 2:
                            cardComponent.CardClan = CardClanType.Diamond;
                            break;
                        case 3:
                            cardComponent.CardClan = CardClanType.Heart;
                            break;
                        case 4:
                            cardComponent.CardClan = CardClanType.Spade;
                            break;
                    }

                    cardComponent.CardPriority = priority;
                }
            }
        }
    }
}