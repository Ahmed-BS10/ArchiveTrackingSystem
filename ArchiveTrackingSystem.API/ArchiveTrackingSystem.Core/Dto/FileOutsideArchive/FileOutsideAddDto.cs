using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.FileOutsideArchive
{
    public class FileOutsideAddDto
    {
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }

        // ForeignKey
        public int EmployeID { get; set; }
        public int FileID { get; set; }
    }
}
