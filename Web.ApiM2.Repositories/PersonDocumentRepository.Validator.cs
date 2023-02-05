using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Web.ApiM2.Repositories.Rules.PersonDocumentRules;

namespace Web.ApiM2.Repositories;

public partial class PersonDocumentRepository
{
    private void ValidateRule(CreatePersonDocumentRule business)
    {
        var documents = this.Find(new FindPersonDocumentRule
        {
            EnterpriseId = business.EnterpriseId,
            UserId = business.UserId,
            Input = new Controller.Models.PersonDocumentModels.ListPersonDocumentInput
            {
                PersonType = business.PersonType,
                PersonId = business.Input.PersonId,
                Type = business.Input.Type
            }
        });

        if (documents.Any()) throw new Exception("ERRO_DOCUMENTTYPE_EXISTS");
    }

    private void ValidateRule(ChangePersonDocumentRule business)
    {
        var documents = this.Find(new FindPersonDocumentRule
        {
            EnterpriseId = business.EnterpriseId,
            UserId = business.UserId,
            Input = new Controller.Models.PersonDocumentModels.ListPersonDocumentInput
            {
                PersonType = business.PersonType,
                PersonId = business.Input.PersonId,
                Type = business.Input.Type,
                Id = business.Input.Id
            }
        });

        if (!documents.Any()) throw new Exception("ERRO_DOCUMENT_NOT_EXISTS");
    }
}
