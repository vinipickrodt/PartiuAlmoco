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
        /// Busca a votação por data. Incluindo os ultimos 1000 votos e os ultimos 30 resultados.
        /// </summary>
        /// <param name="date">Data em que ocorreu a votação.</param>
        /// <returns>Return a votação.</returns>
        RestaurantPoll GetPollByDate(DateTime date); 
    }
}
