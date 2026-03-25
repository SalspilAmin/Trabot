using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using MockQueryable;
using MockQueryable.Moq;
using MockQueryable.EntityFrameworkCore; // تأكد من إضافة هذا الـ Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tradify.Core.Features.User.Queries.Handlers;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Mapping.UserMapping;
using Tradify.Core.Resources.Service;
using Xunit;

namespace Tradify.XUnitTest.CoreTest.User.Queries
{
    public class UserQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<UserManager<Tradify.Data.Entities.Identity.User>> _userManagerMock;
        private readonly Mock<LocalizationService> _localizeMock;
        private readonly UserProfile _userProfile;

        public UserQueryHandlerTest()
        {
            // إعداد الـ Dependencies اللازمة للـ UserManager
            var store = new Mock<IUserStore<Tradify.Data.Entities.Identity.User>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var passwordHasher = new Mock<IPasswordHasher<Tradify.Data.Entities.Identity.User>>();
            var userValidators = new List<IUserValidator<Tradify.Data.Entities.Identity.User>>();
            var passwordValidators = new List<IPasswordValidator<Tradify.Data.Entities.Identity.User>>();
            var keyNormalizer = new Mock<ILookupNormalizer>();
            var errors = new IdentityErrorDescriber();
            var logger = new Mock<ILogger<UserManager<Tradify.Data.Entities.Identity.User>>>();
            var services = new Mock<IServiceProvider>();

            _userManagerMock = new Mock<UserManager<Tradify.Data.Entities.Identity.User>>(
                store.Object,
                options.Object,
                passwordHasher.Object,
                userValidators,
                passwordValidators,
                keyNormalizer.Object,
                errors,
                services.Object,
                logger.Object
            );

            // إعداد LocalizationService
            var stringLocalizerFactory = new Mock<IStringLocalizerFactory>();
            _localizeMock = new Mock<LocalizationService>(stringLocalizerFactory.Object);

            // إعداد AutoMapper
            _userProfile = new UserProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(_userProfile));
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task User_Must_Not_Empty()
        {
            // 1. Arrange (الإعداد)
            var query = new GetUserPaginationQuery
            {
                PageSize = 5,
                PageNumber = 1
            };

            // بيانات وهمية للاختبار
            var usersList = new List<Tradify.Data.Entities.Identity.User>
            {
                new Tradify.Data.Entities.Identity.User { Id = 1, UserName = "test1" },
                new Tradify.Data.Entities.Identity.User { Id = 2, UserName = "test2" }
            }.BuildMock();

         
     
            var mockUsersQueryable = usersList.AsQueryable();

            // ربط الـ Mock بـ UserManager
            _userManagerMock.Setup(x => x.Users).Returns(mockUsersQueryable);

            var handler = new UserQueryHandler(_localizeMock.Object, _userManagerMock.Object, _mapper);

            // 2. Act 
            var result = await handler.Handle(query, CancellationToken.None);

            // 3. Assert
            Assert.NotNull(result);
           
             Assert.Equal(2, result.Data.Count); 
        }


    }
}