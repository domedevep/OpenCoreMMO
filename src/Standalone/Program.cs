﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using NeoServer.Data.Contexts;
using NeoServer.Game.Common;
using NeoServer.Game.Common.Helpers;
using NeoServer.Game.World.Models.Spawns;
using NeoServer.Loaders.Action;
using NeoServer.Loaders.Interfaces;
using NeoServer.Loaders.Items;
using NeoServer.Loaders.Monsters;
using NeoServer.Loaders.Quest;
using NeoServer.Loaders.Spawns;
using NeoServer.Loaders.Spells;
using NeoServer.Loaders.Vocations;
using NeoServer.Loaders.World;
using NeoServer.Networking.Listeners;
using NeoServer.Scripts.Lua;
using NeoServer.Server.Common.Contracts;
using NeoServer.Server.Common.Contracts.Tasks;
using NeoServer.Server.Compiler;
using NeoServer.Server.Configurations;
using NeoServer.Server.Events.Subscribers;
using NeoServer.Server.Helpers.Extensions;
using NeoServer.Server.Jobs.Channels;
using NeoServer.Server.Jobs.Creatures;
using NeoServer.Server.Jobs.Items;
using NeoServer.Server.Jobs.Persistence;
using NeoServer.Server.Security;
using NeoServer.Server.Standalone.IoC;
using NeoServer.Server.Tasks;
using Serilog;

namespace NeoServer.Server.Standalone;

public class Program
{
    public static async Task Main()
    {
        Console.Title = "OpenCoreMMO Server";

        var sw = new Stopwatch();
        sw.Start();

        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

        var container = Container.BuildConfigurations();

        var (serverConfiguration, _, logConfiguration) = (container.Resolve<ServerConfiguration>(),
            container.Resolve<GameConfiguration>(), container.Resolve<LogConfiguration>());

        var (logger, _) = (container.Resolve<ILogger>(), container.Resolve<LoggerConfiguration>());

        logger.Information("Welcome to OpenCoreMMO Server!");

        logger.Information("Log set to: {Log}", logConfiguration.MinimumLevel);
        logger.Information("Environment: {Env}", Environment.GetEnvironmentVariable("ENVIRONMENT"));

        logger.Step("Building extensions...", "{files} extensions build",
            () => ExtensionsCompiler.Compile(serverConfiguration.Data, serverConfiguration.Extensions));

        container = Container.BuildAll();
        Helpers.IoC.Initialize(container);

        GameAssemblyCache.Load();

        await LoadDatabase(container, logger, cancellationToken);

        Rsa.LoadPem(serverConfiguration.Data);

        container.Resolve<IEnumerable<IRunBeforeLoaders>>().ToList().ForEach(x => x.Run());
        container.Resolve<FactoryEventSubscriber>().AttachEvents();

        container.Resolve<ItemTypeLoader>().Load();

        container.Resolve<ActionLoader>().Load();
        container.Resolve<QuestLoader>().Load();

        container.Resolve<WorldLoader>().Load();

        container.Resolve<SpawnLoader>().Load();

        container.Resolve<MonsterLoader>().Load();
        container.Resolve<VocationLoader>().Load();
        container.Resolve<SpellLoader>().Load();

        container.Resolve<IEnumerable<IStartupLoader>>().ToList().ForEach(x => x.Load());

        container.Resolve<SpawnManager>().StartSpawn();

        var scheduler = container.Resolve<IScheduler>();
        var dispatcher = container.Resolve<IDispatcher>();

        dispatcher.Start(cancellationToken);
        scheduler.Start(cancellationToken);

        scheduler.AddEvent(new SchedulerEvent(1000, container.Resolve<GameCreatureJob>().StartChecking));
        scheduler.AddEvent(new SchedulerEvent(1000, container.Resolve<GameItemJob>().StartChecking));
        scheduler.AddEvent(new SchedulerEvent(1000, container.Resolve<GameChatChannelJob>().StartChecking));
        container.Resolve<PlayerPersistenceJob>().Start(cancellationToken);

        container.Resolve<EventSubscriber>().AttachEvents();
        container.Resolve<IEnumerable<IStartup>>().ToList().ForEach(x => x.Run());

        container.Resolve<LuaGlobalRegister>().Register();

        StartListening(container, cancellationToken);

        container.Resolve<IGameServer>().Open();

        sw.Stop();

        logger.Step("Running Garbage Collector", "Garbage collected", () =>
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        });

        logger.Information("Memory usage: {Mem} MB",
            Math.Round(Process.GetCurrentProcess().WorkingSet64 / 1024f / 1024f, 2));

        logger.Information("Server is {Up}! {Time} ms", "up", sw.ElapsedMilliseconds);

        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    private static async Task LoadDatabase(IComponentContext container, ILogger logger,
        CancellationToken cancellationToken)
    {
        var (_, databaseName) = container.Resolve<DatabaseConfiguration>();
        var context = container.Resolve<NeoContext>();

        logger.Information("Loading database: {Db}", databaseName);

        try
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        catch
        {
            logger.Error("Unable to connect to database");
            Environment.Exit(0);
        }

        logger.Information("{Db} database loaded", databaseName);
    }

    private static void StartListening(IComponentContext container, CancellationToken cancellationToken)
    {
        container.Resolve<LoginListener>().BeginListening(cancellationToken);
        container.Resolve<GameListener>().BeginListening(cancellationToken);
    }
}