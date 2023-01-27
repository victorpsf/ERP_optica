export class Login {
    Name = '';
    Key = '';
    Code = '';
    EnterpriseId = 0;
    Enterprises = [];

    setEnterpriseId () {
        this.EnterpriseId = (this.Enterprises.length > 0) ? this.Enterprises[0].id: 0;
    }

    toJSON() {
        return {
            Name: this.Name,
            Key: this.Key,
            Code: this.Code,
            EnterpriseId: this.EnterpriseId
        }
    }
}

export class ResendEmail {
    Name = '';
    EnterpriseId = '';
}