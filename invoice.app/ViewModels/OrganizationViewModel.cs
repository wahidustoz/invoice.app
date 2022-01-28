using System;

namespace invoice.app.ViewModels
{
    public class OrganizationViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public int EmployeeOrganizationsCount { get; set; }
        
        public int InvoicesCount { get; set; }
        
        public int ContactsCount { get; set; }
    }
}