using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shop.Core.Domain
{
    public class Car
    {
        [Key]
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string Color { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifieAt { get; set; }
        public IEnumerable<ExistingFilePath> ExistingFilePaths { get; set; } = new List<ExistingFilePath>();
    }
}
