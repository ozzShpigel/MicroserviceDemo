using CustomerApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}
