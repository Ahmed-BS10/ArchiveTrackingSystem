using ArchiveTrackingSystem.Core.Dto.AddressDtos;
using ArchiveTrackingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.FileDtos
{
    public class FileAddDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CommercialNumber { get; set; }
        [Required]
        public string TaxNumber { get; set; }
        [Required]
        public int DocumentCount { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string FileStatus { get; set; }
        public string? Notes { get; set; }

        // ForeignKey properties
        public int? ArchiveID { get; set; }
        public int? ActiveID { get; set; }
        public int? PaymentID { get; set; }

        public AddressAddDto address { get; set; }
    }
}
