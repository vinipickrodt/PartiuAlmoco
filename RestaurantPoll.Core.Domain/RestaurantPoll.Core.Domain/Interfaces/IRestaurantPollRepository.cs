using PartiuAlmoco.Core.Domain.Entities.RestaurantPollAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartiuAlmoco.Core.Domain.Interfaces
{
    public interface IRestaurantPollRepository
    {
        /// <summary>
        /// Busca a votação por data. Incluindo todos os votos e os ultimos 100 resultados das ultimas votações.
        /// </summary>
        /// <param name="date">Data em que ocorreu a votação.</param>
        /// <returns>Retorna a votação.</returns>
        RestaurantPoll GetPollByDate(DateTime date);
    }
}
