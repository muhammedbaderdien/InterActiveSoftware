using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    public sealed class RestServiceClient : IDisposable
    {
	   #region Instance Fields

	   private readonly string _baseUrl;
	   private readonly AuthenticationHeaderValue _authHeader;
	   private readonly Header[] _clientHeaders;
	   private readonly WebProxy _webProxy;

	   private HttpClient _client;
	   private readonly object _clientSync = new object();

	   private bool _disposed;

	   #endregion // Instance Fields


	   #region Properties

	   private HttpClient Client
	   {
		  get
		  {
			 if (_client == null)
			 {
				lock (_clientSync)
				{
				    if (_client == null)
				    {
					   if (_webProxy != null)
					   {
						  var clientHandler = new HttpClientHandler()
						  {
							 Proxy = _webProxy,
						  };
						  _client = new HttpClient(clientHandler);
					   }
					   else
					   {
						  _client = new HttpClient();
					   }
				    }
				}
			 }

			 return _client;
		  }
	   }

	   #endregion // Properties


	   #region Constructor

	   public RestServiceClient(string baseUrl, ClientAuthOptions authOptions = null, WebProxy webProxy = null)
	   {
		  if (baseUrl == null)
		  {
			 throw new ArgumentNullException(nameof(baseUrl));
		  }

		  if (string.IsNullOrWhiteSpace(baseUrl))
		  {
			 throw new ArgumentOutOfRangeException(nameof(baseUrl));
		  }

		  _baseUrl = baseUrl;

		  if (authOptions != null)
		  {
			 if (authOptions.BasicAuthCredentials != null)
			 {
				_authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{authOptions.BasicAuthCredentials.UserName}:{authOptions.BasicAuthCredentials.Password}")));
			 }

			 if (authOptions.SharedAccessSignature != null)
			 {
				_authHeader = new AuthenticationHeaderValue(nameof(SharedAccessSignature), authOptions.SharedAccessSignature.SharedAccessToken);
			 }

			 _clientHeaders = authOptions.Headers;
			 _webProxy = webProxy;
		  }

#if DEBUG
		  // Disable certificate validation for test environments
		  ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
#endif
	   }

	   #endregion // Constructor

	   #region Public Methods

	   public async Task<string> SendRequestAsync(string requestPath, Header[] messageHeaders = null, QueryStringParam[] parameters = null, Payload payload = null)
	   {
		  if (_disposed)
		  {
			 throw new ObjectDisposedException(typeof(RestServiceClient).FullName);
		  }

		  if (requestPath == null)
		  {
			 throw new ArgumentNullException(nameof(requestPath));
		  }

		  if (string.IsNullOrWhiteSpace(requestPath))
		  {
			 throw new ArgumentOutOfRangeException(nameof(requestPath));
		  }

		  using (var request = GetRequestMessage(HttpMethod.Get, GetRequestUrl(requestPath, parameters), messageHeaders))
		  {
			 if (payload != null)
			 {
				request.Content = payload.GetContent();
			 }
			 return await GetResponseContent(request).ConfigureAwait(false);
		  }
	   }
	   public async Task<byte[]> SendRequestBytesAsync(string requestPath, Header[] messageHeaders = null, QueryStringParam[] parameters = null)
	   {
		  if (_disposed)
		  {
			 throw new ObjectDisposedException(typeof(RestServiceClient).FullName);
		  }

		  if (requestPath == null)
		  {
			 throw new ArgumentNullException(nameof(requestPath));
		  }

		  if (string.IsNullOrWhiteSpace(requestPath))
		  {
			 throw new ArgumentOutOfRangeException(nameof(requestPath));
		  }

		  using (var request = GetRequestMessage(HttpMethod.Get, GetRequestUrl(requestPath, parameters), messageHeaders))
		  {
			 return await GetResponseContentBytes(request).ConfigureAwait(false);
		  }
	   }
	   public async Task<string> SendRequestAsync(string requestPath, RequestMethod method, Header[] messageHeaders = null, QueryStringParam[] parameters = null)
	   {
		  if (_disposed)
		  {
			 throw new ObjectDisposedException(typeof(RestServiceClient).FullName);
		  }

		  if (requestPath == null)
		  {
			 throw new ArgumentNullException(nameof(requestPath));
		  }

		  if (string.IsNullOrWhiteSpace(requestPath))
		  {
			 throw new ArgumentOutOfRangeException(nameof(requestPath));
		  }

		  using (var request = GetRequestMessage(GetHttpMethod(method), GetRequestUrl(requestPath, parameters), messageHeaders))
		  {
			 return await GetResponseContent(request).ConfigureAwait(false);
		  }
	   }

	   public async Task<string> SendRequestAsync(string requestPath, RequestMethod method, Payload payload, Header[] messageHeaders = null, QueryStringParam[] parameters = null)
	   {
		  if (_disposed)
		  {
			 throw new ObjectDisposedException(typeof(RestServiceClient).FullName);
		  }

		  if (requestPath == null)
		  {
			 throw new ArgumentNullException(nameof(requestPath));
		  }

		  if (string.IsNullOrWhiteSpace(requestPath))
		  {
			 throw new ArgumentOutOfRangeException(nameof(requestPath));
		  }

		  if (payload == null)
		  {
			 throw new ArgumentNullException(nameof(payload));
		  }

		  using (var request = GetRequestMessage(GetHttpMethod(method), GetRequestUrl(requestPath, parameters), messageHeaders))
		  {
			 request.Content = payload.GetContent();
			 return await GetResponseContent(request).ConfigureAwait(false);
		  }
	   }

	   #endregion // Public Methods

	   #region Helpers

	   private string GetRequestUrl(string requestPath, QueryStringParam[] parameters)
	   {
		  string url = $"{_baseUrl}{requestPath}";

		  if (parameters != null && parameters.Length > 0)
		  {
			 url += $"?{string.Join("&", parameters.Select(p => $"{WebUtility.UrlEncode(p.Name)}={WebUtility.UrlEncode(p.Value)}"))}";
		  }

		  return url;
	   }

	   private HttpMethod GetHttpMethod(RequestMethod method)
	   {
		  switch (method)
		  {
			 case RequestMethod.POST:
				return HttpMethod.Post;

			 case RequestMethod.PUT:
				return HttpMethod.Put;

			 default:
				throw new ArgumentOutOfRangeException(nameof(method));
		  }
	   }

	   private HttpRequestMessage GetRequestMessage(HttpMethod method, string url, Header[] messageHeaders)
	   {
		  HttpRequestMessage request = null;
		  HttpRequestMessage tempRequest = null;

		  try
		  {
			 tempRequest = new HttpRequestMessage(method, url);

			 // Set auth header
			 if (_authHeader != null)
			 {
				tempRequest.Headers.Authorization = _authHeader;
			 }

			 // Add client headers (if any)
			 if (_clientHeaders != null)
			 {
				foreach (var header in _clientHeaders)
				{
				    tempRequest.Headers.Add(header.Name, header.Value);
				}
			 }

			 // Add message headers (if any)
			 if (messageHeaders != null)
			 {
				foreach (var header in messageHeaders)
				{
				    tempRequest.Headers.Add(header.Name, header.Value);
				}
			 }

			 request = tempRequest;
			 tempRequest = null;
		  }
		  finally
		  {
			 tempRequest?.Dispose();
		  }

		  return request;
	   }

	   private async Task<string> GetResponseContent(HttpRequestMessage request)
	   {
		  // Asynchronously send request and await response (allow continuation outside captured context to avoid locking)
		  using (var response = await Client.SendAsync(request).ConfigureAwait(false))
		  {
			 // Asynchronously read response and await completion (allow continuation outside captured context to avoid locking)
			 var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			 // Handle error response
			 if (!response.IsSuccessStatusCode)
			 {
				throw new HttpClientException($"Request failed. with Response {responseContent}", request.RequestUri.ToString(), request.Method, responseContent, response.StatusCode, response.ReasonPhrase);
			 }

			 return responseContent;
		  }
	   }

	   private async Task<byte[]> GetResponseContentBytes(HttpRequestMessage request)
	   {
		  // Asynchronously send request and await response (allow continuation outside captured context to avoid locking)
		  using (var response = await Client.SendAsync(request).ConfigureAwait(false))
		  {
			 // Asynchronously read response and await completion (allow continuation outside captured context to avoid locking)
			 var responseContent = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

			 // Handle error response
			 if (!response.IsSuccessStatusCode)
			 {
				throw new HttpClientException($"Request failed. with Response {responseContent}", request.RequestUri.ToString(), request.Method, responseContent.ToString(), response.StatusCode, response.ReasonPhrase);
			 }

			 return responseContent;
		  }
	   }

	   #endregion // Helpers


	   #region IDisposable Support

	   private void Dispose(bool disposing)
	   {
		  if (!_disposed)
		  {
			 if (disposing)
			 {
				_client?.Dispose();
			 }

			 _disposed = true;
		  }
	   }

	   public void Dispose()
	   {
		  Dispose(true);
	   }

	   #endregion // IDisposable Support


	   #region Nested Enums (tightly coupled to parent - no stand-alone significance)

	   public enum RequestMethod
	   {
		  PUT,
		  POST
	   }

	   #endregion // Nested Enums


	   #region Nested Classes (tightly coupled to parent - no stand-alone significance)

	   public sealed class QueryStringParam
	   {
		  public string Name { get; private set; }
		  public string Value { get; private set; }

		  public QueryStringParam(string name, string value)
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
	   }

	   public abstract class Payload
	   {
		  public abstract HttpContent GetContent();
	   }

	   public sealed class StringPayload : Payload
	   {
		  private readonly string _content;
		  private readonly string _contentType;
		  public readonly Encoding _contentEncoding;

		  public StringPayload(string content, string contentType, Encoding contentEncoding)
		  {
			 if (content == null)
			 {
				throw new ArgumentNullException(nameof(content));
			 }

			 if (string.IsNullOrWhiteSpace(content))
			 {
				throw new ArgumentOutOfRangeException(nameof(content));
			 }

			 if (contentType == null)
			 {
				throw new ArgumentNullException(nameof(contentType));
			 }

			 if (string.IsNullOrWhiteSpace(contentType))
			 {
				throw new ArgumentOutOfRangeException(nameof(contentType));
			 }

			 if (contentEncoding == null)
			 {
				throw new ArgumentNullException(nameof(contentEncoding));
			 }

			 _content = content;
			 _contentType = contentType;
			 _contentEncoding = contentEncoding;
		  }

		  public override HttpContent GetContent()
		  {
			 return new StringContent(_content, _contentEncoding, _contentType);
		  }
	   }

	   public sealed class FormPayload : Payload
	   {
		  private readonly IEnumerable<KeyValuePair<string, string>> _formData;

		  public FormPayload(IEnumerable<KeyValuePair<string, string>> formData)
		  {
			 if (formData == null)
			 {
				throw new ArgumentNullException(nameof(formData));
			 }

			 _formData = formData;
		  }

		  public override HttpContent GetContent()
		  {
			 return new FormUrlEncodedContent(_formData);
		  }
	   }

	   #endregion // Nested Classes
    }
}
