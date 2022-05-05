using Citrouille.Data.Entities;

namespace Citrouille.Infrastructure.Commands.Models;

public record CreateCollection(Guid Id, string Title, string Description, List<string> Tags, Guid ThemeId, List<FieldTemplate> Fields);
