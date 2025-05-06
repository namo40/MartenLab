using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using MartenLab.Core.Events;
using MartenLab.Core.Projections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine.Marten;

namespace MartenLab.Infrastructure.Extensions;

public static class MartenConfig
{
    public static void AddMartenWithDefaults(this IServiceCollection services, IConfiguration config)
    {
        var connStr = config.GetConnectionString("Postgres")
                      ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

        services
            .AddMarten(options =>
            {
                options.Connection(connStr);
                options.DatabaseSchemaName = "public";
                options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.All;

                options.Projections.Add<MemberIndexProjection>(ProjectionLifecycle.Inline);
                options.Projections.Add<MemberProjection>(ProjectionLifecycle.Async);
                options.Events.AddEventType<MemberRegistered>();

                options.Schema.For<MemberIndex>()
                    .UniqueIndex(x => x.UserId)
                    .UniqueIndex(x => x.Nickname);
            })
            .AddAsyncDaemon(DaemonMode.HotCold)
            .UseLightweightSessions()
            .IntegrateWithWolverine();
    }
}