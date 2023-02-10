using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test;

public class AuthenticationTest
{
    private readonly Services services;

    private AuthenticationTest(Configuration testConfiguration)
    {
        this.services = new Services(testConfiguration);
    }

    public string Execute()
    {
        return string.Empty;
    }

    public static AuthenticationTest Create(Configuration testConfiguration) => new AuthenticationTest(testConfiguration);
}
