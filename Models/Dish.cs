using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish 
    {
    [Key]
    public int DishId {get;set;}
    [Required(ErrorMessage="Please name your dish!")]
    public string Name{get;set;}
    [Required(ErrorMessage="Please enter Chef's name!")]
    public string Chef{get;set;}
    [Required(ErrorMessage="Please Rank Tastiness!")]
    public int Tastiness{get;set;}
    [Required(ErrorMessage="Please enter calories!")]
    [Range(1,Int32.MaxValue,ErrorMessage="Calories out of acceptable range!")]
    public int Calories{get;set;}
    [Required(ErrorMessage="Please describe the dish!")]
    public string Description {get;set;}
    public DateTime CreatedAt {get;set;}=DateTime.Now;
    public DateTime UpdatedAT {get; set;}=DateTime.Now;
    }
}