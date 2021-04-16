using Hsec.Application.Common.Interfaces;
using System;

namespace Hsec.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
