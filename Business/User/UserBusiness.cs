using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Dal;
using Dal.Repositories.User;

namespace Business.User
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserBusiness> _logger;


        public UserBusiness(IUnitOfWork uow, ILogger<UserBusiness> logger, IMapper mapper)
        {
            _uow = uow;
            _repository = _uow.Repository<IUserRepository, Dal.Entities.Identity.ApplicationUser>();
            _logger = logger;
            _mapper = mapper;
        }
    }
}
