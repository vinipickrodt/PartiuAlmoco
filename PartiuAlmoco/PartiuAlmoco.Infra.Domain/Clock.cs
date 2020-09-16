using PartiuAlmoco.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Infra.Domain
{
    // TODO: Pesquisar a melhor forma de evitar o uso de DateTime.Now para facilitar os testes com data.
    public class Clock : IClock
    {
        public DateTime Now { get => DateTime.Now; }
    }
}
