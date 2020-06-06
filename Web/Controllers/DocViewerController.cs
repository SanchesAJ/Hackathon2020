using cp2020.DAL;
using cp2020.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace cp2020.Controllers
{
    public class DocViewerController : Controller
    {
        private readonly ILogger<DocViewerController> _logger;
        private readonly DbRepository _repository;

        public DocViewerController(ILogger<DocViewerController> logger, DbRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index(Guid id)
        {
            ViewBag.Doc = _repository.GetDocById(id);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
