using AutoMapper;
using Domain.Entities;
using GravityDAL;
using GravityDAL.Implementations;
using GravityServices.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GravityWebTest
{
    public class UsefulLinksRepositoryFixture
    {
        private static IList<UsefulLink> _usefulLinks;
        private static Mock<IMapper> _mapperMoq;
        static long[] IdNumbers =
        {
            1L, 2L, 3L, 4L
        };

        [SetUp]
        public void Setup()
        {
            _usefulLinks = new List<UsefulLink>
            {
                new UsefulLink{ Id=1L, Description="some description 1", Link="https://amdaris.com/"},
                new UsefulLink{ Id=2L, Description="some description 2", Link="https://amdaris.com/"},
                new UsefulLink{ Id=3L, Description="some description 3", Link="https://amdaris.com/"},
                new UsefulLink{ Id=4L, Description="some description 4", Link="https://amdaris.com/"}
            };

            _mapperMoq = new Mock<IMapper>();
        }

        [Test]
        public async Task WhenGetAll_ShouldReturnListOfUsefulLink()
        {
            //arange 
            var options = new DbContextOptionsBuilder<GravityGymDbContext>()
                .UseInMemoryDatabase(databaseName: "Get_UsefulLinks")
                .Options;

            using (var ctx = new GravityGymDbContext(options))
            {
                ctx.UsefulLinks.AddRange(_usefulLinks);
                ctx.SaveChanges();
            }

            using (var ctx = new GravityGymDbContext(options))
            {
                var usefulLinkRepo = new UsefulLinksRepository(ctx, _mapperMoq.Object);


            //act
                var result = await usefulLinkRepo.GetAllAsync();


            //assert
                            
                Assert.IsInstanceOf(typeof(IList<UsefulLink>),result);

            }
        }

        [Test]
        public async Task WhenAdd_NumberOfRecordsShouldIncrease()
        {
            //arange 
            var options = new DbContextOptionsBuilder<GravityGymDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_UsefulLinks")
                .Options;

            using (var ctx = new GravityGymDbContext(options))
            {
                ctx.UsefulLinks.AddRange(_usefulLinks);
                ctx.SaveChanges();
            }

            using (var ctx = new GravityGymDbContext(options))
            {
                var usefulLinkRepo = new UsefulLinksRepository(ctx, _mapperMoq.Object);


                var ul = new UsefulLink { Id = 5L, Description = "some description 5", Link = "https://amdaris.com/" };

                //act
                await usefulLinkRepo.AddAsync(ul);
                var result = await usefulLinkRepo.GetAllAsync();

                //assert
                Assert.Greater(result.Count,_usefulLinks.Count);
                

            }
        }

        [Test]
        public async Task WhenDelete_NumberOfRecordsShouldDecrease()
        {
            //arange 
            var options = new DbContextOptionsBuilder<GravityGymDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_UsefulLinks")
                .Options;

            using (var ctx = new GravityGymDbContext(options))
            {
                ctx.UsefulLinks.AddRange(_usefulLinks);
                ctx.SaveChanges();
            }

            using (var ctx = new GravityGymDbContext(options))
            {
                var usefulLinkRepo = new UsefulLinksRepository(ctx, _mapperMoq.Object);
                                               

                //act
                await usefulLinkRepo.DeleteAsync(_usefulLinks[2]);
                await usefulLinkRepo.DeleteAsync(2);

                var result = await usefulLinkRepo.GetAllAsync();

                //assert
                Assert.Less(result.Count, _usefulLinks.Count);


            }
        }
        [Test]        
        public async Task WhenGetById_ShouldReturnSpecificUsefulLink()
        {
            //arange 
            var options = new DbContextOptionsBuilder<GravityGymDbContext>()
                .UseInMemoryDatabase(databaseName: "GetWithId_UsefulLinks")
                .Options;

            using (var ctx = new GravityGymDbContext(options))
            {
                ctx.UsefulLinks.AddRange(_usefulLinks);
                ctx.SaveChanges();
            }


            using (var ctx = new GravityGymDbContext(options))
            {
                var usefulLinkRepo = new UsefulLinksRepository(ctx, _mapperMoq.Object);


                //act
                var result = await usefulLinkRepo.GetByIdAsync(2L);

                
                //assert
                var actual = _usefulLinks.Where(ul => ul.Id == 2L).FirstOrDefault();

                Assert.AreEqual(result.Id, actual.Id);
                Assert.AreEqual(result.Link, actual.Link);
                Assert.AreEqual(result.Description, actual.Description);

            }

        }

        [Test]
        public async Task WhenUpdateUL_ShouldBeUpdated()
        {
            //arange 
            var options = new DbContextOptionsBuilder<GravityGymDbContext>()
                .UseInMemoryDatabase(databaseName: "UpdateWithId_UsefulLinks")
                .Options;

            using (var ctx = new GravityGymDbContext(options))
            {
                ctx.UsefulLinks.AddRange(_usefulLinks);
                ctx.SaveChanges();
            }


            using (var ctx = new GravityGymDbContext(options))
            {
                var usefulLinkRepo = new UsefulLinksRepository(ctx, _mapperMoq.Object);


                //act
                var ul = await usefulLinkRepo.GetByIdAsync(2L);
                ul.Description = "NUnit Test";
                ul.Link = "https://amdariss.com/";
                await usefulLinkRepo.UpdateAsync(ul);

                var result = await usefulLinkRepo.GetByIdAsync(2L);



                //assert
                Assert.AreEqual(result.Id, ul.Id);
                Assert.AreEqual(result.Link, ul.Link);
                Assert.AreEqual(result.Description, ul.Description);


            }

        }
    }
}