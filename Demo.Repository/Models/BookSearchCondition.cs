﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Repository.Models
{
    public class BookSearchCondition
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }
    }
}