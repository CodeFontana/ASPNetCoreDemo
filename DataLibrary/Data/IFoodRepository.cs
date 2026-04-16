using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Data;

public interface IFoodRepository
{
    Task<IEnumerable<FoodModel>> GetFoodAsync();
    Task<int> CreateFoodAsync(FoodModel food);
    Task<int> UpdateFoodAsync(FoodModel food);
    Task<int> DeleteFoodAsync(int foodId);
}
