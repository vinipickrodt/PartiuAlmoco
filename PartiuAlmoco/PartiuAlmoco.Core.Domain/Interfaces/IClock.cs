using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    /// <summary>
    /// Objetivo desta interface é permitir a independência do codigo do DateTime.Now,
    /// para facilitar os testes.
    /// </summary>
    public interface IClock
    {
        DateTime Now { get; }
    }
}
