using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AneeqStorybook.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AneeqStorybook.Server.Configurations.Entities
{
    public class AuthorSeedConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(
                new Author()
                {
                    Id = 1,
                    Name = "Sylvia Path",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Author()
                {
                    Id = 2,
                    Name = "Geronimo Stilton",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                 new Author()
                 {
                     Id = 3,
                     Name = "Yu Miri",
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now,
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 }
                );
        }
    }
}
