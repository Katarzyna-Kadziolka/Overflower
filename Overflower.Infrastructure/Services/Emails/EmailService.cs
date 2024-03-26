using Overflower.Application.Services.Emails;

namespace Overflower.Infrastructure.Services.Emails; 

public class EmailService : IEmailService {
	public Task SendEmail() {
		return Task.CompletedTask;
	}
}