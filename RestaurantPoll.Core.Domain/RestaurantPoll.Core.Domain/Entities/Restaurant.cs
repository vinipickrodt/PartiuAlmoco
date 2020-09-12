using Ardalis.GuardClauses;
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
        /// Site do restaurante.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Telefone do restaurante.
        /// </summary>
        public string Phone { get; set; }

        public Restaurant(string name, string website, string phone)
        {
            Name = name;
            Website = website;
            Phone = phone;
        }
    }
}
