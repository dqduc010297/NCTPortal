using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Domain.Entities
{
    public interface IVersionedEntity : IEntity
    {
        byte[] RowVersion { get; set; }
    }
    

    public interface IVersionedEntity<TKeyType> : IVersionedEntity, IEntity<TKeyType>
    {
    }
}
