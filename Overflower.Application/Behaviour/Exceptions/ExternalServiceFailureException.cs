using Overflower.Application.Behaviour.Exceptions.ErrorCode;

namespace Overflower.Application.Behaviour.Exceptions;

public class ExternalServiceFailureException : BaseApplicationException {
	public ExternalServiceFailureException() : this("External service failed", ErrorCodes.Default.ExternalServiceFailed) { }

	public ExternalServiceFailureException(string message) : this(message, ErrorCodes.Default.ExternalServiceFailed) { }
	public ExternalServiceFailureException(string message, string errorCode) : base(message, errorCode) { }

	public ExternalServiceFailureException(string message, Exception innerException) : base(message,
		ErrorCodes.Default.ExternalServiceFailed, innerException) { }
}