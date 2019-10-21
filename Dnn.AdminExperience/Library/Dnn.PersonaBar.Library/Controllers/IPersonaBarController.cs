﻿#region Copyright
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

using Dnn.PersonaBar.Library.Model;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace Dnn.PersonaBar.Library.Controllers
{
    /// <summary>
    /// Interface responsible to manage the PersonaBar structure by User's Roles and Sku
    /// </summary>
    public interface IPersonaBarController
    {
        /// <summary>
        /// Gets the menu structure of the persona bar
        /// </summary>
        /// <param name="portalSettings"></param>
        /// <param name="userInfo">the user that will be used to filter the menu</param>
        /// <returns>Persona bar menu structure for the user</returns>
        PersonaBarMenu GetMenu(PortalSettings portalSettings, UserInfo userInfo);

        /// <summary>
        /// Whether the menu item is visible.
        /// </summary>
        /// <param name="portalSettings">Portal Settings.</param>
        /// <param name="user">User Info.</param>
        /// <param name="menuItem">The menu item.</param>
        /// <returns></returns>
        bool IsVisible(PortalSettings portalSettings, UserInfo user, MenuItem menuItem);
    }
}