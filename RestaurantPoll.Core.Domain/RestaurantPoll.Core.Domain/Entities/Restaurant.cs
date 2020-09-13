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

        public Restaurant(Guid id, string name, string website, string phone)
        {
            Guard.Against.NullOrEmpty(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            if (string.IsNullOrWhiteSpace(website) && string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("A restaurant must have at least a website or a phone.");
            }

            Id = id;
            Name = name;
            Website = website;
            Phone = phone;
        }
    }
}
