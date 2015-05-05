using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Telerik.OpenAccess;

namespace Bandits.Usability
{
    public enum UpsertViewMode { Create = 0, Edit, View, Undefined }

    public class UpsertException : Exception {
        public UpsertException(string p) : base(p) { }
    }

    /// <typeparam name="T">The scoped controller type</typeparam>
    /// <typeparam name="K">The model type</typeparam>
    /// <typeparam name="Q">The struct type of the primary key</typeparam>
    public abstract class UpsertBase<T, K, Q> : System.Web.UI.UserControl
        where T : IScopable<K, Q>, new()
        where K : class, new()
        where Q : struct
    {
        public Q? PrimaryKey
        {
            get
            {
                Q result;
                return ViewState["PrimaryKey"] != null && TryParse(ViewState["PrimaryKey"].ToString(), out result) ? result : (Q?)null;
            }
            set
            {
                ViewState["PrimaryKey"] = value;
            }
        }

        public UpsertViewMode UpsertViewMode
        {
            get
            {
                UpsertViewMode upsertViewMode;
                return ViewState["UpsertViewMode"] != null && Enum.TryParse<UpsertViewMode>(ViewState["UpsertViewMode"].ToString(), out upsertViewMode) ? upsertViewMode : UpsertViewMode.Undefined;
            }
            set
            {
                ViewState["UpsertViewMode"] = value;
            }
        }

        public string RequiredPermission
        {
            get
            {
                switch (UpsertViewMode)
                {
                    case Usability.UpsertViewMode.Create: return CreatePermission;
                    case Usability.UpsertViewMode.Edit: return EditPermission;
                    case Usability.UpsertViewMode.View: return ViewPermission;
                    default: throw new Exception("Unable to get required permission.");
                }
            }
        }

        protected abstract bool TryParse(string raw, out Q value);
        protected abstract Func<K, Q> IdentityCheck { get; }
        protected abstract string CreatePermission { get; }
        protected abstract string EditPermission { get; }
        protected abstract string ViewPermission { get; }

        protected bool IsCurrentUserScopedToEntity()
        {
            if (UpsertViewMode == Usability.UpsertViewMode.Create)
            {
                return true;
            }

            Scoped<T, K, Q> controller = new Scoped<T, K, Q>();
            return controller.GetScopedObjects(RequiredPermission, i => PrimaryKey.HasValue && IdentityCheck(i).Equals(PrimaryKey.Value)).Count() > 0;
        }

        protected K Entity
        {
            get
            {
                if (UpsertViewMode == Usability.UpsertViewMode.Undefined)
                {
                    throw new UpsertException("An error occurred retrieving the selected entity. No valid upsert view mode was selected.");
                }

                K entity = Session["UpsertEntity"] as K;
                if (entity == null)
                {
                    if (PrimaryKey.HasValue && (UpsertViewMode == Usability.UpsertViewMode.View || UpsertViewMode == Usability.UpsertViewMode.Edit))
                    {
                        K possible = GetEntity(PrimaryKey.Value);

                        if (possible == null)
                        {
                            throw new UpsertException("An error occurred retrieving the selected entity.");
                        }

                        entity = possible;
                    }
                    else
                    {
                        entity = new K();
                    }

                    Session["UpsertEntity"] = entity;
                }

                return entity;
            }
        }

        protected abstract K GetEntity(Q id);

        protected abstract K SaveEntity();

        protected abstract void Delete();

        protected abstract bool IsSaveValid();
    }
}