using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    public sealed class ClientAuthOptions
    {
	   public BasicCredentials BasicAuthCredentials { get; set; }

	   public SharedAccessSignature SharedAccessSignature { get; set; }

	   public Header[] Headers { get; set; }
    }
}
