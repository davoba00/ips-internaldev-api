using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using RebuildProject.Extensions;

namespace RebuildProject.Controllers
{
    // TODO: remove
    // - remove unused `usings`
    //
    public abstract class BaseODataController : ODataController
    {
        #region Protected Fields

        protected readonly IMediator mediator;

        #endregion

        #region Constructors

        public BaseODataController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Protected Methods

        protected ObjectResult StatusCode(Result result)
        {
            return base.StatusCode(result.GetErrorStatusCode(), result.Errors);
        }

        #endregion
    }
}
