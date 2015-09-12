using System;
using System.IO;
using LibGdNet;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;

namespace LibGdAspNet5
{
    public class Startup
    {
        public Startup(ILibraryManager libraryManager)
        {
            if (PlatformApis.IsWindows())
            {
                var library = libraryManager.GetLibrary("LibGdAspNet5");

                string libraryPath;
                if (library.Type == "Project")
                {
                    libraryPath = Path.GetDirectoryName(library.Path);
                }
                else
                {
                    libraryPath = library.Path;
                }

                libraryPath = Path.Combine(libraryPath, "native", "windows", "x86");
                LibGd.LoadWindows(libraryPath);
            }
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseErrorPage();

            app.Run(async (context) =>
            {
                IFormFile fullImgFile = null;
                if (context.Request.HasFormContentType)
                {
                    fullImgFile = context.Request?.Form?.Files?.GetFile("fieldNameHere");
                }
                if (fullImgFile != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (var fullImgStream = fullImgFile.OpenReadStream())
                        {
                            await fullImgStream.CopyToAsync(memoryStream);
                        }

                        var fullImgBytes = memoryStream.ToArray();
                        var thumbnailBytes = PngThumbnailer.CreateThumbnail(fullImgBytes);

                        context.Response.Headers["Content-Type"] = "img/png";
                        context.Response.Headers["Content-Length"] = thumbnailBytes.Length.ToString();

                        await context.Response.Body.WriteAsync(thumbnailBytes, 0, thumbnailBytes.Length);
                    }
                }
                else
                {
                    await context.Response.WriteAsync("Hello World!");
                }
            });
        }
    }
}
