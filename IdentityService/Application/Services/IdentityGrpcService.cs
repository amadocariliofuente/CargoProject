using Grpc.Core;
using IdentityService.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Shared.Contracts;

namespace IdentityService.Application.Services;

public class IdentityGrpcService(IUserRepository userRepository) : Shared.Contracts.IdentityService.IdentityServiceBase
{
    private readonly IUserRepository _userRepository = userRepository;

    public override async Task<GetUserEmailResponse> GetUserEmail(
        GetUserEmailRequest request,
        ServerCallContext context)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user is null)
        {
            throw new RpcException(
                new Status(StatusCode.NotFound, "User not found"));
        }

        return new GetUserEmailResponse
        {
            Email = user.Email
        };
    }
}