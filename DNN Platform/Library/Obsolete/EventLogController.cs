﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Services.Log.EventLog
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using DotNetNuke.Abstractions.Logging;
    using DotNetNuke.Abstractions.Portals;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Framework;

    using Microsoft.Extensions.DependencyInjection;

    public partial class EventLogController : ServiceLocator<IEventLogController, EventLogController>, IEventLogController
    {
        [Obsolete("Deprecated in 9.8.0. Use 'DotNetNuke.Abstractions.Logging.EventLogType' instead. Scheduled for removal in v11.0.0.")]
        public enum EventLogType
        {
            USER_CREATED = 0,
            USER_DELETED = 1,
            LOGIN_SUPERUSER = 2,
            LOGIN_SUCCESS = 3,
            LOGIN_FAILURE = 4,
            LOGIN_USERLOCKEDOUT = 5,
            LOGIN_USERNOTAPPROVED = 6,
            CACHE_REFRESHED = 7,
            PASSWORD_SENT_SUCCESS = 8,
            PASSWORD_SENT_FAILURE = 9,
            LOG_NOTIFICATION_FAILURE = 10,
            PORTAL_CREATED = 11,
            PORTAL_DELETED = 12,
            PORTALGROUP_CREATED = 13,
            PORTALGROUP_DELETED = 14,
            PORTAL_ADDEDTOPORTALGROUP = 15,
            PORTAL_REMOVEDFROMPORTALGROUP = 16,
            TAB_CREATED = 17,
            TAB_UPDATED = 18,
            TAB_DELETED = 19,
            TAB_SENT_TO_RECYCLE_BIN = 20,
            TAB_RESTORED = 21,
            USER_ROLE_CREATED = 22,
            USER_ROLE_DELETED = 23,
            USER_ROLE_UPDATED = 24,
            ROLE_CREATED = 25,
            ROLE_UPDATED = 26,
            ROLE_DELETED = 27,
            MODULE_CREATED = 28,
            MODULE_UPDATED = 29,
            MODULE_DELETED = 30,
            MODULE_SENT_TO_RECYCLE_BIN = 31,
            MODULE_RESTORED = 32,
            SCHEDULER_EVENT_STARTED = 33,
            SCHEDULER_EVENT_PROGRESSING = 34,
            SCHEDULER_EVENT_COMPLETED = 35,
            APPLICATION_START = 36,
            APPLICATION_END = 37,
            APPLICATION_SHUTTING_DOWN = 38,
            SCHEDULER_STARTED = 39,
            SCHEDULER_SHUTTING_DOWN = 40,
            SCHEDULER_STOPPED = 41,
            ADMIN_ALERT = 42,
            HOST_ALERT = 43,
            CACHE_REMOVED = 44,
            CACHE_EXPIRED = 45,
            CACHE_UNDERUSED = 46,
            CACHE_DEPENDENCYCHANGED = 47,
            CACHE_OVERFLOW = 48,
            CACHE_REFRESH = 49,
            LISTENTRY_CREATED = 50,
            LISTENTRY_UPDATED = 51,
            LISTENTRY_DELETED = 52,
            DESKTOPMODULE_CREATED = 53,
            DESKTOPMODULE_UPDATED = 54,
            DESKTOPMODULE_DELETED = 55,
            SKINCONTROL_CREATED = 56,
            SKINCONTROL_UPDATED = 57,
            SKINCONTROL_DELETED = 58,
            PORTALALIAS_CREATED = 59,
            PORTALALIAS_UPDATED = 60,
            PORTALALIAS_DELETED = 61,
            PROFILEPROPERTY_CREATED = 62,
            PROFILEPROPERTY_UPDATED = 63,
            PROFILEPROPERTY_DELETED = 64,
            USER_UPDATED = 65,
            DESKTOPMODULEPERMISSION_CREATED = 66,
            DESKTOPMODULEPERMISSION_UPDATED = 67,
            DESKTOPMODULEPERMISSION_DELETED = 68,
            PERMISSION_CREATED = 69,
            PERMISSION_UPDATED = 70,
            PERMISSION_DELETED = 71,
            TABPERMISSION_CREATED = 72,
            TABPERMISSION_UPDATED = 73,
            TABPERMISSION_DELETED = 74,
            AUTHENTICATION_CREATED = 75,
            AUTHENTICATION_UPDATED = 76,
            AUTHENTICATION_DELETED = 77,
            FILE_ADDED = 78,
            FILE_CHANGED = 79,
            FILE_DELETED = 80,
            FILE_DOWNLOADED = 81,
            FILE_MOVED = 82,
            FILE_OVERWRITTEN = 83,
            FILE_RENAMED = 84,
            FILE_METADATACHANGED = 85,
            FOLDER_CREATED = 86,
            FOLDER_UPDATED = 87,
            FOLDER_DELETED = 88,
            PACKAGE_CREATED = 89,
            PACKAGE_UPDATED = 90,
            PACKAGE_DELETED = 91,
            LANGUAGEPACK_CREATED = 92,
            LANGUAGEPACK_UPDATED = 93,
            LANGUAGEPACK_DELETED = 94,
            LANGUAGE_CREATED = 95,
            LANGUAGE_UPDATED = 96,
            LANGUAGE_DELETED = 97,
            LIBRARY_UPDATED = 98,
            SKINPACKAGE_CREATED = 99,
            SKINPACKAGE_UPDATED = 100,
            SKINPACKAGE_DELETED = 101,
            SCHEDULE_CREATED = 102,
            SCHEDULE_UPDATED = 103,
            SCHEDULE_DELETED = 104,
            HOST_SETTING_CREATED = 105,
            HOST_SETTING_UPDATED = 106,
            HOST_SETTING_DELETED = 107,
            PORTALDESKTOPMODULE_CREATED = 108,
            PORTALDESKTOPMODULE_UPDATED = 109,
            PORTALDESKTOPMODULE_DELETED = 110,
            TABMODULE_CREATED = 111,
            TABMODULE_UPDATED = 112,
            TABMODULE_DELETED = 113,
            TABMODULE_SETTING_CREATED = 114,
            TABMODULE_SETTING_UPDATED = 115,
            TABMODULE_SETTING_DELETED = 116,
            MODULE_SETTING_CREATED = 117,
            MODULE_SETTING_UPDATED = 118,
            MODULE_SETTING_DELETED = 119,
            PORTAL_SETTING_CREATED = 120,
            PORTAL_SETTING_UPDATED = 121,
            PORTAL_SETTING_DELETED = 122,
            PORTALINFO_CREATED = 123,
            PORTALINFO_UPDATED = 124,
            PORTALINFO_DELETED = 125,
            AUTHENTICATION_USER_CREATED = 126,
            AUTHENTICATION_USER_UPDATED = 127,
            AUTHENTICATION_USER_DELETED = 128,
            LANGUAGETOPORTAL_CREATED = 129,
            LANGUAGETOPORTAL_UPDATED = 130,
            LANGUAGETOPORTAL_DELETED = 131,
            TAB_ORDER_UPDATED = 132,
            TAB_SETTING_CREATED = 133,
            TAB_SETTING_UPDATED = 134,
            TAB_SETTING_DELETED = 135,
            HOST_SQL_EXECUTED = 136,
            USER_RESTORED = 137,
            USER_REMOVED = 138,
            USER_IMPERSONATED = 139,
            USERNAME_UPDATED = 140,
            IP_LOGIN_BANNED = 141,
            PAGE_NOT_FOUND_404 = 142,
            TABURL_CREATED = 143,
            TABURL_UPDATED = 144,
            TABURL_DELETED = 145,
            SCRIPT_COLLISION = 146,
            POTENTIAL_PAYPAL_PAYMENT_FRAUD = 147,
            WEBSERVER_CREATED = 148,
            WEBSERVER_UPDATED = 149,
            WEBSERVER_DISABLED = 150,
            WEBSERVER_ENABLED = 151,
            WEBSERVER_PINGFAILED = 152,
            FOLDER_MOVED = 153,
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]

        public static void AddSettingLog(EventLogType logTypeKey, string idFieldName, int idValue, string settingName, string settingValue, int userId) =>
            Globals.DependencyProvider.GetRequiredService<IEventLogger>()
                .AddSettingLog((Abstractions.Logging.EventLogType)logTypeKey, idFieldName, idValue, settingName, settingValue, userId);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(string propertyName, string propertyValue, EventLogType logType) =>
            this.service.AddLog(propertyName, propertyValue, (Abstractions.Logging.EventLogType)logType);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(string propertyName, string propertyValue, PortalSettings portalSettings, int userID, EventLogType logType)
        {
            this.AddLog(propertyName, propertyValue, (IPortalSettings)portalSettings, userID, logType);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(string propertyName, string propertyValue, IPortalSettings portalSettings, int userID, EventLogType logType) =>
            this.service.AddLog(propertyName, propertyValue, portalSettings, userID, (Abstractions.Logging.EventLogType)logType);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(string propertyName, string propertyValue, PortalSettings portalSettings, int userID, string logType)
        {
            this.AddLog(propertyName, propertyValue, (IPortalSettings)portalSettings, userID, logType);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(string propertyName, string propertyValue, IPortalSettings portalSettings, int userID, string logType) =>
            this.service.AddLog(propertyName, propertyValue, portalSettings, userID, logType);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(LogProperties properties, PortalSettings portalSettings, int userID, string logTypeKey, bool bypassBuffering)
        {
            this.AddLog(properties, (IPortalSettings)portalSettings, userID, logTypeKey, bypassBuffering);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(LogProperties properties, IPortalSettings portalSettings, int userID, string logTypeKey, bool bypassBuffering) =>
            this.service.AddLog(properties, portalSettings, userID, logTypeKey, bypassBuffering);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(PortalSettings portalSettings, int userID, EventLogType logType)
        {
            this.AddLog((IPortalSettings)portalSettings, userID, logType);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(IPortalSettings portalSettings, int userID, EventLogType logType) =>
            this.service.AddLog(portalSettings, userID, (Abstractions.Logging.EventLogType)logType);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(object businessObject, PortalSettings portalSettings, int userID, string userName, EventLogType logType)
        {
            this.AddLog(businessObject, (IPortalSettings)portalSettings, userID, userName, logType);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(object businessObject, IPortalSettings portalSettings, int userID, string userName, EventLogType logType) =>
            this.service.AddLog(businessObject, portalSettings, userID, userName, (Abstractions.Logging.EventLogType)logType);

        [Obsolete("Deprecated in DNN 9.7.  It has been replaced by the overload taking IPortalSettings. Scheduled removal in v11.0.0.")]
        public void AddLog(object businessObject, PortalSettings portalSettings, int userID, string userName, string logType)
        {
            this.AddLog(businessObject, (IPortalSettings)portalSettings, userID, userName, logType);
        }

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(object businessObject, IPortalSettings portalSettings, int userID, string userName, string logType) =>
            this.service.AddLog(businessObject, portalSettings, userID, userName, logType);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLog(LogInfo logInfo) =>
            this.service.AddLog(logInfo);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLogType(string configFile, string fallbackConfigFile) =>
            this.service.AddLogType(configFile, fallbackConfigFile);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLogType(LogTypeInfo logType) =>
            this.service.AddLogType(logType);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void AddLogTypeConfigInfo(LogTypeConfigInfo logTypeConfig) =>
            this.service.AddLogTypeConfigInfo(logTypeConfig);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void ClearLog() =>
            this.service.ClearLog();

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void DeleteLog(LogInfo logInfo) =>
            this.service.DeleteLog(logInfo);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void DeleteLogType(LogTypeInfo logType) =>
            this.service.DeleteLogType(logType);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void DeleteLogTypeConfigInfo(LogTypeConfigInfo logTypeConfig) =>
            this.service.DeleteLogTypeConfigInfo(logTypeConfig);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public List<LogInfo> GetLogs(int portalID, string logType, int pageSize, int pageIndex, ref int totalRecords) =>
            this.service.GetLogs(portalID, logType, pageSize, pageIndex, ref totalRecords).Cast<LogInfo>().ToList();

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public ArrayList GetLogTypeConfigInfo() =>
            this.service.GetLogTypeConfigInfo();

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public LogTypeConfigInfo GetLogTypeConfigInfoByID(string id) =>
            (LogTypeConfigInfo)this.service.GetLogTypeConfigInfoByID(id);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public Dictionary<string, LogTypeInfo> GetLogTypeInfoDictionary() =>
            this.service
                .GetLogTypeInfoDictionary()
                .ToDictionary(key => key.Key, value => (LogTypeInfo)value.Value);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public object GetSingleLog(LogInfo log, LoggingProvider.ReturnType returnType) =>
            this.service.GetSingleLog(log, (LoggingProviderReturnType)returnType);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public void PurgeLogBuffer() =>
            this.service.PurgeLogBuffer();

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public virtual void UpdateLogTypeConfigInfo(LogTypeConfigInfo logTypeConfig) =>
            this.service.UpdateLogTypeConfigInfo(logTypeConfig);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        public virtual void UpdateLogType(LogTypeInfo logType) =>
            this.service.UpdateLogType(logType);

        [Obsolete("Deprecated in 9.8.0. Use Dependency Injection to resolve 'DotNetNuke.Abstractions.Logging.IEventLogger' instead. Scheduled for removal in v11.0.0.")]
        protected override Func<IEventLogController> GetFactory()
        {
            return () => new EventLogController();
        }
    }
}