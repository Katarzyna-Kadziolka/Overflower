namespace Overflower.Api.Configuration.JsonSerilizer; 

// ReSharper disable once InconsistentNaming
public static class IMvcBuilderExtensions {
	public static IMvcBuilder AddJsonSerializer(this IMvcBuilder builder)
	{
		builder.AddNewtonsoftJson(options =>
		{
			options.SerializerSettings.AddJsonSettings();
		});
		return builder;
	}
}