﻿namespace MovieSessions.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Threading.Tasks;
    using MovieSessions.Data;
    using MovieSessions.Models;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Testing
    {
        static readonly IServiceScopeFactory ScopeFactory;

        public static IConfigurationRoot Configuration { get; }

        static Testing()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables(Program.ApplicationName + ":")
                .Build();

            var startup = new Startup(Configuration);
            var services = new ServiceCollection();
            startup.ConfigureServices(services);

            var rootContainer = services.BuildServiceProvider();
            ScopeFactory = rootContainer.GetService<IServiceScopeFactory>();
        }

        public static string Json(object? value) =>
            JsonSerializer.Serialize(value, new JsonSerializerOptions
            {
                WriteIndented = true
            });

        public static void Scoped<TService>(Action<TService> action)
        {
            using var scope = ScopeFactory.CreateScope();
            action(scope.ServiceProvider.GetService<TService>());
        }

        public static async Task Send(IRequest message)
        {
            using var scope = ScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            Validator(serviceProvider, message)?.Validate(message).ShouldBeSuccessful();

            var database = serviceProvider.GetService<SessionDatabase>();

            try
            {
                database.BeginTransaction();
                await serviceProvider.GetService<IMediator>().Send(message);
                database.CloseTransaction();
            }
            catch (Exception exception)
            {
                database.CloseTransaction(exception);
                throw;
            }
        }

        public static async Task<TResponse> Send<TResponse>(IRequest<TResponse> message)
        {
            TResponse response;

            using var scope = ScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            Validator(serviceProvider, message)?.Validate(message).ShouldBeSuccessful();

            var database = serviceProvider.GetService<SessionDatabase>();

            try
            {
                //database.BeginTransaction();
                response = await serviceProvider.GetService<IMediator>().Send(message);
                //database.CloseTransaction();
            }
            catch (Exception exception)
            {
                //database.CloseTransaction(exception);
                throw;
            }

            return response;
        }

        public static void Transaction(Action<SessionDatabase> action)
        {
            using var scope = ScopeFactory.CreateScope();
            var database = scope.ServiceProvider.GetService<SessionDatabase>();

            try
            {
                database.BeginTransaction();
                action(database);
                database.CloseTransaction();
            }
            catch (Exception exception)
            {
                database.CloseTransaction(exception);
                throw;
            }
        }

        public static TResult Query<TResult>(Func<SessionDatabase, TResult> query)
        {
            using var scope = ScopeFactory.CreateScope();
            var database = scope.ServiceProvider.GetService<SessionDatabase>();

            try
            {
                database.BeginTransaction();
                var result = query(database);
                database.CloseTransaction();
                return result;
            }
            catch (Exception exception)
            {
                database.CloseTransaction(exception);
                throw;
            }
        }

        public static TEntity Query<TEntity>(Guid id) where TEntity : Entity
        {
            return Query(database => database.Set<TEntity>().Find(id));
        }

        public static int Count<TEntity>() where TEntity : class
        {
            return Query(database => database.Set<TEntity>().Count());
        }

        public static ValidationResult Validation<TResult>(IRequest<TResult> message)
        {
            using var scope = ScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var validator = Validator(serviceProvider, message);

            if (validator == null)
                throw new Exception($"There is no validator for {message.GetType()} messages.");

            return validator.Validate(message);
        }

        static IValidator? Validator<TResult>(IServiceProvider serviceProvider, IRequest<TResult> message)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(message.GetType());
            return serviceProvider.GetService(validatorType) as IValidator;
        }
    }
}
