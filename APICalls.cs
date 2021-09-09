using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardDeckApp
{
    class APICalls
    {
        private static RestClient client = new RestClient();
        public static IRestResponse CallApi(Method restMethod, string resource)
        {
            string baseUrl = "http://deckofcardsapi.com/api/deck/";

            RestRequest request = new RestRequest();
            request.Method = restMethod;
            request.Resource = baseUrl + resource;

            return client.Execute(request);

        }
    }
}
