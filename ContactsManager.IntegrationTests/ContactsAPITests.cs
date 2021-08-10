using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContactsManager.IntegrationTests
{
    public class ContactsAPITests
    {
        [Fact]
        public async Task GetAllContactsTestAsycn_Returns_UnAuthorizedResponse()
        {
            using var client = new TestClientProvider().Client;
            var request = new HttpRequestMessage(new HttpMethod("Get"), "api/v1.0/Contacts");

            var response = await client.SendAsync(request);

            //response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
