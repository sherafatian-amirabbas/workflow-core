using System;

namespace WebApplication.DB
{
    public interface IStore
    {
        string this[Guid key] { get; }

        Guid Add(string description);
    }
}