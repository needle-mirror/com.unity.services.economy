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
    /// ErrorResponseConflictInventoryDeleteData model
    /// </summary>
    [Preserve]
    [DataContract(Name = "error_response_conflict_inventory_delete_data")]
    internal class ErrorResponseConflictInventoryDeleteData
    {
        /// <summary>
        /// Creates an instance of ErrorResponseConflictInventoryDeleteData.
        /// </summary>
        /// <param name="attempted">attempted param</param>
        /// <param name="existing">existing param</param>
        [Preserve]
        public ErrorResponseConflictInventoryDeleteData(ErrorResponseConflictInventoryDeleteDataAttempted attempted, InventoryResponse existing)
        {
            Attempted = attempted;
            Existing = existing;
        }

        /// <summary>
        /// Parameter attempted of ErrorResponseConflictInventoryDeleteData
        /// </summary>
        [Preserve]
        [DataMember(Name = "attempted", IsRequired = true, EmitDefaultValue = true)]
        public ErrorResponseConflictInventoryDeleteDataAttempted Attempted{ get; }
        
        /// <summary>
        /// Parameter existing of ErrorResponseConflictInventoryDeleteData
        /// </summary>
        [Preserve]
        [DataMember(Name = "existing", IsRequired = true, EmitDefaultValue = true)]
        public InventoryResponse Existing{ get; }
    
        /// <summary>
        /// Formats a ErrorResponseConflictInventoryDeleteData into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Attempted != null)
            {
                serializedModel += "attempted," + Attempted.ToString() + ",";
            }
            if (Existing != null)
            {
                serializedModel += "existing," + Existing.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a ErrorResponseConflictInventoryDeleteData as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            return dictionary;
        }
    }
}
