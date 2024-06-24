using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TechnicalPursuitApi.Domain.JoueurAggregate.ValueObjects;

namespace TechnicalPursuitApi.Infrastructure.Configurations
{
    public class JoueurConfigurations : IEntityTypeConfiguration<Domain.JoueurAggregate.Joueur>
    {
        public void Configure(EntityTypeBuilder<Domain.JoueurAggregate.Joueur> builder)
        {
            builder
                .HasKey(b => b.Id)
                .HasName("Id");

            builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, id => JoueurId.Create(id));

            builder
                .Property(b => b.Score)
                .IsRequired();
        }
    }
}