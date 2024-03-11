using System;
using Microsoft.Extensions.Primitives;

namespace FirstApp
{
    public class DoAuthorization : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/" && context.Request.Method == "POST")
            {
                StreamReader reader = new StreamReader(context.Request.Body);
                string? body = await reader.ReadToEndAsync();

                //Parse the request body from string into Dictionary
                Dictionary<string, StringValues> queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
                if (queryDict["email"] == "email" && queryDict["Password"] == "password")
                {
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync("Valid Login");
                    await next(context);
                 }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid Credentials");
                    //await next(context);
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Please enter Credentials");
            }
        }

        private bool isValidLogin(Dictionary<string, StringValues> queryDict)
        {
            return queryDict["email"] == "email" && queryDict["Password"][0] == "password";
        }
    }
}

