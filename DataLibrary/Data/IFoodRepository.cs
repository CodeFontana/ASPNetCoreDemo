using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Data;

public interface IFoodRepository
{
    Task<IEnumerable<FoodModel>> GetFoodAsync();
}