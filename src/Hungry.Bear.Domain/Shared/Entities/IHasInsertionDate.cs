using System;

namespace Hungry.Bear.Domain.Shared.Entities
{
    public interface IHasInsertionDate
    {
        DateTime InsertionDate { get; set; }
    }
}