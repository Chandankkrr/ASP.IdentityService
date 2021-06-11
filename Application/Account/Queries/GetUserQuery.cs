using System;
using Application.Common.Models.Account;
using MediatR;

namespace Application.Account.Queries
{
    public class GetUserQuery: IRequest<ApplicationUserResult>
    {
        public Guid Id { get; set; }
    }
}