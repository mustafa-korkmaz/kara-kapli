using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Common;
using Common.Response;
using Dal.Entities;
using Dto;
using System.Linq;
using Dal.Repositories;
using Microsoft.Extensions.Logging;
using Dal;
using System;

namespace Business
{
    /// <summary>
    /// Abstract class for basic create, update, delete and get operations.
    /// </summary>
    /// <typeparam name="TEntity">TEntity is db entity.</typeparam>
    /// <typeparam name="TDto">TDto is data transfer object.</typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public abstract class CrudBusiness<TRepository, TEntity, TDto> : ICrudBusiness<TDto>
        where TEntity : EntityBase
        where TDto : DtoBase
        where TRepository : IRepository<TEntity>
    {
        /// <summary>
        /// when we need to validate entity owner we will need this owner UserId property
        /// </summary>
        public Guid OwnerId { get; set; }

        protected readonly IUnitOfWork Uow;
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        protected readonly ILogger Logger;
        protected TEntity Entity;

        /// <summary>
        /// to avoid IDOR attacks checks userId of entity and the given dto are the same or not
        /// </summary>
        protected bool ValidateEntityOwner;

        protected CrudBusiness(IUnitOfWork uow, ILogger logger, IMapper mapper)
        {
            Uow = uow;
            Repository = Uow.Repository<TRepository, TEntity>();
            Logger = logger;
            Mapper = mapper;
        }

        public virtual ResponseBase Add(TDto dto)
        {
            var entity = Mapper.Map<TDto, TEntity>(dto);

            Repository.Insert(entity);

            Uow.Save();

            dto.Id = entity.Id;

            return new ResponseBase
            {
                Type = ResponseType.Success
            };
        }

        public virtual ResponseBase AddRange(IEnumerable<TDto> dtoList)
        {
            var entities = Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(dtoList);

            Repository.InsertRange(entities);

            Uow.Save();

            return new ResponseBase
            {
                Type = ResponseType.Success
            };
        }

        public virtual DataResponse<int> Edit(TDto dto)
        {
            var businessResp = new DataResponse<int>
            {
                Type = ResponseType.Fail
            };

            var entity = Repository.GetById(dto.Id);

            if (entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var type = typeof(TEntity);

            var entityProperties = type.GetProperties();

            if (ValidateEntityOwner)
            {
                //client wants to check for an IDOR attack
                if (!IsEntityOwnerValid(entity))
                {
                    businessResp.ErrorCode = ErrorCode.NotAuthorized;
                    return businessResp;
                }
            }

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                //Only modify settable properties. Do not change CreatedAt property.
                if (entityProperty.CanWrite && entityProperty.Name != "CreatedAt")
                {
                    PropertyInfo dtoProperty = typeof(TDto).GetProperty(entityProperty.Name); //POCO obj must have same prop as model

                    var value = dtoProperty.GetValue(dto); //get new value of entity from dto object

                    entityProperty.SetValue(entity, value, null); //set new value of entity
                }
            }

            Repository.Update(entity);

            var affectedRows = Uow.Save();

            businessResp.Data = affectedRows;
            businessResp.Type = ResponseType.Success;
            return businessResp;
        }

        public virtual ResponseBase Delete(int id)
        {
            var resp = new ResponseBase
            {
                Type = ResponseType.Fail
            };

            var entity = Entity;

            if (entity == null || entity.Id != id)
            {
                entity = Repository.GetById(id);
            }

            if (entity == null)
            {
                resp.ErrorCode = ErrorCode.RecordNotFound;
                return resp;
            }

            if (ValidateEntityOwner)
            {
                //client wants to check for an IDOR attack
                if (!IsEntityOwnerValid(entity))
                {
                    resp.ErrorCode = ErrorCode.NotAuthorized;
                    return resp;
                }
            }

            Repository.Delete(entity);

            Uow.Save();

            var type = typeof(TEntity);

            //log db record deletion as an info
            Logger.LogInformation($"'{type}' entity has been hard-deleted.");

            resp.Type = ResponseType.Success;

            return resp;
        }

        public virtual ResponseBase SoftDelete(int id)
        {
            var entity = Repository.GetById(id);

            bool updated = false;

            if (entity == null)
            {
                return new ResponseBase
                {
                    Type = ResponseType.Fail,
                    ErrorCode = ErrorCode.RecordNotFound
                };
            }

            var type = typeof(TEntity);

            var entityProperties = type.GetProperties();

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                //Only modify IsDeleted property. Do not change others
                if (entityProperty.CanWrite && entityProperty.Name == "IsDeleted")
                {
                    entityProperty.SetValue(entity, true, null); //soft deletion

                    updated = true;
                    break;
                }
            }

            if (updated)
            {
                Repository.Update(entity);
                Uow.Save();
            }

            //log db record modification as an info
            Logger.LogInformation($"'{type}' entity with ID: {id} has been modified.");

            return new ResponseBase
            {
                Type = ResponseType.Success
            };
        }

        public virtual DataResponse<TDto> Get(int id)
        {
            var businessResp = new DataResponse<TDto>
            {
                Type = ResponseType.Fail
            };

            Entity = Repository.GetById(id);

            if (Entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var dto = Mapper.Map<TEntity, TDto>(Entity);

            businessResp.Type = ResponseType.Success;
            businessResp.Data = dto;

            return businessResp;
        }

        public virtual DataResponse<IEnumerable<TDto>> GetAll()
        {
            var businessResp = new DataResponse<IEnumerable<TDto>>
            {
                Type = ResponseType.Fail
            };

            var entities = Repository.GetAll();

            if (!entities.Any())
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);

            businessResp.Type = ResponseType.Success;
            businessResp.Data = dtos;

            return businessResp;
        }

        private bool IsEntityOwnerValid(TEntity entity)
        {
            if (OwnerId == Guid.Empty)
            {
                throw new Exception("OwnerId cannot be empty");
            }

            var type = typeof(TEntity);

            var entityProperties = type.GetProperties();

            var userIdPropExists = entityProperties.Any(p => p.Name == "UserId");

            if (userIdPropExists)
            {
                PropertyInfo entityProperty = typeof(TEntity).GetProperty("UserId");

                var entityUserId = (Guid)entityProperty.GetValue(entity);

                return entityUserId.Equals(OwnerId);
            }

            return false;
        }
    }
}
