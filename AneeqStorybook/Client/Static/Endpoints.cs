using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AneeqStorybook.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string AuthorsEndpoint = $"{Prefix}/Authors";
        public static readonly string TitlesEndpoint = $"{Prefix}/Titles";
        public static readonly string BooksEndpoint = $"{Prefix}/Books";
        public static readonly string PatronsEndpoint = $"{Prefix}/Patrons";
        public static readonly string ReservationsEndpoint = $"{Prefix}/Reservations";
    }
}
