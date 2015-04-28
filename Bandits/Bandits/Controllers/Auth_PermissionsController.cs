// Code is generated by Telerik Data Access Service Wizard
// using WebApiController.tt template

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BanditsModel;

namespace Bandits
{
    /// <summary>
    /// Web API Controller for Auth_Permissions entity defined in BanditsModel.BanditsModel data model
    /// </summary>
    public partial class Auth_PermissionsController : OpenAccessBaseApiController<BanditsModel.Auth_Permission, BanditsModel.BanditsModel>
    {
        /// <summary>
        /// Constructor used by the Web API infrastructure.
        /// </summary>
        public Auth_PermissionsController()
        {
            this.repository = new Auth_PermissionRepository();
        }

        /// <summary>
        /// Dependency Injection ready constructor.
        /// Usable also for unit testing.
        /// </summary>
        /// <remarks>Web API Infrastructure will ALWAYS use the default constructor!</remarks>
        /// <param name="repository">Repository instance of the specific type</param>
        public Auth_PermissionsController(IOpenAccessBaseRepository<BanditsModel.Auth_Permission , BanditsModel.BanditsModel> repository)
        {
            this.repository = repository;
        }

        // Get all method is implemented in the base class

        /// <summary>
        /// Gets single instance by it's primary key
        /// </summary>
        /// <param name="id">Primary key value to filter by</param>
        /// <returns>Entity instance if a matching entity is found</returns>
        public virtual BanditsModel.Auth_Permission Get(Int32 id)
        {
            BanditsModel.Auth_Permission entity = repository.GetBy(b => b.PermissionId == id);

            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return entity;
        }

        
        /// <summary>
        /// Updates single entity.
        /// </summary>
        /// <remarks>Replaces the whole existing entity with the provided one</remarks>
        /// <param name="id">ID of the entity to update</param>
        /// <param name="entity">Entity with the new updated values</param>
        /// <returns>HttpStatusCode.BadRequest if ID parameter does not match the ID value of the entity,
        /// or HttpStatusCode.NoContent if the operation was successful</returns>
        public virtual HttpResponseMessage Put(Int32 id, BanditsModel.Auth_Permission entity)
        {
                        if (entity == null ||
                id != entity.PermissionId)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            repository.Update(entity);

            return Request.CreateResponse(HttpStatusCode.NoContent);
                    }

        /// <summary>
        /// Deletes an entity by it's ID
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        /// <returns>Always HttpStatusCode.OK</returns>
        public virtual HttpResponseMessage Delete(Int32 id)
        {
                        BanditsModel.Auth_Permission entity = repository.GetBy(b => b.PermissionId == id);
            if (entity != null)
            {
                repository.Delete(entity);
            }

            // According to the HTTP specification, the DELETE method must be idempotent, 
            // meaning that several DELETE requests to the same URI must have the same effect as a single DELETE request. 
            // Therefore, the method should not return an error code if the product was already deleted.
            return new HttpResponseMessage(HttpStatusCode.OK);
                    }

        /// <summary>
        /// Creates the response sent back to client after a new entity is successfully created.
        /// </summary>
        /// <param name="httpStatusCode">Status code to return</param>
        /// <param name="entityToEmbed">Entity instance to embed in the response</param>
        /// <returns>HttpResponseMessage with the provided status code and object to embed</returns>
        protected override HttpResponseMessage CreateResponse(HttpStatusCode httpStatusCode, BanditsModel.Auth_Permission entityToEmbed)
        {
            HttpResponseMessage response = Request.CreateResponse<BanditsModel.Auth_Permission>(httpStatusCode, entityToEmbed);

            string uri = Url.Link("DefaultApi", new { id = entityToEmbed.PermissionId });
            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}
