using AutoMapper;
using Restaurant.BLL.DTOs.CustomerDTOs;
using Restaurant.BLL.DTOs.IngredientModule;
using Restaurant.BLL.DTOs.OrderDTO;
using Restaurant.BLL.DTOs.OrderDTOs;
using Restaurant.BLL.DTOs.OrderItemsModule;
using Restaurant.BLL.DTOs.PaymentModule;
using Restaurant.BLL.DTOs.TableModule;
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
            CreateMap<Ingredient,IngredientDetailsDto>()
                .ForMember(dest=>dest.Image,options=>options.MapFrom(src=>src.ImageName));
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
            //CreateMap<CreateRecipeDto, RecipeLine>();
            //CreateMap<RecipeLine, RecipeDto>()
            // .ForMember(dest=>dest.IngredientName,options=>options.MapFrom(src=>src.Ingredient.Name))
            // .ForMember(dest=>dest.MenuItemName,options=>options.MapFrom(src=>src.MenuItem.ItemName))
            // .ReverseMap();
            //CreateMap<RecipeLine, RecipesDetailsDto>()
            //    .ForMember(dest => dest.Ingredient, options => options.MapFrom(src => src.Ingredient.Name))
            // .ForMember(dest => dest.MenuItem, options => options.MapFrom(src => src.MenuItem.ItemName));
            //CreateMap<UpdatedRecipeDto, RecipeLine>();

            #region RecipeLines
            CreateMap<CreateRecipeLineDto, RecipeLine>();

            CreateMap<RecipeLine, RecipeLineDto>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.ItemName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.IsDeleted));

            CreateMap<RecipeLine, RecipeLineDetailsDto>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.ItemName));

            CreateMap<RecipeLine, RecipeLineSelectDto>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.ItemName));

            CreateMap<UpdateRecipeLineDto, RecipeLine>();
            #endregion
            #endregion
            #region OrderItems
            CreateMap<OrderItems, OrderItemDto>();
            CreateMap<OrderItems, OrderItemsDetailsDto>();
            CreateMap<CreatedOrderItems, OrderItems>();
            CreateMap<UpdatedOrderItems, OrderItems>();
            #endregion
            #region Payment
            CreateMap<Payment, PaymentDto>();
            CreateMap<Payment, PaymentDetailsDto>();
            CreateMap<CreatedPaymentDto, Payment>();
            CreateMap<UpdatedPaymentDto, Payment>();

            #endregion
            #region Table
            CreateMap<Table, TableDto>();
            CreateMap<Table, TabelDetailsDto>();
            CreateMap<CreatedTableDto, Table>();
            CreateMap<UpdatedTableDto, Table>();
            #endregion
        }
    }
}
