﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public IntegrationBaseEvent(Guid id, DateTime creationDate) 
        {
            Id = id;
            CreationDate = creationDate;
        }

        // This will act as a correlation id
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
