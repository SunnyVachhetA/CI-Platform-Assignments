using CI_SkillMaster.Utility.JwtUtil;

namespace CI_SkillMaster.Utility;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration _config)
    {
        //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var token = context.Request.Cookies["Token"];

        var jwtUtil = new JwtTokenUtil(_config);
        bool isUserValid = jwtUtil.ValidateToken(token);
        Console.WriteLine(token);
        await _next(context);
    }
}
