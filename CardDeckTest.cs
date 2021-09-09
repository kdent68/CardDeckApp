using CardDeckApp.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnitLite;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CardDeckApp
{
    [TestFixture]
    class CardDeckTest
    {
        public static int Main(string[] args)
        {
            return new AutoRun().Execute(args);
        }


        public string baseUrl = "http://deckofcardsapi.com/api/deck/";

        private NewDeckResult CreateNewDeck(Method restMethod, string resource)
        {
            NewDeckResult newDeck = new NewDeckResult();
            var response = APICalls.CallApi(restMethod, resource);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                newDeck = JsonConvert.DeserializeObject<NewDeckResult>(response.Content);
            }

            return newDeck;
        }

        private DrawnCardResult DrawCards(Method restMethod, string resource, int numberOfCards)
        {
            DrawnCardResult drawnCard = new DrawnCardResult();
            var response = APICalls.CallApi(restMethod, resource + numberOfCards);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                drawnCard = JsonConvert.DeserializeObject<DrawnCardResult>(response.Content);
            }

            return drawnCard;
        }

        [Test]
        [TestCase(Method.GET, "new/", TestName = "Test Response for regular new deck")]
        [TestCase(Method.POST, "new/?jokers_enabled=true", TestName = "Test Response for joker new deck")]
        public void TestApiResponse(Method restMethod, string resource)
        {
            var response = APICalls.CallApi(restMethod, resource);
            Assert.AreEqual(baseUrl + resource, response.Request.Resource);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void TestRegularDeckProperties()
        {
            var deck = CreateNewDeck(Method.GET, "new/");

            Assert.AreEqual(52, deck.Remaining);
            Assert.IsNotNull(deck.Deck_id);

        }

        [Test]
        public void TestJokerDeckProperties()
        {
            var deck = CreateNewDeck(Method.POST, "new/?jokers_enabled=true");

            Assert.AreEqual(54, deck.Remaining);
            Assert.IsNotNull(deck.Deck_id);

        }

        [Test]
        [TestCase(2, TestName = "Draw 2 cards")]
        [TestCase(15, TestName = "Draw 15 cards")]
        public void TestDraw(int numberOfCards)
        {
            NewDeckResult deck = CreateNewDeck(Method.GET, "new/");
            DrawnCardResult cardResult = DrawCards(Method.GET, deck.Deck_id + "/draw/?count=", numberOfCards);

            Assert.AreEqual((deck.Remaining - numberOfCards), cardResult.Remaining);
            Assert.AreEqual(deck.Deck_id, cardResult.Deck_id);
            Assert.That(cardResult.Cards[0].Image.Contains(cardResult.Cards[0].Code));

        }
    }
}
