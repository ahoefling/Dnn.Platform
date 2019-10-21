#region Copyright
//
// DotNetNuke® - https://www.dnnsoftware.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Data;
using System.Xml.Serialization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Security.Permissions;

#endregion

namespace Dnn.PersonaBar.Library.Model
{
    /// -----------------------------------------------------------------------------
    /// Project	 : DotNetNuke
    /// Namespace: DotNetNuke.Security.Permissions
    /// Class	 : MenuPermissionInfo
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// MenuPermissionInfo provides the Entity Layer for Module Permissions
    /// </summary>
    /// -----------------------------------------------------------------------------
    [Serializable]
    public class MenuPermissionInfo : PermissionInfoBase, IHydratable
    {
        #region "Private Members"

        private int _menuId;

        //local property declarations
        private int _menuPermissionId;

        #endregion

        #region "Constructors"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructs a new MenuPermissionInfo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public MenuPermissionInfo()
        {
            _menuPermissionId = Null.NullInteger;
            _menuId = Null.NullInteger;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructs a new MenuPermissionInfo
        /// </summary>
        /// <param name="permission">A PermissionInfo object</param>
        /// -----------------------------------------------------------------------------
        public MenuPermissionInfo(PermissionInfo permission) : this()
        {
            ModuleDefID = Null.NullInteger;
            PermissionCode = "PERSONABAR_MENU";
            PermissionID = permission.PermissionId;
            PermissionKey = permission.PermissionKey;
            PermissionName = permission.PermissionName;
        }

        #endregion

        #region "Public Properties"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets and sets the Module Permission ID
        /// </summary>
        /// <returns>An Integer</returns>
        /// -----------------------------------------------------------------------------
        [XmlElement("menupermissionid")]
        public int MenuPermissionId
        {
            get
            {
                return _menuPermissionId;
            }
            set
            {
                _menuPermissionId = value;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets and sets the Module ID
        /// </summary>
        /// <returns>An Integer</returns>
        /// -----------------------------------------------------------------------------
        [XmlElement("menuid")]
        public int MenuId
        {
            get
            {
                return _menuId;
            }
            set
            {
                _menuId = value;
            }
        }

        [XmlElement("portalid")]
        public int PortalId { get; set; }

        #endregion

        #region IHydratable Members

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Fills a MenuPermissionInfo from a Data Reader
        /// </summary>
        /// <param name="dr">The Data Reader to use</param>
        /// -----------------------------------------------------------------------------
        public void Fill(IDataReader dr)
        {
            base.FillInternal(dr);
            MenuPermissionId = Null.SetNullInteger(dr["MenuPermissionId"]);
            MenuId = Null.SetNullInteger(dr["MenuId"]);
            PortalId = Null.SetNullInteger(dr["PortalId"]);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Gets and sets the Key ID
        /// </summary>
        /// <returns>An Integer</returns>
        /// -----------------------------------------------------------------------------
        [XmlIgnore]
        public int KeyID
        {
            get
            {
                return MenuPermissionId;
            }
            set
            {
                MenuPermissionId = value;
            }
        }

        #endregion

        #region "Public Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Compares if two MenuPermissionInfo objects are equivalent/equal
        /// </summary>
        /// <param name="other">a ModulePermissionObject</param>
        /// <returns>true if the permissions being passed represents the same permission
        /// in the current object
        /// </returns>
        /// <remarks>
        /// This function is needed to prevent adding duplicates to the ModulePermissionCollection.
        /// ModulePermissionCollection.Contains will use this method to check if a given permission
        /// is already included in the collection.
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public bool Equals(MenuPermissionInfo other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return (AllowAccess == other.AllowAccess) && (MenuId == other.MenuId) && (RoleID == other.RoleID) && (PermissionID == other.PermissionID);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Compares if two MenuPermissionInfo objects are equivalent/equal
        /// </summary>
        /// <param name="obj">a ModulePermissionObject</param>
        /// <returns>true if the permissions being passed represents the same permission
        /// in the current object
        /// </returns>
        /// <remarks>
        /// This function is needed to prevent adding duplicates to the ModulePermissionCollection.
        /// ModulePermissionCollection.Contains will use this method to check if a given permission
        /// is already included in the collection.
        /// </remarks>
        /// -----------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (MenuPermissionInfo))
            {
                return false;
            }
            return Equals((MenuPermissionInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_menuId * 397) ^ _menuPermissionId;
            }
        }

        #endregion
    }
}
