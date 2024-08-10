using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rocketseat.Application.Services;
using Rocketseat.Application.UseCases.CreateMessage;
using Rocketseat.Application.UseCases.CreateRoom;
using Rocketseat.Application.UseCases.GetRoomMessage;
using Rocketseat.Application.UseCases.GetRoomMessages;
using Rocketseat.Application.UseCases.GetRooms;
using Rocketseat.Application.UseCases.MarkMessageAsAnswered;
using Rocketseat.Application.UseCases.ReactToMessage;
using Rocketseat.Application.UseCases.RemoveReactFromMessage;
using Rocketseat.Domain.Interfaces;
using Rocketseat.Infra.Context;
using Rocketseat.Infra.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Rocketseat.Infra.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {
        services
            .AddDbContext()
            .AddUseCases()
            .AddRepositories()
            .AddMappings()
            .AddWebSocketHandler()
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddRouting(options => options.LowercaseUrls = true);

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateMessageUseCase>();
        services.AddScoped<CreateRoomUseCase>();
        services.AddScoped<GetRoomMessageUseCase>();
        services.AddScoped<GetRoomMessagesUseCase>();
        services.AddScoped<GetRoomsUseCase>();
        services.AddScoped<MarkMessageAsAnsweredUseCase>();
        services.AddScoped<ReactToMessageUseCase>();
        services.AddScoped<RemoveReactFromMessageUseCase>();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CreateMessageMappings));
        services.AddAutoMapper(typeof(CreateRoomMappings));
        services.AddAutoMapper(typeof(GetRoomMessageMappings));
        services.AddAutoMapper(typeof(GetRoomMessagesMappings));
        services.AddAutoMapper(typeof(GetRoomsMappings));

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<RocketseatDbContext>(options =>
        {
            var path = Directory.GetCurrentDirectory().Split("src");
            var dbPath = Path.Combine(path[0], "Rocketseat_DB.db");

            options.UseSqlite($"Data Source={dbPath}");
        });

        return services;
    }

    private static IServiceCollection AddWebSocketHandler(this IServiceCollection services)
    {
        services.AddSingleton<WebSocketHandler>();

        return services;
    }
}