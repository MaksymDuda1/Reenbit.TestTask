using Microsoft.Extensions.DependencyInjection;

namespace Chatter.Application.Models;

public class LazyInstance<T>(IServiceProvider serviceProvider) : Lazy<T>(serviceProvider.GetRequiredService<T>());