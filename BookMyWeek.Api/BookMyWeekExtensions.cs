using Microsoft.Extensions.FileProviders;

namespace BookMyWeek.Application;

public static class BookMyWeekExtensions
{
    public static IApplicationBuilder UseBookMyWeek(this IApplicationBuilder applicationBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var lastIndexOf = currentDirectory.LastIndexOf("BookMyWeek", StringComparison.Ordinal);
        var brokerPath =currentDirectory[..lastIndexOf];
        applicationBuilder.UseStaticFiles(new StaticFileOptions()  
        {  
            FileProvider = new PhysicalFileProvider(
                Path.Combine(brokerPath, "StaticFiles")),  
            RequestPath = "/book-my-week",  
        }) ;
        return applicationBuilder;
    }
}