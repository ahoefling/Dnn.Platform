﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Dnn.PersonaBar.Extensions.Components.Dto;
using Dnn.PersonaBar.Extensions.Components.Dto.Editors;
using DotNetNuke.Common;
using DotNetNuke.Abstractions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Logging;
using DotNetNuke.Services.Installer.Packages;
using DotNetNuke.Services.Localization;
using Microsoft.Extensions.Logging;

namespace Dnn.PersonaBar.Extensions.Components.Editors
{
    public class CoreLanguagePackageEditor : IPackageEditor
    {
        private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<JsLibraryPackageEditor>>();
        protected INavigationManager NavigationManager { get; }
        public CoreLanguagePackageEditor()
        {
            NavigationManager = Globals.DependencyProvider.GetRequiredService<INavigationManager>();
        }

        public PackageInfoDto GetPackageDetail(int portalId, PackageInfo package)
        {
            var languagePack = LanguagePackController.GetLanguagePackByPackage(package.PackageID);
            var languagesTab = TabController.GetTabByTabPath(portalId, "//Admin//Languages", Null.NullString);

            var detail = new CoreLanguagePackageDetailDto(portalId, package)
            {
                Locales = Utility.GetAllLanguagesList(),
                LanguageId = languagePack.LanguageID,
                EditUrlFormat = NavigationManager.NavigateURL(languagesTab, "", "Locale={0}")
            };

            if (languagePack.PackageType == LanguagePackType.Extension)
            {
                //Get all the packages but only bind to combo if not a language package
                detail.Packages = Utility.GetAllPackagesListExceptLangPacks();
            }

            return detail;
        }

        public bool SavePackageSettings(PackageSettingsDto packageSettings, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                string value;
                var changed = false;
                var languagePack = LanguagePackController.GetLanguagePackByPackage(packageSettings.PackageId);
                if (packageSettings.EditorActions.TryGetValue("languageId", out value)
                    && !string.IsNullOrEmpty(value) && value != languagePack.LanguageID.ToString())
                {
                    languagePack.LanguageID = Convert.ToInt32(value);
                    changed = true;
                }

                if (changed) LanguagePackController.SaveLanguagePack(languagePack);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
