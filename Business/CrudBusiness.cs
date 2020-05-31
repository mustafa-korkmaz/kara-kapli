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

        /// <summary>
        /// to avoid IDOR attacks checks whether userId of entity and the ApplicationUser.Id are the same
        /// </summary>
        protected bool ValidateEntityOwner;

        protected CrudBusiness(IUnitOfWork uow, ILogger logger, IMapper mapper)
        {
            Uow = uow;
            Repository = Uow.Repository<TRepository, TEntity>();
            Logger = logger;
            Mapper = mapper;
        }

        public virtual Response Add(TDto dto)
        {
            var entity = Mapper.Map<TDto, TEntity>(dto);

            Repository.Insert(entity);

            Uow.Save();

            dto.Id = entity.Id;

            return new Response
            {
                Type = ResponseType.Success
            };
        }

        public virtual Response AddRange(TDto[] dtoList)
        {
            var entities = Mapper.Map<TDto[], TEntity[]>(dtoList);

            Repository.InsertRange(entities);

            Uow.Save();

            for (int i = 0; i < dtoList.Length; i++)
            {
                dtoList[i].Id = entities[i].Id;
            }

            return new Response
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

            if (ValidateEntityOwner)
            {
                //client wants to check for an IDOR attack
                if (!IsEntityOwnerValid(entity))
                {
                    businessResp.ErrorCode = ErrorCode.NotAuthorized;
                    return businessResp;
                }
            }

            var type = typeof(TEntity);
            var entityProperties = type.GetProperties();

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                //Get CreatedAt property value from entity.
                if (entityProperty.Name == "CreatedAt")
                {
                    PropertyInfo dtoProperty = typeof(TDto).GetProperty(entityProperty.Name); //POCO obj must have same prop as model

                    var value = entityProperty.GetValue(entity); //get value of entity

                    dtoProperty.SetValue(dto, value, null); //set dto.CreatedAt as entity.CreatedAt

                    break;
                }
            }

            entity = Mapper.Map<TDto, TEntity>(dto);

            Repository.Update(entity);

            var affectedRows = Uow.Save();

            businessResp.Data = affectedRows;
            businessResp.Type = ResponseType.Success;
            return businessResp;
        }

        public virtual Response Delete(int id)
        {
            var resp = new Response
            {
                Type = ResponseType.Fail
            };

            var entity = Repository.GetById(id);

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

        public virtual Response SoftDelete(int id)
        {
            var resp = new Response
            {
                Type = ResponseType.Fail
            };

            var entity = Repository.GetById(id);

            bool updated = false;

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

            return new Response
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

            var entity = Repository.GetById(id);

            if (entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var dto = Mapper.Map<TEntity, TDto>(entity);

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

        protected virtual bool IsEntityOwnerValid(TEntity entity)
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
