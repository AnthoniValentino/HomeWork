namespace Library.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Books
    {
        public int id { get; set; }

        public int authorId { get; set; }

        [Required]
        [StringLength(500)]
        public string title { get; set; }

        public int? price { get; set; }

        public int pages { get; set; }

        public virtual Authors Authors { get; set; }
    }
}
