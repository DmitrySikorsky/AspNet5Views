// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Mvc;

namespace AspNet5Views.Controllers
{
  public class PrecompiledViewsController : Controller
  {
    public ActionResult Index()
    {
      return this.View();
    }
  }
}