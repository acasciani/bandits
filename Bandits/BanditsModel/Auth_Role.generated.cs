#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using BanditsModel;

namespace BanditsModel	
{
	public partial class Auth_Role
	{
		private int _roleId;
		public virtual int RoleId
		{
			get
			{
				return this._roleId;
			}
			set
			{
				this._roleId = value;
			}
		}
		
		private string _roleName;
		public virtual string RoleName
		{
			get
			{
				return this._roleName;
			}
			set
			{
				this._roleName = value;
			}
		}
		
		private string _comments;
		public virtual string Comments
		{
			get
			{
				return this._comments;
			}
			set
			{
				this._comments = value;
			}
		}
		
		private IList<Auth_Permission> _auth_Permissions = new List<Auth_Permission>();
		public virtual IList<Auth_Permission> Permissions
		{
			get
			{
				return this._auth_Permissions;
			}
		}
		
		private IList<WebUser> _webUsers = new List<WebUser>();
		public virtual IList<WebUser> WebUsers
		{
			get
			{
				return this._webUsers;
			}
		}
		
	}
}
#pragma warning restore 1591
