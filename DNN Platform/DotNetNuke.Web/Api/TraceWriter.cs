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

using System;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;
using DotNetNuke.Common;
using DotNetNuke.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.Web.Api
{
    internal sealed class TraceWriter : ITraceWriter
    {
    	private static readonly ILogger Logger = Globals.DependencyProvider.GetService<ILogger<TraceWriter>>();
        private readonly bool _enabled;

        public TraceWriter(bool isTracingEnabled)
        {
            _enabled = isTracingEnabled;
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if(!_enabled || level == TraceLevel.Off)
            {
                return;
            }

            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            Log(rec);
        }

        private void Log(TraceRecord rec)
        {
            var message = new StringBuilder();

            if (rec.Request != null)
            {
                if (rec.Request.Method != null)
                {
                    message.Append(" ").Append(rec.Request.Method.Method);
                }

                if (rec.Request.RequestUri != null)
                {
                    message.Append(" ").Append(rec.Request.RequestUri.AbsoluteUri);
                }
            }

            if (!string.IsNullOrEmpty(rec.Category))
            {
                message.Append(" ").Append(rec.Category);
            }

            if (!string.IsNullOrEmpty(rec.Message))
            {
                message.Append(" ").Append(rec.Message);
            }

            string output = message.ToString();

            if (!String.IsNullOrEmpty(output))
            {
                switch (rec.Level)
                {
                    case TraceLevel.Debug:
                        Logger.LogDebug(output);
                        break;
                    case TraceLevel.Info:
                        Logger.LogInformation(output);
                        break;
                    case TraceLevel.Warn:
                        Logger.LogWarning(output);
                        break;
                    case TraceLevel.Error:
                        Logger.LogError(output);
                        break;
                    case TraceLevel.Fatal:
                        Logger.LogCritical(output);
                        break;
                }
            }
        }
    }
}