using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.Domain.ToDoTasks;
using ToDoApp.Infrastructure.Database;

namespace ToDoApp.Infrastructure.Domain.ToDoTasks;

public class ToDoTaskEntityTypeConfiguration : IEntityTypeConfiguration<ToDoTask>
{
    public void Configure(EntityTypeBuilder<ToDoTask> builder)
    {
        builder.ToTable("todos", SchemaName.ToDoApp);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(256);

        builder.Property(x => x.PercentageCompletion)
            .IsRequired();

        builder.Property(x => x.ExpirationDateTime)
            .IsRequired();
    }
}