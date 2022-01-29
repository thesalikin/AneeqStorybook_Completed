using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AneeqStorybook.Shared.Domain
{
    public class Author : BaseDomainModel
    {
        [Required]
        public string Name { get; set; }
    }
}