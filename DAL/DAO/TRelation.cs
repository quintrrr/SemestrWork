using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.DAO
{
    [Table("trelation")]
    [PrimaryKey(nameof(ParentId), nameof(ChildId))]
    public class TRelation
    {
        [Column("id_parent")]
        public long ParentId { get; set; }
        [Column("id_child")]
        public long ChildId { get; set; }
    }
}
