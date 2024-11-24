using System.ComponentModel.DataAnnotations;

namespace SpendSmartV3.Objects.Models.Expense
{
    public class Expense : AModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Discerption { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Value { get; set; }
    }
}
