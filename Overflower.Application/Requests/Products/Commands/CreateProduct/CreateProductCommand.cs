using MediatR;

namespace Overflower.Application.Requests.Products.Commands.CreateProduct; 

public class CreateProductCommand : IRequest<ProductDto> {
	public required string Name { get; set; }
}