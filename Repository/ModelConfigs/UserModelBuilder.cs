

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApi.Models.User;

namespace TodoApi.Repository.ModelConfigs;

public class UserModelBuilderConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.HasMany(u => u.Todos)
        .WithOne(t => t.User)
        .HasForeignKey(t => t.UserId)
        .HasPrincipalKey(u => u.Id)
        .IsRequired();
    }
}