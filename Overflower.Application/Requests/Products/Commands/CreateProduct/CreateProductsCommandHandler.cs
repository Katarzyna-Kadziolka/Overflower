using MapsterMapper;
using MediatR;
using Overflower.Persistence;
using Overflower.Persistence.Entities.Products;

namespace Overflower.Application.Requests.Products.Commands.CreateProduct; 

public class CreateProductsCommandHandler : IRequestHandler<CreateProductCommand, ProductDto> {
	private readonly ApplicationDbContext _context;
	private readonly IMapper _mapper;

	public CreateProductsCommandHandler(ApplicationDbContext context, IMapper mapper) {
		_context = context;
		_mapper = mapper;
	}

	public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
		var product = _mapper.Map<ProductEntity>(request);
		product.Id = Guid.NewGuid();
		_context.Products.Add(product);
		await _context.SaveChangesAsync(cancellationToken);
		var productDto = _mapper.Map<ProductDto>(product);
		return productDto;
	}
}