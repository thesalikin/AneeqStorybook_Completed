﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AneeqStorybook.Shared.Domain
{
    public class Book : BaseDomainModel
    {
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
        [Required]
        public int? TitleId { get; set; }
        public virtual Title Title { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}