﻿/*
 * Original author: Nicholas Shulman <nicksh .at. u.washington.edu>,
 *                  MacCoss Lab, Department of Genome Sciences, UW
 *
 * Copyright 2014 University of Washington - Seattle, WA
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Windows.Forms;
using pwiz.Common.DataBinding.Controls;

namespace pwiz.Skyline.Controls.Databinding
{
    /// <summary>
    /// Subclass of BoundDataGridView which cooperates with the SkylineWindow in handling 
    /// clipboard commands.
    /// </summary>
    public class BoundDataGridViewEx : BoundDataGridView
    {
        /// <summary>
        /// If this control is a child of the SkylineWindow (not a popup), then returns the
        /// SkylineWindow.  Otherwise returns null.
        /// </summary>
        protected SkylineWindow FindParentSkylineWindow()
        {
            for (Control control = this; control != null; control = control.Parent)
            {
                var skylineWindow = control as SkylineWindow;
                if (skylineWindow != null)
                {
                    return skylineWindow;
                }
            }
            return null;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            var skylineWindow = FindParentSkylineWindow();
            if (skylineWindow != null)
            {
                skylineWindow.ClipboardControlGotFocus(this);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            var skylineWindow = FindParentSkylineWindow();
            if (skylineWindow != null)
            {
                skylineWindow.ClipboardControlLostFocus(this);
            }
        }
    }
}