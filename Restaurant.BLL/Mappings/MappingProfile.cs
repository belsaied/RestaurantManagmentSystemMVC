using AutoMapper;
using Restaurant.BLL.DTOs.CustomerDTOs;
using Restaurant.BLL.DTOs.IngredientModule;
using Restaurant.BLL.DTOs.OrderDTO;
using Restaurant.BLL.DTOs.OrderDTOs;
using Restaurant.BLL.DTOs.RecipeLinesDtos;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //Create your mappings here
            //CreateMap<Source, Destination>();
            #region Ingredients
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<CreateIngredientDto, Ingredient>();
            #endregion
            #region Customers
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Customer,CustomerDetailsDTO>();
            CreateMap<CreateCustomerDTO,Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
            #endregion
            #region Orders
            CreateMap<Order, OrderDTO>();
            CreateMap<Order, OrderDetailsDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<UpdateOrderDTO,Order>();
            #endregion
            #region RecipeLines
            CreateMap<CreateRecipeDto, RecipeLine>();
            CreateMap<RecipeLine, RecipeDto>().ReverseMap();
            #endregion
        }
    }
}
