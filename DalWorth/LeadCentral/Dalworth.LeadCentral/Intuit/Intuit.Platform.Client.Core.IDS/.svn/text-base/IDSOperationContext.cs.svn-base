/*
 * Copyright (c) 2009-2010 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 *
 * Contributors:
 *    Intuit Partner Platform – initial contribution
 */

namespace Intuit.Platform.Client.Core.IDS
{

    public delegate void RealmIdChanged(string realmId);

	/// <summary>
	/// Encapsulates the data required to make a call to IDS, including the resource (Entity type), the realm (company),
	/// and any other metadata that provides the Operation context.
	/// </summary>
	public class IDSOperationContext
	{
        private RealmIdChanged RealmChanged;
		private IDSResource _resource;
		private string _realmId;

        /// <summary>
        /// Creates IDSOperationContext object
        /// </summary>
        /// <param name="resource">Resource </param>
        /// <param name="realmId">Realm Id</param>
		public IDSOperationContext(IDSResource resource, string realmId)
		{
            this._resource = resource;
            this.RealmId = realmId;
		}

        /// <summary>
        /// Creates IDSOperationContext object
        /// </summary>
        /// <param name="resource">Resource </param>
        /// <param name="realmId">Realm Id</param>
        /// <param name="realmIdChanged">Delegate function to call when Realm Id changes</param>
        public IDSOperationContext(IDSResource resource, string realmId, RealmIdChanged realmIdChanged)
        {
            this._resource = resource;
            this.RealmChanged = realmIdChanged;
            this.RealmId = realmId;
        }

        /// <summary>
        /// Resource Property
        /// </summary>
		public IDSResource Resource
		{
			get
			{
				return _resource;
			}
		}

        /// <summary>
        /// RealmId Property
        /// </summary>
		public string RealmId
		{
			get
			{
				return _realmId;
			}
            set
            {
                _realmId = value;

                if (RealmChanged != null)
                    RealmChanged(_realmId);
            }
		}

		public string EntityId { get; set; }

		public string OfferingId { get; set; }

		public string Users { get; set; }

		public string RoleCommand { get; set; }

		public string[] CompanyParameters { get; set; }

		public string[] Parameters { get; set; }

        public string ContentType { get; set; }
	}
}