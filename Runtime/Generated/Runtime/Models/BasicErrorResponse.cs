//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Economy.Internal.Http;



namespace Unity.Services.Economy.Internal.Models
{
    /// <summary>
    /// Referenced from - https://tools.ietf.org/html/rfc7807#page-3 Consumers MUST use the &#39;type&#39; string as the primary identifier for the problem type; the &#39;title&#39; string is advisory and included only for users who are not aware of the semantics of the URI and do not have the ability to discover them (for example, offline log analysis). Consumers SHOULD NOT automatically dereference the type URI. The \&quot;status\&quot; member, if present, is only advisory; it conveys the HTTP status code used for the convenience of the consumer. Generators MUST use the same status code in the actual HTTP response, to assure that generic HTTP software that does not understand this format still behaves correctly.  See Section 5 for further caveats regarding its use. Consumers can use the status member to determine what the original status code used by the generator was, in cases where it has been changed (for example, by an intermediary or cache), and when message bodies persist without HTTP information.  Generic HTTP software still uses the HTTP status code. The \&quot;detail\&quot; member, if present, should focus on helping the client correct the problem, rather than giving debugging information. 
    /// </summary>
    [Preserve]
    [DataContract(Name = "basic-error-response")]
    internal class BasicErrorResponse
    {
        /// <summary>
        /// Referenced from - https://tools.ietf.org/html/rfc7807#page-3 Consumers MUST use the &#39;type&#39; string as the primary identifier for the problem type; the &#39;title&#39; string is advisory and included only for users who are not aware of the semantics of the URI and do not have the ability to discover them (for example, offline log analysis). Consumers SHOULD NOT automatically dereference the type URI. The \&quot;status\&quot; member, if present, is only advisory; it conveys the HTTP status code used for the convenience of the consumer. Generators MUST use the same status code in the actual HTTP response, to assure that generic HTTP software that does not understand this format still behaves correctly.  See Section 5 for further caveats regarding its use. Consumers can use the status member to determine what the original status code used by the generator was, in cases where it has been changed (for example, by an intermediary or cache), and when message bodies persist without HTTP information.  Generic HTTP software still uses the HTTP status code. The \&quot;detail\&quot; member, if present, should focus on helping the client correct the problem, rather than giving debugging information. 
        /// </summary>
        /// <param name="type">A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when dereferenced, it provide human-readable documentation for the problem type (for example, using HTML [W3C.REC-html5-20141028]). When this member is not present, its value is assumed to be \&quot;about:blank\&quot;.</param>
        /// <param name="title">A short, human-readable summary of the problem type. It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization (for example, using proactive content negotiation; see [RFC7231], Section 3.4).</param>
        /// <param name="status">The HTTP status code ([RFC7231], Section 6) generated by the origin server for this occurrence of the problem.</param>
        /// <param name="code">Service specific error code.</param>
        /// <param name="detail">A human-readable explanation specific to this occurrence of the problem.</param>
        /// <param name="instance">A URI reference that identifies the specific occurrence of the problem. It may or may not yield further information if dereferenced.</param>
        /// <param name="details">Machine readable service specific errors.</param>
        [Preserve]
        public BasicErrorResponse(string type, string title = default, int status = default, int code = default, string detail = default, string instance = default, List<object> details = default)
        {
            Type = type;
            Title = title;
            Status = status;
            Code = code;
            Detail = detail;
            Instance = instance;
            Details = (List<IDeserializable>) JsonObject.GetNewJsonObjectResponse(details);
        }

        /// <summary>
        /// A URI reference [RFC3986] that identifies the problem type. This specification encourages that, when dereferenced, it provide human-readable documentation for the problem type (for example, using HTML [W3C.REC-html5-20141028]). When this member is not present, its value is assumed to be \&quot;about:blank\&quot;.
        /// </summary>
        [Preserve]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public string Type{ get; }
        
        /// <summary>
        /// A short, human-readable summary of the problem type. It SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization (for example, using proactive content negotiation; see [RFC7231], Section 3.4).
        /// </summary>
        [Preserve]
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title{ get; }
        
        /// <summary>
        /// The HTTP status code ([RFC7231], Section 6) generated by the origin server for this occurrence of the problem.
        /// </summary>
        [Preserve]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int Status{ get; }
        
        /// <summary>
        /// Service specific error code.
        /// </summary>
        [Preserve]
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int Code{ get; }
        
        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        [Preserve]
        [DataMember(Name = "detail", EmitDefaultValue = false)]
        public string Detail{ get; }
        
        /// <summary>
        /// A URI reference that identifies the specific occurrence of the problem. It may or may not yield further information if dereferenced.
        /// </summary>
        [Preserve]
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string Instance{ get; }
        
        /// <summary>
        /// Machine readable service specific errors.
        /// </summary>
        [Preserve]
        [DataMember(Name = "details", EmitDefaultValue = false)]
        public List<IDeserializable> Details{ get; }
    
        /// <summary>
        /// Formats a BasicErrorResponse into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Type != null)
            {
                serializedModel += "type," + Type + ",";
            }
            if (Title != null)
            {
                serializedModel += "title," + Title + ",";
            }
            serializedModel += "status," + Status.ToString() + ",";
            serializedModel += "code," + Code.ToString() + ",";
            if (Detail != null)
            {
                serializedModel += "detail," + Detail + ",";
            }
            if (Instance != null)
            {
                serializedModel += "instance," + Instance + ",";
            }
            if (Details != null)
            {
                serializedModel += "details," + Details.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a BasicErrorResponse as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (Type != null)
            {
                var typeStringValue = Type.ToString();
                dictionary.Add("type", typeStringValue);
            }
            
            if (Title != null)
            {
                var titleStringValue = Title.ToString();
                dictionary.Add("title", titleStringValue);
            }
            
            var statusStringValue = Status.ToString();
            dictionary.Add("status", statusStringValue);
            
            var codeStringValue = Code.ToString();
            dictionary.Add("code", codeStringValue);
            
            if (Detail != null)
            {
                var detailStringValue = Detail.ToString();
                dictionary.Add("detail", detailStringValue);
            }
            
            if (Instance != null)
            {
                var instanceStringValue = Instance.ToString();
                dictionary.Add("instance", instanceStringValue);
            }
            
            if (Details != null)
            {
                var detailsStringValue = Details.ToString();
                dictionary.Add("details", detailsStringValue);
            }
            
            return dictionary;
        }
    }
}
