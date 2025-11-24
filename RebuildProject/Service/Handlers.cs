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

    #region GetMaxResourceQuery

    public partial class GetMaxResourceQuery : IRequest<GetMaxResourceResult> { }

    public partial class GetMaxResourceResult : Result
    {
    }

    public interface IGetMaxResourcesService
    {
        Task<GetMaxResourceResult> GetMaxResource(GetMaxResourceQuery query, CancellationToken token);
    }

    public class GetMaxResourcesHandler : IRequestHandler<GetMaxResourceQuery, GetMaxResourceResult>
    {
        private readonly IGetMaxResourcesService service;

        public GetMaxResourcesHandler(IGetMaxResourcesService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetMaxResourceResult> Handle(GetMaxResourceQuery request, CancellationToken cancellationToken)
        {
            return await service.GetMaxResource(request, cancellationToken);
        }

        #endregion
    }

    #endregion

    #region GetCapacityPreviewQuery

    public partial class GetCapacityPreviewQuery : IRequest<GetCapacityPreviewResult> { }

    public partial class GetCapacityPreviewResult : Result
    {
    }

    public interface IGetCapacityPreviewService
    {
        Task<GetCapacityPreviewResult> GetCapacityPreview(GetCapacityPreviewQuery query, CancellationToken token);
    }

    public class GetCapacityPreviewHandler : IRequestHandler<GetCapacityPreviewQuery, GetCapacityPreviewResult>
    {
        private readonly IGetCapacityPreviewService service;

        public GetCapacityPreviewHandler(IGetCapacityPreviewService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetCapacityPreviewResult> Handle(GetCapacityPreviewQuery request, CancellationToken cancellationToken)
        {
            return await service.GetCapacityPreview(request, cancellationToken);
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

    #region GetResourcesQueryAssignment
    public partial class GetResourcesAssignmentQuery : IRequest<GetResourcesAssignmentResult> { }

    public partial class GetResourcesAssignmentResult : Result
    {
    }

    public interface IGetResourcesAssignmentService
    {
        Task<GetResourcesAssignmentResult> GetResourceAssignment(GetResourcesAssignmentQuery query, CancellationToken cancellationToken);
    }

    public class GetResourcesAssignmentHandler : IRequestHandler<GetResourcesAssignmentQuery, GetResourcesAssignmentResult>
    {
        private readonly IGetResourcesAssignmentService service;

        public GetResourcesAssignmentHandler(IGetResourcesAssignmentService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourcesAssignmentResult> Handle(GetResourcesAssignmentQuery request, CancellationToken cancellationToken)
        {
            var resource = await service.GetResourceAssignment(request, cancellationToken);
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

    #region GetResourceAssignmentQuery
    public partial class GetResourceAssignmentQuery : IRequest<GetResourceAssignmenResult> { }

    public partial class GetResourceAssignmenResult : Result
    {
    }

    public interface IGetResourceAssignmentService
    {
        Task<GetResourceAssignmenResult> GetResourceAssignment(GetResourceAssignmentQuery query, CancellationToken cancellationToken);
    }

    public class GetResourceAssignmentHandler : IRequestHandler<GetResourceAssignmentQuery, GetResourceAssignmenResult>
    {
        private readonly IGetResourceAssignmentService service;

        public GetResourceAssignmentHandler(IGetResourceAssignmentService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourceAssignmenResult> Handle(GetResourceAssignmentQuery request, CancellationToken cancellationToken)
        {
            var resource = await service.GetResourceAssignment(request, cancellationToken);
            return resource;
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

    #region GetResourcesCapacityQuery

    public partial class GetResourcesCapacityQuery : IRequest<GetResourcesCapacityResult> { }

    public partial class GetResourcesCapacityResult : Result
    {
    }

    public interface IGetResourcesCapacityService
    {
        Task<GetResourcesCapacityResult> GetResourcesCapacity(GetResourcesCapacityQuery query, CancellationToken token);
    }

    public class GetResourcesCapacityHandler : IRequestHandler<GetResourcesCapacityQuery, GetResourcesCapacityResult>
    {
        private readonly IGetResourcesCapacityService service;

        public GetResourcesCapacityHandler(IGetResourcesCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourcesCapacityResult> Handle(GetResourcesCapacityQuery request, CancellationToken cancellationToken)
        {
            return await service.GetResourcesCapacity(request, cancellationToken);
        }

        #endregion
    }

    #endregion

    #region GetResourceCapacityQuery
    public partial class GetResourceCapacityQuery : IRequest<GetResourceCapacityResult> { }

    public partial class GetResourceCapacityResult : Result
    {
    }

    public interface IGetResourceCapacityService
    {
        Task<GetResourceCapacityResult> GetResource(GetResourceCapacityQuery query, CancellationToken cancellationToken);
    }

    public class GetResourceCapacityHandler : IRequestHandler<GetResourceCapacityQuery, GetResourceCapacityResult>
    {
        private readonly IGetResourceCapacityService service;

        public GetResourceCapacityHandler(IGetResourceCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<GetResourceCapacityResult> Handle(GetResourceCapacityQuery request, CancellationToken cancellationToken)
        {
            var resource = await service.GetResource(request, cancellationToken);
            return resource;
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

    #region AddResourceAssignmentCommand

    public partial class AddResourceAssignmentCommand : IRequest<AddResourceAssignmentResult>
    {
    }

    public partial class AddResourceAssignmentResult : Result
    {
        public bool IsValid { get; set; }
    }
    public interface IAddResourceAssignmentService
    {
        Task<AddResourceAssignmentResult> AddResourceAssignment(AddResourceAssignmentCommand query, CancellationToken cancellationToken);
    }


    public class AddResourceAssignmentHandler : IRequestHandler<AddResourceAssignmentCommand, AddResourceAssignmentResult>
    {

        private readonly IAddResourceAssignmentService service;

        public AddResourceAssignmentHandler(IAddResourceAssignmentService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddResourceAssignmentResult> Handle(AddResourceAssignmentCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.AddResourceAssignment(request, cancellationToken);
            return resource;
        }

        #endregion

    }
    #endregion

    #region AddResourceAssignmentCommand

    public partial class AddRecalculateResourceCapacityCommand : IRequest<AddRecalculateResourceCapacityResult>
    {
    }

    public partial class AddRecalculateResourceCapacityResult : Result
    {
        public bool IsValid { get; set; }
    }
    public interface IAddRecalculateResourceCapacityService
    {
        Task<AddRecalculateResourceCapacityResult> RecalculateResourceCapacityCapacity(AddRecalculateResourceCapacityCommand query, CancellationToken cancellationToken);
    }


    public class AddRecalculateResourceCapacityHandler : IRequestHandler<AddRecalculateResourceCapacityCommand, AddRecalculateResourceCapacityResult>
    {

        private readonly IAddRecalculateResourceCapacityService service;

        public AddRecalculateResourceCapacityHandler(IAddRecalculateResourceCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddRecalculateResourceCapacityResult> Handle(AddRecalculateResourceCapacityCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.RecalculateResourceCapacityCapacity(request, cancellationToken);
            return resource;
        }

        #endregion

    }
    #endregion

    #region AddResourceCapacityCommand

    public partial class AddResourceCapacityCommand : IRequest<AddResourceCapacityResult>
    {
    }

    public partial class AddResourceCapacityResult : Result
    {
        public bool IsValid { get; set; }
    }
    public interface IAddResourceCapacityService
    {
        Task<AddResourceCapacityResult> AddResourceCapacity(AddResourceCapacityCommand query, CancellationToken cancellationToken);
    }


    public class AddRequestCapacityHandler : IRequestHandler<AddResourceCapacityCommand, AddResourceCapacityResult>
    {

        private readonly IAddResourceCapacityService service;

        public AddRequestCapacityHandler(IAddResourceCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<AddResourceCapacityResult> Handle(AddResourceCapacityCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.AddResourceCapacity(request, cancellationToken);
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

    #region PatchResourceAssignmentCommand

    public partial class PatchResourceAssignmentCommand : IRequest<PatchResourceAssignmentResult>
    {

    }

    public partial class PatchResourceAssignmentResult : Result
    {
    }
    public interface IUpdateResourceAssingmentService
    {
        Task<PatchResourceAssignmentResult> PatchResource(PatchResourceAssignmentCommand query, CancellationToken cancellationToken);
    }


    public class PatchRequestAssignmentHandler : IRequestHandler<PatchResourceAssignmentCommand, PatchResourceAssignmentResult>
    {

        private readonly IUpdateResourceAssingmentService service;

        public PatchRequestAssignmentHandler(IUpdateResourceAssingmentService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<PatchResourceAssignmentResult> Handle(PatchResourceAssignmentCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.PatchResource(request, cancellationToken);
            return resource;
        }

        #endregion
    }
    #endregion

    #region PatchResourceCommand

    public partial class PatchResourceCapacityCommand : IRequest<PatchResourceCapacityResult>
    {

    }

    public partial class PatchResourceCapacityResult : Result
    {
    }
    public interface IUpdateResourceCapacityService
    {
        Task<PatchResourceCapacityResult> PatchResource(PatchResourceCapacityCommand query, CancellationToken cancellationToken);
    }


    public class PatchRequestCapacityHandler : IRequestHandler<PatchResourceCapacityCommand, PatchResourceCapacityResult>
    {

        private readonly IUpdateResourceCapacityService service;

        public PatchRequestCapacityHandler(IUpdateResourceCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<PatchResourceCapacityResult> Handle(PatchResourceCapacityCommand request, CancellationToken cancellationToken)
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

    #region DeleteResourceAssignmentCommand

    public partial class DeleteResourceAssignmentCommand : IRequest<DeleteResourceAssingmentResult>
    {
    }

    public partial class DeleteResourceAssingmentResult : Result
    {
    }

    public interface IDeleteResourceAssignmentService
    {
        Task<DeleteResourceAssingmentResult> DeleteResource(DeleteResourceAssignmentCommand query, CancellationToken cancellationToken);
    }


    public class DeleteRequestAssignmentHandler : IRequestHandler<DeleteResourceAssignmentCommand, DeleteResourceAssingmentResult>
    {

        private readonly IDeleteResourceAssignmentService service;

        public DeleteRequestAssignmentHandler(IDeleteResourceAssignmentService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<DeleteResourceAssingmentResult> Handle(DeleteResourceAssignmentCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.DeleteResource(request, cancellationToken);
            return resource;
        }

        #endregion
    }

    #endregion

    #region DeleteResourceCapacityCommand

    public partial class DeleteResourceCapacityCommand : IRequest<DeleteResourceCapacityResult>
    {
    }

    public partial class DeleteResourceCapacityResult : Result
    {
    }

    public interface IDeleteResourceCapacityService
    {
        Task<DeleteResourceCapacityResult> DeleteResourceCapacity(DeleteResourceCapacityCommand query, CancellationToken cancellationToken);
    }


    public class DeleteRequestCapacityHandler : IRequestHandler<DeleteResourceCapacityCommand, DeleteResourceCapacityResult>
    {

        private readonly IDeleteResourceCapacityService service;

        public DeleteRequestCapacityHandler(IDeleteResourceCapacityService service)
        {
            this.service = service;
        }

        #region Public Methods

        public async Task<DeleteResourceCapacityResult> Handle(DeleteResourceCapacityCommand request, CancellationToken cancellationToken)
        {
            var resource = await service.DeleteResourceCapacity(request, cancellationToken);
            return resource;
        }

        #endregion
    }

    #endregion
}
