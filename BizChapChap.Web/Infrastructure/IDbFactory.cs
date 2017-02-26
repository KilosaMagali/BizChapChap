using BizChapChap.Web.Models;
using System;

namespace BizChapChap.Web.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ApplicationDbContext Init();
    }
}
