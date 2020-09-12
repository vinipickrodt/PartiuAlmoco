using System;
using System.Collections.Generic;
using System.Text;

namespace PartiuAlmoco.Core.Domain.Entities
{
    public class Restaurant : Entity
    {
        /// <summary>
        /// Nome do restaurante.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Custo médio de um almoço.
        /// </summary>
        public decimal AvgLunchCost { get; set; }

        /// <summary>
        /// Site do restaurante.
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// Telefone do restaurante.
        /// </summary>
        public string Phone { get; set; }
    }
}
