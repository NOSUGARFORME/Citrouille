using Citrouille.Data.Entities;
using Citrouille.Shared.Exceptions;

namespace Citrouille.Infrastructure.Commands.Exceptions;

public class MissingThemeException : BadRequestException
{
    public MissingThemeException(Guid id) : base($"Couldn't find theme with id '{id}'") { }
}