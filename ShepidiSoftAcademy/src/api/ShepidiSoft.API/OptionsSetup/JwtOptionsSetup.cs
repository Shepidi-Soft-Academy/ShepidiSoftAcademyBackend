using Microsoft.Extensions.Options;
using ShepidiSoft.Identity.Auths.Jwt;

namespace ShepidiSoft.API.OptionsSetup;

public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection("JWT").Bind(options);
    }
}
