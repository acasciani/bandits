﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
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
	public partial class BanditsModel : OpenAccessContext, IBanditsModelUnitOfWork
	{
		private static string connectionStringName = @"BanditsConnection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
				
		private static MetadataSource metadataSource = XmlMetadataSource.FromAssemblyResource("EntitiesModel.rlinq");
		
		public BanditsModel()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public BanditsModel(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public BanditsModel(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public BanditsModel(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public BanditsModel(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }
			
		public IQueryable<WebUser> WebUsers 
		{
			get
			{
				return this.GetAll<WebUser>();
			}
		}
		
		public IQueryable<UserRole> UserRoles 
		{
			get
			{
				return this.GetAll<UserRole>();
			}
		}
		
		public IQueryable<Team> Teams 
		{
			get
			{
				return this.GetAll<Team>();
			}
		}
		
		public IQueryable<TeamPlayer> TeamPlayers 
		{
			get
			{
				return this.GetAll<TeamPlayer>();
			}
		}
		
		public IQueryable<Person> People 
		{
			get
			{
				return this.GetAll<Person>();
			}
		}
		
		public IQueryable<Player> Players 
		{
			get
			{
				return this.GetAll<Player>();
			}
		}
		
		public IQueryable<Guardian> Guardians 
		{
			get
			{
				return this.GetAll<Guardian>();
			}
		}
		
		public IQueryable<Program> Programs 
		{
			get
			{
				return this.GetAll<Program>();
			}
		}
		
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MsSql";
			backend.ProviderName = "System.Data.SqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of BanditsModel.
		/// </summary>
		/// <param name="config">The BackendConfiguration of BanditsModel.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface IBanditsModelUnitOfWork : IUnitOfWork
	{
		IQueryable<WebUser> WebUsers
		{
			get;
		}
		IQueryable<UserRole> UserRoles
		{
			get;
		}
		IQueryable<Team> Teams
		{
			get;
		}
		IQueryable<TeamPlayer> TeamPlayers
		{
			get;
		}
		IQueryable<Person> People
		{
			get;
		}
		IQueryable<Player> Players
		{
			get;
		}
		IQueryable<Guardian> Guardians
		{
			get;
		}
		IQueryable<Program> Programs
		{
			get;
		}
	}
}
#pragma warning restore 1591
