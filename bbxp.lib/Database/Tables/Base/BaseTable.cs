using System.ComponentModel.DataAnnotations.Schema;

namespace bbxp.lib.Database.Tables.Base
{
    public class BaseTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool Active { get;set; }

        public DateTimeOffset Modified { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}