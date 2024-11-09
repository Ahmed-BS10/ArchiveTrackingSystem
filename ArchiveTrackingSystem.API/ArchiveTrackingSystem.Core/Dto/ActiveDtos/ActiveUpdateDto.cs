﻿namespace ArchiveTrackingSystem.Core.Dto.ActiveDtos
{
    public class ActiveUpdateDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string CommercialNumber { get; set; }
        public string TaxNumber { get; set; }
        public int DocumentCount { get; set; }
        public string EmployeeName { get; set; }
        public string FileStatus { get; set; }
        public string Notes { get; set; }
        public int ArchiveID { get; set; }
        public int AddressID { get; set; }
        public int ActiveID { get; set; }
        public int PaymentID { get; set; }

    }
}
