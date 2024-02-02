using System.ComponentModel.DataAnnotations;

namespace Udemy.Api.Models
{
    public class Base
    {
        [Key]
        public int Id { get; set; }
        private DateTime? _createDate;
        public DateTime? CreateDate
        {
            get { return _createDate; }
            set { _createDate = value == null ? DateTime.Now : value; }
        }
        public DateTime? UpdateDate { get; set; }
    }
}
