﻿using WebApp.Core.Accounts.Commands;
using WebApp.Core.Accounts.Queries;
using Carter;
using MediatR;

namespace WebApp.Api.Features.Accounts
{
    public class AccountEndpoints : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/v1/accounts", async (IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new GetAllAccountQuery());
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex);
                }
            });

            app.MapGet("api/v1/accounts/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new GetAccountQuery(id));
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex);
                }
            });

            //app.MapPut("api/v1/accounts/{id:guid}", async (HttpContext context, int id) =>
            //{

            //});

            app.MapPut("api/v1/accounts/deposit", async (MoneyDepositedAccountCommand depositAmount, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(depositAmount);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex);
                }
            });

            app.MapPost("api/v1/accounts", async (CreateAccountCommand account, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(account);
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex);
                }
            });
          
            app.MapDelete("api/v1/accounts/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                try
                {
                    var result = await mediator.Send(new DeleteAccountCommand(id));
                    return Results.Ok(result);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex);
                }
            });
        }
    }
}
