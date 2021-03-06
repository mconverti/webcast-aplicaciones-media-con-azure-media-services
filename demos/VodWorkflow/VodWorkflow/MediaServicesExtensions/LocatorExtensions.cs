﻿// <copyright file="LocatorExtensions.cs" company="open-source">
//  No rights reserved. Copyright (c) 2013 by Mariano Converti
//   
//  Redistribution and use in source and binary forms, with or without modification, are permitted.
//
//  The names of its contributors may not be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// </copyright>

namespace Microsoft.WindowsAzure.MediaServices.Client
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains extension methods and helpers related to the <see cref="ILocator"/> interface.
    /// </summary>
    public static class LocatorExtensions
    {
        /// <summary>
        /// Returns a <see cref="System.Threading.Tasks.Task&lt;ILocator&gt;"/> instance for new <see cref="ILocator"/>.
        /// </summary>
        /// <param name="context">The <see cref="CloudMediaContext"/> instance.</param>
        /// <param name="asset">The <see cref="IAsset"/> instance for the new <see cref="ILocator"/>.</param>
        /// <param name="locatorType">The <see cref="LocatorType"/>.</param>
        /// <param name="permissions">The <see cref="AccessPermissions"/> of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="duration">The duration of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="startTime">The start time of the new <see cref="ILocator"/>.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task&lt;ILocator&gt;"/> instance for new <see cref="ILocator"/>.</returns>
        public static async Task<ILocator> CreateLocatorAsync(this CloudMediaContext context, IAsset asset, LocatorType locatorType, AccessPermissions permissions, TimeSpan duration, DateTime? startTime)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "The context cannot be null.");
            }

            if (asset == null)
            {
                throw new ArgumentNullException("asset", "The asset cannot be null.");
            }

            var policy = await context.AccessPolicies.CreateAsync(asset.Name, duration, permissions);

            return await context.Locators.CreateLocatorAsync(locatorType, asset, policy, startTime);
        }

        /// <summary>
        /// Returns a <see cref="System.Threading.Tasks.Task&lt;ILocator&gt;"/> instance for new <see cref="ILocator"/>.
        /// </summary>
        /// <param name="context">The <see cref="CloudMediaContext"/> instance.</param>
        /// <param name="asset">The <see cref="IAsset"/> instance for the new <see cref="ILocator"/>.</param>
        /// <param name="locatorType">The <see cref="LocatorType"/>.</param>
        /// <param name="permissions">The <see cref="AccessPermissions"/> of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="duration">The duration of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task&lt;ILocator&gt;"/> instance for new <see cref="ILocator"/>.</returns>
        public static Task<ILocator> CreateLocatorAsync(this CloudMediaContext context, IAsset asset, LocatorType locatorType, AccessPermissions permissions, TimeSpan duration)
        {
            return context.CreateLocatorAsync(asset, locatorType, permissions, duration, null);
        }

        /// <summary>
        /// Returns a new <see cref="ILocator"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="CloudMediaContext"/> instance.</param>
        /// <param name="asset">The <see cref="IAsset"/> instance for the new <see cref="ILocator"/>.</param>
        /// <param name="locatorType">The <see cref="LocatorType"/>.</param>
        /// <param name="permissions">The <see cref="AccessPermissions"/> of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="duration">The duration of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="startTime">The start time of the new <see cref="ILocator"/>.</param>
        /// <returns>A a new <see cref="ILocator"/> instance.</returns>
        public static ILocator CreateLocator(this CloudMediaContext context, IAsset asset, LocatorType locatorType, AccessPermissions permissions, TimeSpan duration, DateTime? startTime)
        {
            using (Task<ILocator> task = context.CreateLocatorAsync(asset, locatorType, permissions, duration, startTime))
            {
                return task.Result;
            }
        }

        /// <summary>
        /// Returns a new <see cref="ILocator"/> instance.
        /// </summary>
        /// <param name="context">The <see cref="CloudMediaContext"/> instance.</param>
        /// <param name="asset">The <see cref="IAsset"/> instance for the new <see cref="ILocator"/>.</param>
        /// <param name="locatorType">The <see cref="LocatorType"/>.</param>
        /// <param name="permissions">The <see cref="AccessPermissions"/> of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <param name="duration">The duration of the <see cref="IAccessPolicy"/> associated with the new <see cref="ILocator"/>.</param>
        /// <returns>A a new <see cref="ILocator"/> instance.</returns>
        public static ILocator CreateLocator(this CloudMediaContext context, IAsset asset, LocatorType locatorType, AccessPermissions permissions, TimeSpan duration)
        {
            return context.CreateLocator(asset, locatorType, permissions, duration, null);
        }
    }
}
