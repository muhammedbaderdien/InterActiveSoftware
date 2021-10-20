using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace InteractiveSoftware.Assessment.Services.ServiceClient
{
    /// <summary>
    /// Represents HTTP errors that occur during interaction with a HTTP endpoint.
    /// </summary>
    [Serializable]
    public class HttpClientException : Exception
    {
	   #region Properties

	   /// <summary>
	   /// HTTP request URL.
	   /// </summary>
	   public string Url { get; }

	   /// <summary>
	   /// HTTP method.
	   /// </summary>
	   public HttpMethod HttpMethod { get; }

	   /// <summary>
	   /// HTTP Response payload.
	   /// </summary>
	   public string ResponsePayload { get; }

	   /// <summary>
	   /// HTTP status code.
	   /// </summary>
	   public HttpStatusCode StatusCode { get; }

	   /// <summary>
	   /// HTTP response reason phrase.
	   /// </summary>
	   public string ReasonPhrase { get; }

	   #endregion // Properties

	   #region Constructors

	   /// <summary>
	   /// Creates an instance of the HttpClientException class with the specified exception details.
	   /// </summary>
	   /// <param name="message">Exception message.</param>
	   /// <param name="url">HTTP URL.</param>
	   /// <param name="responsePayload">HTTP response payload.</param>
	   /// <param name="httpMethod">HTTP method.</param>
	   /// <param name="statusCode">HTTP status code.</param>
	   /// <param name="reasonPhrase">HTTP reason phrase.</param>
	   public HttpClientException(string message, string url, HttpMethod httpMethod, string responsePayload,
		  HttpStatusCode statusCode, string reasonPhrase) : base(message)
	   {
		  Url = url;
		  HttpMethod = httpMethod;
		  ResponsePayload = responsePayload;
		  StatusCode = statusCode;
		  ReasonPhrase = reasonPhrase;
	   }

	   /// <summary>
	   /// Creates and initializes a HttpClientException with serialized data.
	   /// </summary>
	   /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
	   /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
	   protected HttpClientException(SerializationInfo info, StreamingContext context) : base(info, context)
	   {
		  Url = info.GetString("Url");
		  HttpMethod = (HttpMethod)info.GetValue("HttpMethod", typeof(HttpMethod));
		  ResponsePayload = info.GetString("ResponsePayload");
		  StatusCode = (HttpStatusCode)info.GetValue("StatusCode", typeof(HttpStatusCode));
		  ReasonPhrase = info.GetString("ReasonPhrase");
	   }

	   #endregion // Constructors


	   #region Protected Methods

	   /// <summary>
	   /// Sets the System.Runtime.Serialization.SerializationInfo with information about the exception.
	   /// </summary>
	   /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
	   /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
	   [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	   public override void GetObjectData(SerializationInfo info, StreamingContext context)
	   {
		  base.GetObjectData(info, context);

		  info.AddValue("Url", Url);
		  info.AddValue("HttpMethod", HttpMethod);
		  info.AddValue("ResponsePayload", ResponsePayload);
		  info.AddValue("StatusCode", StatusCode);
		  info.AddValue("ReasonPhrase", ReasonPhrase);
	   }

	   #endregion // Protected Methods
    }
}
