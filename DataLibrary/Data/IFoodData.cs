using System.Collections.Generic;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Data;

public interface IFoodData
{
    Task<List<FoodModel>> GetFood();
}