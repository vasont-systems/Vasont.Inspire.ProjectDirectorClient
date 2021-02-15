//-----------------------------------------------------------------------
// <copyright file="OrganizationUserModel.cs" company="GlobalLink Vasont">
//     Copyright (c) GlobalLink Vasont. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Vasont.Inspire.ProjectDirectorClient.Models
{
    /// <summary>
    /// This enumeration represents the valid User Types
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// The organization User Type
        /// </summary>
        ORGANIZATION,

        /// <summary>
        /// The vendor User Type
        /// </summary>
        VENDOR,

        /// <summary>
        /// The client user User Type
        /// </summary>
        CLIENT_USER
    }

    /// <summary>
    /// This class represents the Organization User Model
    /// </summary>
    public class OrganizationUserModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationUserModel" /> class.
        /// </summary>
        public OrganizationUserModel()
        {
            this.UserType = UserType.ORGANIZATION;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [Single Sign On user].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [Single Sign On user]; otherwise, <c>false</c>.
        /// </value>
        public bool SsoUser { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="OrganizationUserModel" /> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public UserType UserType { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }
    }
}
