using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TestEF.Models
{
    public class People
    {
        public int Id { get; set; }
        [Index("IX_NameAndDetail", 1, IsUnique = true)]
        [MaxLength(20)]
        public string Name { get; set; }
        [Index("IX_NameAndDetail", 2, IsUnique = true)]
        public PeopleDetail Detail { get; set; }

    }

    public class PeopleDetail
    {
        public int Id { get; set; }
        public string IdCard { get; set; }
        public string Description { get; set; }
    }
}