using BethanyPieShop.Models;
using BethanyPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanyPieShop.Controllers
{
  public class PieController : Controller
  {
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
    {
      _pieRepository = pieRepository;
      _categoryRepository = categoryRepository;
    }

    //Action methods that will handle incoming requests

    public ViewResult List()
    {
      //ViewBag.CurrentCategory = "Cheesecakes";
      PiesListViewModel piesListViewModel = new PiesListViewModel();
      piesListViewModel.Pies = _pieRepository.AllPies;
      piesListViewModel.CurrentCategory = "Cheese cakes";
      return View(piesListViewModel);
    }

    public IActionResult Details(int id)
    {
      var pie = _pieRepository.GetPieById(id);
      if (pie == null)
        return NotFound();
      return View(pie);
    }
  }
}
