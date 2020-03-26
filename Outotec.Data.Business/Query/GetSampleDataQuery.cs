using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Outotec.Data.Business.Query
{
    public class GetSampleDataQuery : IRequest<List<string>>
    {
        
    }

    public class GetSampleDataQueryHandler : IRequestHandler<GetSampleDataQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetSampleDataQuery request, CancellationToken cancellationToken)
        {
            // Get your data and do your business logic here.
            return new List<string> { "test" };
        }
    }
}
