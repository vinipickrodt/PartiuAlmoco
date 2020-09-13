using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    /// <summary>
    /// Utilitário de para logar erros, avisos e informações.
    /// </summary>
    public interface ILog
    {
        void LogWarning(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogInformation(string message, params object[] args);
    }
}
