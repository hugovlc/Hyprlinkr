﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ploeh.Hyprlinkr;

namespace Ploeh.Samples.Hyprlinkr.ExampleService
{
    public class HomeController : ApiController
    {
        private readonly RouteLinker linker;

        public HomeController(RouteLinker linker)
        {
            this.linker = linker;
        }

        public HomeModelCollection Get()
        {
            return new HomeModelCollection
            {
                Links = new[]
                {
                    new AtomLinkModel
                    {
                        Href = this.linker.GetUri<HomeController>(r =>
                            r.Get()).ToString(),
                        Rel = "self"
                    },
                    new AtomLinkModel
                    {
                        Href = this.linker.GetUri<NoDIController>(r =>
                            r.Get("foo")).ToString(),
                        Rel = "http://sample.ploeh.dk/rels/no-di"
                    }
                },
                Homes = new[]
                {
                    new HomeModel
                    {
                        Name = "ploeh",
                        Links = new[]
                        {
                            new AtomLinkModel
                            {
                                Href = this.linker.GetUri<HomeController>(r =>
                                    r.Get("ploeh")).ToString(),
                                Rel = "http://sample.ploeh.dk/rels/specific-home"
                            }
                        }
                    },
                    new HomeModel
                    {
                        Name = "fnaah",
                        Links = new[]
                        {
                            new AtomLinkModel
                            {
                                Href = this.linker.GetUri<HomeController>(r =>
                                    r.Get("fnaah")).ToString(),
                                Rel = "http://sample.ploeh.dk/rels/specific-home"
                            }
                        }
                    }
                }
            };
        }

        [Route("custom/route/{id}", Name = "CustomRoute")]
        public HomeModel Get(string id)
        {
            return new HomeModel
            {
                Name = id,
                Links = new[]
                {
                    new AtomLinkModel
                    {
                        Href = this.linker.GetUri<HomeController>(r =>
                            r.Get(id)).ToString(),
                        Rel = "self"
                    },
                    new AtomLinkModel
                    {
                        Href = this.linker.GetUri<HomeController>(r =>
                            r.Get()).ToString(),
                        Rel = "http://sample.ploeh.dk/rels/home"
                    }
                }
            };
        }
    }
}