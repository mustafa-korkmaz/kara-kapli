
using Common.Response;
using Dto;
using System.Collections.Generic;

namespace Business
{
    public interface ICrudBusiness<TDto>
       where TDto : DtoBase
    {
        /// <summary>
        /// creates new entity from given dto
        /// </summary>
        /// <param name="dto"></param>
        ResponseBase Add(TDto dto);

        /// <summary>
        /// creates new entities as bulk insert from given dto list
        /// </summary>
        /// <param name="dtoList"></param>
        ResponseBase AddRange(IEnumerable<TDto> dtoList);

        /// <summary>
        /// updates given entity and returns affected row count.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>affected row count in db</returns>
        DataResponse<int> Edit(TDto dto);

        /// <summary>
        /// hard deletes entity by given id
        /// </summary>
        /// <param name="id"></param>
        ResponseBase Delete(int id);

        /// <summary>
        /// soft deletes entity
        /// </summary>
        /// <param name="id"></param>
        ResponseBase SoftDelete(int id);

        /// <summary>
        /// returns dto object by given id
        /// </summary>
        /// <param name="id"></param>
        DataResponse<TDto> Get(int id);

        /// <summary>
        /// returns all dto objects
        /// </summary>
        DataResponse<IEnumerable<TDto>> GetAll();
    }
}
