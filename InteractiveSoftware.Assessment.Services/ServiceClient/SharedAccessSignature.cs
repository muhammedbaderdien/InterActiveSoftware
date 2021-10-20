using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    public sealed class SharedAccessSignature
    {
	   #region Instance Fields

	   private readonly string _id;
	   private readonly string _key;
	   private readonly DateTime _expiry;

	   #endregion Instance Fields

	   #region Properties

	   public string SharedAccessToken
	   {
		  get
		  {
			 return GetSharedAccessToken();
		  }
	   }

	   #endregion Properties

	   #region Constructor

	   public SharedAccessSignature(string id, string key, DateTime expiry)
	   {
		  if (id == null)
		  {
			 throw new ArgumentNullException(nameof(id));
		  }

		  if (string.IsNullOrWhiteSpace(id))
		  {
			 throw new ArgumentOutOfRangeException(nameof(id));
		  }

		  if (key == null)
		  {
			 throw new ArgumentNullException(nameof(key));
		  }

		  if (string.IsNullOrWhiteSpace(key))
		  {
			 throw new ArgumentOutOfRangeException(nameof(key));
		  }

		  if (expiry == default)
		  {
			 throw new ArgumentOutOfRangeException(nameof(expiry));
		  }

		  _id = id;
		  _key = key;
		  _expiry = expiry;
	   }

	   #endregion Constructor

	   #region Helpers

	   private string GetSharedAccessToken()
	   {
		  using (var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(_key)))
		  {
			 var dataToSign = _id + "\n" + _expiry.ToString("O", CultureInfo.InvariantCulture);
			 var x = $"{_id}\n{_expiry.ToString("O", CultureInfo.InvariantCulture)}";
			 var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
			 var signature = Convert.ToBase64String(hash);
			 return $"uid={_id}&ex={_expiry:o}&sn={signature}";
		  }
	   }

	   #endregion Helpers
    }
}
