﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantPoll.Core.Domain.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
