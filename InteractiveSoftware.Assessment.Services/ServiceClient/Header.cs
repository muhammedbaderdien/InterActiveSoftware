using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    public sealed class Header
    {
	   #region Properties

	   public string Name { get; }

	   public string Value { get; }

	   #endregion // Properties


	   #region Constructor

	   public Header(string name, string value)
	   {
		  if (name == null)
		  {
			 throw new ArgumentNullException(nameof(name));
		  }

		  if (string.IsNullOrWhiteSpace(name))
		  {
			 throw new ArgumentOutOfRangeException(nameof(name));
		  }

		  if (value == null)
		  {
			 throw new ArgumentNullException(nameof(value));
		  }

		  if (string.IsNullOrWhiteSpace(value))
		  {
			 throw new ArgumentOutOfRangeException(nameof(value));
		  }

		  Name = name;
		  Value = value;
	   }

	   #endregion // Constructor
    }
}
