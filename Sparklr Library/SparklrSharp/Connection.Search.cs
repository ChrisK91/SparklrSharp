using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparklrSharp;

namespace SparklrSharp
{
  public partial class Connection
    {
        public string SearchOutput;
        public async Task<string>Search(string Search)
        {
            
            var response = await webClient.GetRawResponseAsync("search/"+Search);
            SearchOutput = response.Response.ToString();
            return "";
        }
    }
}
