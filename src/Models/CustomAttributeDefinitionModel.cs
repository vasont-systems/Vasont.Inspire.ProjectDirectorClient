//-----------------------------------------------------------------------
// <copyright file="CustomAttributeDefinitionModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// This enumeration is used for the Custom Attribute Type
    /// </summary>
    public enum CustomAttributeType
    {
        /// <summary>
        /// The combo custom attribute type used for dropdown lists.
        /// </summary>
        COMBO,

        /// <summary>
        /// The text custom attribute type used for free-text.
        /// </summary>
        TEXT
    }

    /// <summary>
    /// This class represents the Custom Attribute Definition Model
    /// </summary>
    public class CustomAttributeDefinitionModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAttributeDefinitionModel"/> class.
        /// </summary>
        public CustomAttributeDefinitionModel()
        {
            this.Values = new List<string>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CustomAttributeDefinitionModel" /> is mandatory.
        /// </summary>
        /// <value>
        ///   <c>true</c> if mandatory; otherwise, <c>false</c>.
        /// </value>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public CustomAttributeType Type { get; set; }

        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public List<string> Values { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
