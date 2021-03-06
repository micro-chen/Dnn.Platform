﻿// 
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 
#region Usings

using System.Collections.Generic;

using DotNetNuke.Collections.Internal;
using DotNetNuke.UI.Containers.EventListeners;
using DotNetNuke.UI.Skins.EventListeners;

#endregion

namespace DotNetNuke.Application
{
    /// <summary>
    /// Defines the context for the environment of the DotNetNuke application
    /// </summary>
    public class DotNetNukeContext
    {
        private static DotNetNukeContext _current;
        private readonly Application _application;
        private readonly IList<ContainerEventListener> _containerEventListeners;
        private readonly IList<SkinEventListener> _skinEventListeners;

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNukeContext" /> class.
        /// </summary>
        protected DotNetNukeContext() : this(new Application())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotNetNukeContext" /> class using the provided application as base.
        /// </summary>
        /// <param name="application">The application.</param>
        protected DotNetNukeContext(Application application)
        {
            _application = application;
            _containerEventListeners = new NaiveLockingList<ContainerEventListener>();
            _skinEventListeners = new NaiveLockingList<SkinEventListener>();
        }

		/// <summary>
		/// Get the application.
		/// </summary>
        public Application Application
        {
            get
            {
                return _application;
            }
        }

		/// <summary>
		/// Gets the container event listeners. The listeners will be called in each life cycle of load container.
		/// </summary>
		/// <see cref="ContainerEventListener"/>
		/// <seealso cref="DotNetNuke.UI.Containers.Container.OnInit"/>
		/// <seealso cref="DotNetNuke.UI.Containers.Container.OnLoad"/>
		/// <seealso cref="DotNetNuke.UI.Containers.Container.OnPreRender"/>
		/// <seealso cref="DotNetNuke.UI.Containers.Container.OnUnload"/>
        public IList<ContainerEventListener> ContainerEventListeners
        {
            get
            {
                return _containerEventListeners;
            }
        }

		/// <summary>
		/// Gets the skin event listeners. The listeners will be called in each life cycle of load skin.
		/// </summary>
		/// <see cref="SkinEventListener"/>
		/// <seealso cref="DotNetNuke.UI.Skins.Skin.OnInit"/>
		/// <seealso cref="DotNetNuke.UI.Skins.Skin.OnLoad"/>
		/// <seealso cref="DotNetNuke.UI.Skins.Skin.OnPreRender"/>
		/// <seealso cref="DotNetNuke.UI.Skins.Skin.OnUnload"/>
        public IList<SkinEventListener> SkinEventListeners
        {
            get
            {
                return _skinEventListeners;
            }
        }

		/// <summary>
		/// Gets or sets the current app context.
		/// </summary>
        public static DotNetNukeContext Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DotNetNukeContext();
                }
                return _current;
            }
            set
            {
                _current = value;
            }
        }
    }
}
