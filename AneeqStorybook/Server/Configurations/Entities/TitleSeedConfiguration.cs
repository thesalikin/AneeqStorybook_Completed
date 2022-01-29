using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AneeqStorybook.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AneeqStorybook.Server.Configurations.Entities
{
    public class TitleSeedConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.HasData(
                new Title()
                {
                    Id = 1,
                    Name = "The Bell Jar",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Title()
                {
                    Id = 2,
                    Name = "Lost Treasure Of The Emerald Eye",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                 new Title()
                 {
                     Id = 3,
                     Name = "Tokyo Veno Station",
                     DateCreated = DateTime.Now,
                     DateUpdated = DateTime.Now,
                     CreatedBy = "System",
                     UpdatedBy = "System"
                 }
                );
        }
    }
}
