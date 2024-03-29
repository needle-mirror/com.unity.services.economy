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
    /// ErrorResponseConflictInventoryDeleteDataAttempted model
    /// </summary>
    [Preserve]
    [DataContract(Name = "error_response_conflict_inventory_delete_data_attempted")]
    internal class ErrorResponseConflictInventoryDeleteDataAttempted
    {
        /// <summary>
        /// Creates an instance of ErrorResponseConflictInventoryDeleteDataAttempted.
        /// </summary>
        /// <param name="writeLock">The write lock for the inventory item instance.</param>
        [Preserve]
        public ErrorResponseConflictInventoryDeleteDataAttempted(string writeLock = default)
        {
            WriteLock = writeLock;
        }

        /// <summary>
        /// The write lock for the inventory item instance.
        /// </summary>
        [Preserve]
        [DataMember(Name = "writeLock", EmitDefaultValue = false)]
        public string WriteLock{ get; }
    
        /// <summary>
        /// Formats a ErrorResponseConflictInventoryDeleteDataAttempted into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (WriteLock != null)
            {
                serializedModel += "writeLock," + WriteLock;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a ErrorResponseConflictInventoryDeleteDataAttempted as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (WriteLock != null)
            {
                var writeLockStringValue = WriteLock.ToString();
                dictionary.Add("writeLock", writeLockStringValue);
            }
            
            return dictionary;
        }
    }
}
