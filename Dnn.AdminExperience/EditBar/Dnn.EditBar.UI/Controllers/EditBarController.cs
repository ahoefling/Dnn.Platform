﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Dnn.EditBar.Library;
using Dnn.EditBar.Library.Items;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework;
using DotNetNuke.Framework.Reflections;
using DotNetNuke.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Dnn.EditBar.UI.Controllers
{
    public class EditBarController : ServiceLocator<IEditBarController, EditBarController>, IEditBarController
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<EditBarController>>();

        private static object _threadLocker = new object();

        protected override Func<IEditBarController> GetFactory()
        {
            return () => new EditBarController();
        }

        #region Public Methods

        public IDictionary<string, object> GetConfigurations(int portalId)
        {
            var settings = new Dictionary<string, object>();
            var portalSettings = PortalSettings.Current;
            var user = portalSettings.UserInfo;


            settings.Add("applicationPath", Globals.ApplicationPath);
            settings.Add("buildNumber", Host.CrmVersion.ToString(CultureInfo.InvariantCulture));
            settings.Add("userId", user.UserID);
            settings.Add("debugMode", HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled);
            settings.Add("portalId", portalSettings.PortalId);
            settings.Add("culture", portalSettings.CultureCode);
            settings.Add("loginUrl", Globals.LoginURL(HttpContext.Current?.Request.RawUrl, false));
            settings.Add("items", GetMenuItems());

            return settings;
        }

        public IList<BaseMenuItem> GetMenuItems()
        {
            var menuItems = DataCache.GetCache<IList<BaseMenuItem>>(Constants.MenuItemsCacheKey);
            if (menuItems == null)
            {
                lock (_threadLocker)
                {
                    menuItems = DataCache.GetCache<IList<BaseMenuItem>>(Constants.MenuItemsCacheKey);
                    if (menuItems == null)
                    {
                        menuItems = GetMenuItemInstances().ToList();

                        DataCache.SetCache(Constants.MenuItemsCacheKey, menuItems);
                    }
                }
            }

            return menuItems
                    .Where(m => m.Visible())
                    .OrderBy(m => m.Parent)
                    .ThenBy(m => m.Order)
                    .ToList();
        }

        #endregion

        #region Private Methods

        private static IEnumerable<BaseMenuItem> GetMenuItemInstances()
        {
            var types = GetAllMenuItemTypes();

            foreach (var type in types)
            {
                BaseMenuItem menuItem;
                try
                {
                    menuItem = Activator.CreateInstance(type) as BaseMenuItem;
                }
                catch (Exception e)
                {
                    Logger.LogError("Unable to create {0} while getting all edit bar menu items. {1}",
                                       type.FullName, e.Message);
                    menuItem = null;
                }

                if (menuItem != null)
                {
                    yield return menuItem;
                }
            }
        }

        private static IEnumerable<Type> GetAllMenuItemTypes()
        {
            var typeLocator = new TypeLocator();
            return typeLocator.GetAllMatchingTypes(
                t => t != null &&
                     t.IsClass &&
                     !t.IsAbstract &&
                     typeof(BaseMenuItem).IsAssignableFrom(t));
        }

        #endregion
    }
}
