﻿using SparklrSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Communications
{
    /// <summary>
    /// Provides some extended functionality to do API calls. Keeps track on cookies.
    /// </summary>
    internal class WebClient
    {
        private CookieContainer cookies = new CookieContainer();

        /// <summary>
        /// The location of the api
        /// </summary>
        private const string baseUrl = "https://sparklr.me/api/";

        /// <summary>
        /// The base uri - used to identify cookies
        /// </summary>
        private readonly Uri baseUri = new Uri("https://sparklr.me");

        //TODO: switch to OAuth when ready
        private string X = null;

        /// <summary>
        /// Performs a GET-Request on the given location.
        /// </summary>
        /// <param name="uri">The location of the given ressource. Will be appended to the baseUrl, i.e. sparklr.me/api/</param>
        /// <param name="parameters">A set of parameters to append. Will be urlencoded and joined to the Url.</param>
        /// <returns>The result of the request</returns>
        internal async Task<SparklrResponse<string>> GetRawResponseAsync(string uri, params string[] parameters)
        {
            string url = baseUrl + uri;

            //TODO: Urlencode
            if (parameters.Length > 0)
                url = url + "/" + String.Join("/", parameters);

            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.CookieContainer = cookies;

            if (X != null)
                request.Headers["X-X"] = X;

            try
            {
                return CreateResponse((HttpWebResponse)(await request.GetResponseAsync()));
            }
            catch (WebException ex)
            {
                 return CreateResponse((HttpWebResponse)(ex.Response));
            }
        }

        private SparklrResponse<string> CreateResponse(HttpWebResponse response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new Exceptions.NotAuthorizedException();

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                HttpStatusCode statusCode = response.StatusCode;

                if (response.Headers.AllKeys.Contains("Set-Cookie"))
                {
                    //TODO: this is really ugly...
                    string s = response.Headers["Set-Cookie"];

                    string[] parts = s.Split(';');
                    string[] data = parts[0].Split('=');

                    for (int i = 0; i < data.Length; i = i + 2)
                    {
                        string name = data[i];
                        string value = data[i + 1];

                        cookies.Add(baseUri, new Cookie(name, Uri.EscapeDataString(value)));

                        if (name == "D" && !String.IsNullOrEmpty(value))
                        {
                            X = value.Split(',')[1];
                        }
                    }
                }

                if (responseString.Length == 3 && isDigitsOnly(responseString))
                {
                    statusCode = (HttpStatusCode)Convert.ToInt32(responseString);

                    if (statusCode == HttpStatusCode.Forbidden)
                        throw new NotAuthorizedException();
                }

                return new SparklrResponse<string>(statusCode, responseString);
            }
        }

        private static bool isDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
