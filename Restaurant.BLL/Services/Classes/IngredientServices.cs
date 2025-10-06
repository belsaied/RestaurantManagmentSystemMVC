using AutoMapper;
using Restaurant.BLL.AttachmentService;
using Restaurant.BLL.DTOs.IngredientModule;
using Restaurant.BLL.Services.Interfaces;
using Restaurant.DAL.Data.Repositories.Classes;
using Restaurant.DAL.Data.Repositories.Interfaces;
using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services.Classes
{
    public class IngredientServices(IUnitOfWork _unitOfWork, IMapper _mapper,IAttachmentService _attachmentService) : IIngredientServices
    {
        public int AddIngredient(CreateIngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<CreateIngredientDto,Ingredient>(ingredientDto);
            if(ingredientDto.Image != null)
                  ingredient.ImageName = _attachmentService.Upload(ingredientDto.Image, "Images");

            _unitOfWork.IngredientRepository.Add(ingredient);
            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<IngredientDto> GetAllIngredients(bool withTracking = false)
        {
            var ingredients= _unitOfWork.IngredientRepository.GetAll(withTracking).Where(I=>I.IsDeleted==false);   

            return _mapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);
        }

        public IngredientDetailsDto? GetIngredientById(int id)
        {
            var ingredient= _unitOfWork.IngredientRepository.GetById(id);
            return _mapper.Map<Ingredient?, IngredientDetailsDto?>(ingredient);
        }

        public bool RemoveIngredient(int id)
        {
            var ingredient= _unitOfWork.IngredientRepository.GetById(id);
            if (ingredient == null) return false;
            ingredient.IsDeleted = true;
            _unitOfWork.IngredientRepository.Update(ingredient);
            int numberOfRows = _unitOfWork.SaveChanges();
            return numberOfRows > 0 ? true : false;

        }

        public int UpdateIngredient(IngredientDto ingredientDto)
        {
           _unitOfWork.IngredientRepository.Update(_mapper.Map<IngredientDto, Ingredient>(ingredientDto));
            return _unitOfWork.SaveChanges();
        }

       
    }
}
