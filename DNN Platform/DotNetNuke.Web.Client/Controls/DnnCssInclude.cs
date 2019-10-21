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

using System.Web.UI;

namespace DotNetNuke.Web.Client.ClientResourceManagement
{
    using ClientDependency.Core.Controls;

    /// <summary>
    /// Registers a CSS resource
    /// </summary>
    public class DnnCssInclude : CssInclude
    {
        public DnnCssInclude()
        {
            ForceProvider = ClientResourceManager.DefaultCssProvider;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            this.PathNameAlias = this.PathNameAlias.ToLowerInvariant();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (AddTag || Context.IsDebuggingEnabled)
            {
                writer.Write("<!--CDF({0}|{1}|{2}|{3})-->", DependencyType, FilePath, ForceProvider, Priority);
            }
        }
    }
}
