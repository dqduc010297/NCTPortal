using System;
using System.Collections.Generic;
using System.Text;

namespace Groove.AspNetCore.Domain.Entities
{
    public interface IEntity
    {
    }
    public interface IEntity<TType> : IEntity
    {
        /// <summary>
        /// Identity key for the entities
        /// </summary>
        TType Id { get; set; }
    }
}
