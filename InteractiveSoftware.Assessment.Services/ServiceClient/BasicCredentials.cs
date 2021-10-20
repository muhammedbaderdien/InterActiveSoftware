using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    public sealed class BasicCredentials
    {
	   #region Properties

	   public string UserName { get; }

	   public string Password { get; }

	   #endregion // Properties

	   #region Constructor

	   public BasicCredentials(string userName, string password)
	   {
		  if (userName == null)
		  {
			 throw new ArgumentNullException(nameof(userName));
		  }

		  if (string.IsNullOrWhiteSpace(userName))
		  {
			 throw new ArgumentOutOfRangeException(nameof(userName));
		  }

		  if (password == null)
		  {
			 throw new ArgumentNullException(nameof(password));
		  }

		  if (string.IsNullOrWhiteSpace(password))
		  {
			 throw new ArgumentOutOfRangeException(nameof(password));
		  }

		  UserName = userName;
		  Password = password;
	   }

	   #endregion // Constructor
    }
}
