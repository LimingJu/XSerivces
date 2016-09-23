using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminService.Models
{
    /// <summary>
    /// POCO for People used in a Get method
    /// </summary>
    public class PeopleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public PeopleDetailModel Detail { get; set; }
    }

    public class PeopleDetailModel
    {
        public int Id { get; set; }
        public string SocialNumber { get; set; }
        public List<PeopleModel> Children { get; set; }
    }
}