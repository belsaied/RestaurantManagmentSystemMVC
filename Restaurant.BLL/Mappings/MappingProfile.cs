using AutoMapper;
using Restaurant.BLL.DTOs.IngredientModule;
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
            #endregion

        }
    }
}
