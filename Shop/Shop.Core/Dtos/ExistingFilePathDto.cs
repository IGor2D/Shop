using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Dtos
{
    public class ExistingFilePathDto
    {
        public Guid Id { get; set; }
        public string ExistingFilePat { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? CarId { get; set; }
        public Guid? SpaceshipId { get; set; }
    }
}
