using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public interface IFoodData
    {
        Task<List<FoodModel>> GetFood();
    }
}