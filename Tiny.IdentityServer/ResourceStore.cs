﻿using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiny.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new NotImplementedException();
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
