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
        RestaurantPoll GetPollById(Guid id);

        /// <summary>
        /// Busca a votação por data. Incluindo todos os votos e os ultimos 100 resultados das ultimas votações.
        /// </summary>
        /// <param name="date">Data em que ocorreu a votação.</param>
        /// <returns>Retorna a votação.</returns>
        RestaurantPoll GetPollByDate(DateTime date);

        /// <summary>
        /// Cria uma nova votação.
        /// </summary>
        /// <param name="name">Nome da votação</param>
        /// <param name="date">Data da votação</param>
        /// <returns>Retorna a votação</returns>
        RestaurantPoll NewPoll(string name, DateTime date);

        /// <summary>
        /// Confirma as operações realizadas como sendo válidas e podem ser gravadas.
        /// </summary>
        /// <param name="poll">Votação</param>
        void Confirm(RestaurantPoll poll);

        void Add(RestaurantPoll poll);

        /// <summary>
        /// Busca as votações que não tiverem seus resultados computados até a data.
        /// </summary>
        /// <param name="date">Data</param>
        /// <returns>Retorna todas as votações que não tiveram seus resultados computados até a data.</returns>
        IEnumerable<RestaurantPoll> GetUnfinishedPolls(DateTime date);

        /// <summary>
        /// Adiciona o resultado da votação.
        /// </summary>
        /// <param name="result">resultado da votação</param>
        void AddPollResult(RestaurantPollResult result);
    }
}
