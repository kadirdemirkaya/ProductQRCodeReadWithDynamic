using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductQRCodeReadWithDynamic.Entities.Base
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsSuccess { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
