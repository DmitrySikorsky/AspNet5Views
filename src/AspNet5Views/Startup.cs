// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace AspNet5Views
{
  public class Startup
  {
    private string applicationBasePath;

    public Startup(IApplicationEnvironment applicationEnvironment)
    {
      this.applicationBasePath = applicationEnvironment.ApplicationBasePath;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
        // This is to be able to resolve precompiled views from the specific assemblies
        .AddPrecompiledRazorViews(
          new Assembly[] { Assembly.Load(new AssemblyName("AspNet5Views.PrecompiledViews")) }
        );

      services.Configure<RazorViewEngineOptions>(options =>
        {
          // This is to be able to resolve views from current assembly but from non-standard location
          options.ViewLocationExpanders.Add(new CustomViewLocationExpander());
          // This is to be able to resolve views added as resources from the specific assemblies
          options.FileProvider = this.GetFileProvider(this.applicationBasePath);
        }
      );
    }

    public void Configure(IApplicationBuilder applicationBuilder)
    {
      // Next line is required to be able to use static files (like .css and .js ones)
      applicationBuilder.UseStaticFiles();
      applicationBuilder.UseMvcWithDefaultRoute();
    }

    public IFileProvider GetFileProvider(string path)
    {
      IEnumerable<IFileProvider> fileProviders = new IFileProvider[] { new PhysicalFileProvider(path) };

      return new CompositeFileProvider(
        fileProviders.Concat(
          new Assembly[] { Assembly.Load(new AssemblyName("AspNet5Views.ViewsAsResources")) }.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
        )
      );
    }
  }
}