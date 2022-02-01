using System.Collections.Generic;
using invoice.app.Entity;
using invoice.app.ViewModels;

namespace invoice.app.Mappers;

public static class EntityModelMappers
{
    public static CreatedOrganizationsViewModel ToModel(this List<Organization> entity)
    {
        return new CreatedOrganizationsViewModel()
        {
            Organizations = entity.Select(i => 
            {
                return new OrganizationViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    EmployeeOrganizationsCount = i.EmployeeOrganizations.Count(),
                    InvoicesCount = i.EmployeeOrganizations.Count(),
                    ContactsCount = i.Contacts.Count()
                };
            }).ToList()
        };
    }
}