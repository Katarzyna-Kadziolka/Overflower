using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Overflower.Persistence;

namespace Overflower.Application.Requests.Products.Queries.GetAllProducts; 

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductDto[]> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public GetAllProductsQueryHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}
	
	
	public async Task<ProductDto[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) {
		var products = await _context.Products.ToArrayAsync(cancellationToken);
		var productDtos = _mapper.Map<ProductDto[]>(products);
		return productDtos;
	}
}