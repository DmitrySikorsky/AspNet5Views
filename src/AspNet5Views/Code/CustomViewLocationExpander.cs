// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Razor;

namespace AspNet5Views
{
  public class CustomViewLocationExpander : IViewLocationExpander
  {
    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
      List<string> expandedViewLocations = new List<string>();

      expandedViewLocations.AddRange(viewLocations);
      expandedViewLocations.Add("/Views/SomeExtraFolder/{1}/{0}.cshtml");
      return expandedViewLocations;
    }

    public void PopulateValues(ViewLocationExpanderContext context)
    {
    }
  }
}