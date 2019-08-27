using AutoMapper;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.DTO;

namespace Core3_Framework.Contracts.Mapping
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<Users, UserDTO>();
            CreateMap<UserDTO, Users>();

            CreateMap<Products, ProductDTO>();
            CreateMap<ProductDTO, Products>();

            CreateMap<Categories, CategoryDTO>();
            CreateMap<CategoryDTO,Categories>();
        }
    }
}
