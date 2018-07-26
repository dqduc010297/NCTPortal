using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.EntityFramework
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder builder);
    }
}
