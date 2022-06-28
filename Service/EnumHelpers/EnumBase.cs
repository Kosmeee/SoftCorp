using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.EnumHelpers
{
        public class EnumBase<TEnum> where TEnum : struct
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }

            [Required]
            [MaxLength(128)]
            public string Name { get; set; }
        }
}
