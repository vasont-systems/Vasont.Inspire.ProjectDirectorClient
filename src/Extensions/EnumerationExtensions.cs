//-----------------------------------------------------------------------
// <copyright file="EnumerationExtensions.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Extensions
{
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// The class represents the extension methods for Enumeration objects.
    /// </summary>
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Retrieves the Attribute Value associated with the enumeration Value.
        /// </summary>
        /// <typeparam name="T">Contains the enumeration value.</typeparam>
        /// <param name="enumerationValue">The enumeration value.</param>
        /// <returns>Returns the attribute value for the enumeration.</returns>
        public static string ToEnumMemberAttrValue<T>(this T enumerationValue)
        {
            var enumerationType = typeof(T);

            var memberInfo = enumerationType.GetMember(enumerationValue.ToString());
            var enumerationAttribute = memberInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            
            return enumerationAttribute != null ? enumerationAttribute.Value : enumerationValue.ToString();
        }
    }
}
