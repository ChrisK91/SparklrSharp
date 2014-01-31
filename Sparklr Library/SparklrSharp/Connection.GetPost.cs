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
        public string GetPostIDOutput;
        public string GetCommentsOutput;
        public async Task<string> GetPost(int PostID)
        {

            var response = await webClient.GetRawResponseAsync("post/"+PostID);
            GetPostIDOutput = response.Response.ToString();
            return "";
        }

        public async Task<string>GetComments(int PostID,int Time)
        {
            
            var response = await webClient.GetRawResponseAsync("comments/" + PostID+"[?since="+Time+"]");
            GetCommentsOutput = response.Response.ToString();
            return "";
        }

    }
}
