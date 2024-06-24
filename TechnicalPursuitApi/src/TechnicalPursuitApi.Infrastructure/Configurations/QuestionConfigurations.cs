using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TechnicalPursuitApi.Infrastructure.Configurations
{
    public class QuestionConfigurations : IEntityTypeConfiguration<Domain.Question>
    {
        public void Configure(EntityTypeBuilder<Domain.Question> builder)
        {
            builder
                .Property(b => b.Id)
                .IsRequired();

            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.Intitule)
                .IsRequired();

            builder
                .Property(b => b.GoodAnswer)
                .IsRequired();

            builder
                .Property(b => b.PossibleAnswers)
                .IsRequired();
        }
    }
}