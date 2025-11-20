using FluentResults;
using MediatR;

namespace RebuildProject.Service
{

    #region GetResourcesQuery

    public partial class GetResourcesQuery : IRequest<GetResourcesResult> { }

    public partial class GetResourcesResult : Result
    {
    }

    public interface IGetResourcesService
    {
        Task<GetResourcesResult> GetResources(GetResourcesQuery query, CancellationToken token);
    }

    public class GetResourcesHandler : IRequestHandler<GetResourcesQuery, GetResourcesResult>
    {
        private readonly IGetResourcesService service;

        public GetResourcesHandler(IGetResourcesService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourcesResult> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
        {
            return await service.GetResources(request, cancellationToken);
        }

        #endregion
    }

    #endregion

    #region GetResourceQuery
    public partial class GetResourceQuery : IRequest<GetResourceResult> { }

    public partial class GetResourceResult : Result
    {
    }

    public interface IGetResourceService
    {
        Task<GetResourceResult> GetResource(GetResourceQuery query, CancellationToken cancellationToken);
    }

    public class GetResourceByIdHandler : IRequestHandler<GetResourceQuery, GetResourceResult>
    {
        private readonly IGetResourceService service;

        public GetResourceByIdHandler(IGetResourceService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourceResult> Handle(GetResourceQuery request, CancellationToken cancellationToken)
        {
            var resource = await service.GetResource(request, cancellationToken);
            return resource;
        }

        #endregion
    }

    #endregion

    #region GetResourcesItemQuery

    public partial class GetResourcesItemQuery : IRequest<GetResourcesItemResult> { }

    public partial class GetResourcesItemResult : Result
    {
    }

    public interface IGetResourcesItemsService
    {
        Task<GetResourcesItemResult> GetResources(GetResourcesItemQuery query, CancellationToken cancellationToken);
    }

    public class GetResourcesItemHandler : IRequestHandler<GetResourcesItemQuery, GetResourcesItemResult>
    {
        private readonly IGetResourcesItemsService service;

        public GetResourcesItemHandler(IGetResourcesItemsService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourcesItemResult> Handle(GetResourcesItemQuery request, CancellationToken cancellationToken)
        {
            return await service.GetResources(request, cancellationToken);
        }

        #endregion
    }
    #endregion

    #region GetResourceItemQuery

    public partial class GetResourceItemQuery : IRequest<GetResourceItemResult> { }

    public partial class GetResourceItemResult : Result
    {
    }

    public interface IGetResourceItemService
    {
        Task<GetResourceItemResult> GetResource(GetResourceItemQuery query, CancellationToken cancellationToken);
    }

    public class GetResourceItemHandler : IRequestHandler<GetResourceItemQuery, GetResourceItemResult>
    {
        private readonly IGetResourceItemService service;

        public GetResourceItemHandler(IGetResourceItemService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourceItemResult> Handle(GetResourceItemQuery request, CancellationToken cancellationToken)
        {
            return await service.GetResource(request, cancellationToken);
        }

        #endregion
    }
    #endregion

    #region GetApiLogsQuery

    public partial class GetApiLogsQuery : IRequest<GetApiLogsResult> { }

    public partial class GetApiLogsResult : Result
    {
    }

    public interface IGetApiLogsService
    {
        Task<GetApiLogsResult> ApiLogs(GetApiLogsQuery query, CancellationToken cancellationToken);
    }

    public class GetApiLogsHandler : IRequestHandler<GetApiLogsQuery, GetApiLogsResult>
    {
        private readonly IGetApiLogsService service;

        public GetApiLogsHandler(IGetApiLogsService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetApiLogsResult> Handle(GetApiLogsQuery request, CancellationToken cancellationToken)
        {
            return await service.ApiLogs(request, cancellationToken);
        }

        #endregion
    }
    #endregion

    #region GetApiLogQuery

    public partial class GetApiLogQuery : IRequest<GetApiLogResult> { }

    public partial class GetApiLogResult : Result
    {
    }

    public interface IGetApiLogService
    {
        Task<GetApiLogResult> ApiLog(GetApiLogQuery query, CancellationToken cancellationToken);
    }

    public class GetApiLogHandler : IRequestHandler<GetApiLogQuery, GetApiLogResult>
    {
        private readonly IGetApiLogService service;

        public GetApiLogHandler(IGetApiLogService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetApiLogResult> Handle(GetApiLogQuery request, CancellationToken cancellationToken)
        {
            return await service.ApiLog(request, cancellationToken);
        }

        #endregion
    }
    #endregion

    #region AddResourceCommand

    public partial class AddResourceCommand : IRequest<AddResourceResult>
    {
    }

    public partial class AddResourceResult : Result
    {
        public bool IsValid { get; set; }
    }
    public interface IAddResourceService
    {
        Task<AddResourceResult> AddResource(AddResourceCommand query, CancellationToken cancellationToken);
    }


    public class AddRequestHandler : IRequestHandler<AddResourceCommand, AddResourceResult>
    {

        private readonly IAddResourceService service;

        public AddRequestHandler(IAddResourceService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddResourceResult> Handle(AddResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.AddResource(request, cancellationToken);
            return resource;
        }

        #endregion

    }
    #endregion

    #region AddResourceCommandItem

    public partial class AddResourceItemCommand : IRequest<AddResourceItemResult>
    {
    }

    public partial class AddResourceItemResult : Result
    {

    }
    public interface IAddResourceItemService
    {
        Task<AddResourceItemResult> AddResourceItem(AddResourceItemCommand query, CancellationToken cancellationToken);
    }


    public class AddRequestItemHandler : IRequestHandler<AddResourceItemCommand, AddResourceItemResult>
    {

        private readonly IAddResourceItemService service;

        public AddRequestItemHandler(IAddResourceItemService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddResourceItemResult> Handle(AddResourceItemCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.AddResourceItem(request, cancellationToken);
            return resource;
        }

        #endregion
    }
    #endregion

    #region AddApiLogCommand

    public partial class AddApiLogCommand : IRequest<AddApiLogResult>
    {
    }

    public partial class AddApiLogResult : Result
    {
        public bool IsValid { get; set; }
    }
    public interface IAddApiLogService
    {
        Task<AddApiLogResult> AddApiLog(AddApiLogCommand query, CancellationToken cancellationToken);
    }


    public class AddApiLogHandler : IRequestHandler<AddApiLogCommand, AddApiLogResult>
    {

        private readonly IAddApiLogService service;

        public AddApiLogHandler(IAddApiLogService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddApiLogResult> Handle(AddApiLogCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.AddApiLog(request, cancellationToken);
            return resource;
        }

        #endregion
    }

    #endregion

    #region PatchResourceItemCommand

    public partial class PatchResourceItemCommand : IRequest<PatchResourceItemResult>
    {

    }

    public partial class PatchResourceItemResult : Result
    {
    }
    public interface IUpdateResourceItemService
    {
        Task<PatchResourceItemResult> PatchResourceItem(PatchResourceItemCommand query, CancellationToken cancellationToken);
    }


    public class PatchRequestItemHandler : IRequestHandler<PatchResourceItemCommand, PatchResourceItemResult>
    {

        private readonly IUpdateResourceItemService service;

        public PatchRequestItemHandler(IUpdateResourceItemService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<PatchResourceItemResult> Handle(PatchResourceItemCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.PatchResourceItem(request, cancellationToken);
            return resource;
        }

        #endregion
    }
    #endregion

    #region PatchResourceCommand

    public partial class PatchResourceCommand : IRequest<PatchResourceResult>
    {

    }

    public partial class PatchResourceResult : Result
    {
    }
    public interface IUpdateResourceService
    {
        Task<PatchResourceResult> PatchResource(PatchResourceCommand query, CancellationToken cancellationToken);
    }


    public class PatchRequestHandler : IRequestHandler<PatchResourceCommand, PatchResourceResult>
    {

        private readonly IUpdateResourceService service;

        public PatchRequestHandler(IUpdateResourceService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<PatchResourceResult> Handle(PatchResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.PatchResource(request, cancellationToken);
            return resource;
        }

        #endregion
    }
    #endregion

    #region DeleteResourceCommand

    public partial class DeleteResourceCommand : IRequest<DeleteResourceResult>
    {
    }

    public partial class DeleteResourceResult : Result
    {
    }

    public interface IDeleteResourceService
    {
        Task<DeleteResourceResult> DeleteResource(DeleteResourceCommand query, CancellationToken cancellationToken);
    }


    public class DeleteRequestHandler : IRequestHandler<DeleteResourceCommand, DeleteResourceResult>
    {

        private readonly IDeleteResourceService service;

        public DeleteRequestHandler(IDeleteResourceService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<DeleteResourceResult> Handle(DeleteResourceCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.DeleteResource(request, cancellationToken);
            return resource;
        }

        #endregion
    }

    #endregion

    #region DeleteResourceItemCommand

    public partial class DeleteResourceItemCommand : IRequest<DeleteResourceItemResult>
    {
    }

    public partial class DeleteResourceItemResult : Result
    {
    }

    public interface IDeleteResourceItemService
    {
        Task<DeleteResourceItemResult> DeleteResourceItem(DeleteResourceItemCommand query, CancellationToken cancellationToken);
    }


    public class DeleteResourceItemRequestHandler : IRequestHandler<DeleteResourceItemCommand, DeleteResourceItemResult>
    {

        private readonly IDeleteResourceItemService service;

        public DeleteResourceItemRequestHandler(IDeleteResourceItemService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<DeleteResourceItemResult> Handle(DeleteResourceItemCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.DeleteResourceItem(request, cancellationToken);
            return resource;
        }

        #endregion
    }
    #endregion
}
