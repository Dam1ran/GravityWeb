using Domain.Entities;
using GravityDAL.Interfaces;
using GravityServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GravityServices.Implementations
{
    public class UsefulLinkService : IUsefulLinkService
    {
        private readonly IUsefulLinksRepository _usefulLinksRepository;

        public UsefulLinkService(IUsefulLinksRepository usefulLinksRepository)
        {
            _usefulLinksRepository = usefulLinksRepository;
        }
        public async Task<UsefulLink> SaveAsync(UsefulLink usefulLink)
        {
            var ul = new UsefulLink { Link = usefulLink.Link, Description = usefulLink.Description };

            return await _usefulLinksRepository.AddAsync(ul);
        }
    }
}
