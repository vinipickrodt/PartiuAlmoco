using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Infra.Domain
{
    public interface IAppSettings
    {
        string DatabaseConnectionString { get; }
    }
}
