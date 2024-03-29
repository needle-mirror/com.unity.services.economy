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
    /// An error response sent back upon player inventory item conflict.
    /// </summary>
    [Preserve]
    [DataContract(Name = "error-response-conflict-inventory-update")]
    internal class ErrorResponseConflictInventoryUpdate
    {
        /// <summary>
        /// An error response sent back upon player inventory item conflict.
        /// </summary>
        /// <param name="type">type param</param>
        /// <param name="title">title param</param>
        /// <param name="status">e.g 409</param>
        /// <param name="code">For example 10204</param>
        /// <param name="detail">detail param</param>
        /// <param name="data">data param</param>
        /// <param name="instance">instance param</param>
        [Preserve]
        public ErrorResponseConflictInventoryUpdate(string type, string title, int status, int code, string detail, ErrorResponseConflictInventoryUpdateData data, string instance = default)
        {
            Type = type;
            Title = title;
            Status = status;
            Code = code;
            Detail = detail;
            Instance = instance;
            Data = data;
        }

        /// <summary>
        /// Parameter type of ErrorResponseConflictInventoryUpdate
        /// </summary>
        [Preserve]
        [DataMember(Name = "type", IsRequired = true, EmitDefaultValue = true)]
        public string Type{ get; }
        
        /// <summary>
        /// Parameter title of ErrorResponseConflictInventoryUpdate
        /// </summary>
        [Preserve]
        [DataMember(Name = "title", IsRequired = true, EmitDefaultValue = true)]
        public string Title{ get; }
        
        /// <summary>
        /// e.g 409
        /// </summary>
        [Preserve]
        [DataMember(Name = "status", IsRequired = true, EmitDefaultValue = true)]
        public int Status{ get; }
        
        /// <summary>
        /// For example 10204
        /// </summary>
        [Preserve]
        [DataMember(Name = "code", IsRequired = true, EmitDefaultValue = true)]
        public int Code{ get; }
        
        /// <summary>
        /// Parameter detail of ErrorResponseConflictInventoryUpdate
        /// </summary>
        [Preserve]
        [DataMember(Name = "detail", IsRequired = true, EmitDefaultValue = true)]
        public string Detail{ get; }
        
        /// <summary>
        /// Parameter instance of ErrorResponseConflictInventoryUpdate
        /// </summary>
        [Preserve]
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string Instance{ get; }
        
        /// <summary>
        /// Parameter data of ErrorResponseConflictInventoryUpdate
        /// </summary>
        [Preserve]
        [DataMember(Name = "data", IsRequired = true, EmitDefaultValue = true)]
        public ErrorResponseConflictInventoryUpdateData Data{ get; }
    
        /// <summary>
        /// Formats a ErrorResponseConflictInventoryUpdate into a string of key-value pairs for use as a path parameter.
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
            if (Data != null)
            {
                serializedModel += "data," + Data.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a ErrorResponseConflictInventoryUpdate as a dictionary of key-value pairs for use as a query parameter.
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
            
            return dictionary;
        }
    }
}
