using Citrouille.Data.Exceptions.Theme;

namespace Citrouille.Data.Entities;

public class CollectionTheme
{
    public CollectionTheme()
    {
    }

    public CollectionTheme(string theme)
    {
        if (string.IsNullOrWhiteSpace(theme))
        {
            throw new EmptyCollectionThemeException();
        }

        Theme = theme;
    }
    public Guid Id { get; set; }
    public string Theme { get; set; }
    
    public List<Collection> Collections { get; set; }
}
