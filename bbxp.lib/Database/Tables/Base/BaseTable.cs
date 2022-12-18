using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bbxp.lib.Database.Tables.Base
{
    public class BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Active { get;set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}