using System;
using System.ComponentModel.DataAnnotations;
namespace Lab1_Korcz.Rest.Models
{
    public class Posts
    {   [Key]
        public int PostId { get; set; }
        public string UserID { get; set; }
        /*
                [DataType(DataType.Date)]
                [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
                public DateTime CreatedDate { get; set; }
        */
        public string CreatedDate { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }

    }
}
