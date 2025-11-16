using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DAO
{
    [Table("tproperty")]
    public class TProperty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("value")]
        public string Value { get; set; } = string.Empty;
        [Column("id_group")]
        public long GroupId { get; set; }
    }
}
