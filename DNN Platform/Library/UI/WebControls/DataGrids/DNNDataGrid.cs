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
using System.Web.UI.WebControls;

#endregion

namespace DotNetNuke.UI.WebControls
{
    /// -----------------------------------------------------------------------------
    /// Project:    DotNetNuke
    /// Namespace:  DotNetNuke.UI.WebControls
    /// Class:      DNNDataGrid
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The DNNDataGrid control provides an Enhanced Data Grid, that supports other
    /// column types
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    public class DNNDataGrid : DataGrid
    {
        #region "Events"

        public event DNNDataGridCheckedColumnEventHandler ItemCheckedChanged;

        #endregion

        #region "Private Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Centralised Event that is raised whenever a check box is changed.
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void OnItemCheckedChanged(object sender, DNNDataGridCheckChangedEventArgs e)
        {
            if (ItemCheckedChanged != null)
            {
                ItemCheckedChanged(sender, e);
            }
        }

        #endregion

        #region "Protected Methods"

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Called when the grid is Data Bound
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected override void OnDataBinding(EventArgs e)
        {
            foreach (DataGridColumn column in Columns)
            {
                if (ReferenceEquals(column.GetType(), typeof (CheckBoxColumn)))
                {
                    //Manage CheckBox column events
                    var cbColumn = (CheckBoxColumn) column;
                    cbColumn.CheckedChanged += OnItemCheckedChanged;
                }
            }
        }

        protected override void CreateControlHierarchy(bool useDataSource)
        {
            base.CreateControlHierarchy(useDataSource);
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
        }

        #endregion
    }
}
