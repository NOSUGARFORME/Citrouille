using Citrouille.Shared.Commands;

namespace Citrouille.Application.Commands;

public record CreateCollection(Guid Id, string Name, string Description, string Theme, 
    List<TemplateWriteModel> Templates, List<TagWriteModel> Tags) : ICommand;

public record TagWriteModel(Guid Id, string Name);

public record TemplateWriteModel(string Name, string Type);