using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.Spaceship
{
    public class SpaceshipViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public int EnginePower { get; set; }
        public string Country { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifieAt { get; set; }
        public List<ExistingFilePathViewModel> ExistingFilePaths { get; set; } = new List<ExistingFilePathViewModel>();
    }
    public class ExistingFilePathViewModel
    {
        public Guid PhotoId { get; set; }
        public string FilePath { get; set; }
        public Guid SpaceshipId { get; set; }
    }
}
