using Application.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test;

public partial class TestRepository: TestSql
{
    private readonly ITestDatabase database;

    public TestRepository(ITestDatabase database)
    {
        this.database = database;
    }
}
