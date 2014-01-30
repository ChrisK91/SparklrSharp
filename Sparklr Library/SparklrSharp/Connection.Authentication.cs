using SparklrSharp.Communications;
using SparklrSharp.Exceptions;
using SparklrSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    /// <summary>
    /// Handles the communication with the sparklr server.
    /// </summary>
    public partial class Connection
    {
        /// <summary>
        /// Authenticates the user with the sparklr server
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if successful, otherwise false</returns>
        public async Task<bool> SigninAsync(string username, string password)
        {
            try
            {
                SparklrResponse<string> response = await webClient.GetRawResponseAsync("signin", username, password);

                return response.IsOkAndTrue();
            }
            catch (NotAuthorizedException)
            {
                return false;
            }
        }

        /// <summary>
        /// Terminates the sparklr session
        /// </summary>
        /// <returns>Always true</returns>
        public async Task<bool> SignoffAsync()
        {
            //will always return true or 403. 403 will throw an exception
            return (await webClient.GetRawResponseAsync("signoff")).IsOkAndTrue();
        }
    }
}
