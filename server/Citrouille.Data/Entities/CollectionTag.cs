using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Citrouille.Data.Entities;

public class CollectionTag
{
    public Guid CollectionId { get; set; }
    public Guid TagId { get; set; }
    public Collection Collection { get; set; }
    public Tag Tag { get; set; }
    
    internal sealed class Configuration : IEntityTypeConfiguration<CollectionTag>
    {
        public void Configure(EntityTypeBuilder<CollectionTag> builder)
        {
            builder.HasKey(ct => new { ct.CollectionId, ct.TagId });
            builder.ToTable("CollectionTags");

            builder
                .HasOne(ct => ct.Collection)
                .WithMany(c => c.Tags);

            builder
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.Collections);
        }
    }
}