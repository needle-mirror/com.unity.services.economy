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
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Economy.Internal.Http;



namespace Unity.Services.Economy.Internal.Models
{
    /// <summary>
    /// Single error in the Validation Error Response.
    /// </summary>
    [Preserve]
    [DataContract(Name = "validation-error-body")]
    internal class ValidationErrorBody
    {
        /// <summary>
        /// Single error in the Validation Error Response.
        /// </summary>
        /// <param name="field">field param</param>
        /// <param name="messages">messages param</param>
        [Preserve]
        public ValidationErrorBody(string field, List<string> messages)
        {
            Field = field;
            Messages = messages;
        }

        /// <summary>
        /// 
        /// </summary>
        [Preserve]
        [DataMember(Name = "field", IsRequired = true, EmitDefaultValue = true)]
        public string Field{ get; }
        /// <summary>
        /// 
        /// </summary>
        [Preserve]
        [DataMember(Name = "messages", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Messages{ get; }
    
    }
}

