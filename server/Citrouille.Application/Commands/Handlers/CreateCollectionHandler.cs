using Citrouille.Application.Exceptions;
using Citrouille.Application.Services;
using Citrouille.Domain.Factories;
using Citrouille.Domain.Repositories;
using Citrouille.Shared.Commands;

namespace Citrouille.Application.Commands.Handlers;

public class CreateCollectionHandler : ICommandHandler<CreateCollection>
{
    private readonly ICollectionRepository _repository;
    private readonly ICollectionFactory _factory;
    private readonly ICollectionReadService _readService;

    public CreateCollectionHandler(ICollectionRepository repository, ICollectionFactory factory, ICollectionReadService readService)
    {
        _repository = repository;
        _factory = factory;
        _readService = readService;
    }

    public async Task HandleAsync(CreateCollection command)
    {
        var (id, name, description, theme, templates, tags) = command;

        if (await _readService.ExistsByNameAsync(name))
        {
            throw new CollectionAlreadyExistsException(name);
        }
        
        // TODO: get/create tags
        // TODO: create and add collection
        // await _repository.AddAsync(collection);
    }
}