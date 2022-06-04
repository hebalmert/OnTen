using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnTen.Common.Entities;
using OnTen.Web.Data;
using OnTen.Web.Helper;
using OnTen.Web.Models;

namespace OnTen.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public CategoriesController(DataContext context,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {

            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            CategoryViewModel model = new();

            return View(model);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imageId = string.Empty;

                    if (model.ImageFile != null)
                    {
                        //Para Manejar Contenedores AZURE
                        //imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "categories");

                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        string ruta = "wwwroot\\Categories";
                        imageId = await _imageHelper.UploadImage(model.ImageFile, ruta, guid);
                        model.ImageId = imageId;
                    }
                    Category category = _converterHelper.ToCategory(model, imageId, true);

                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = _converterHelper.ToCategoryViewModel(category);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imageId = model.ImageId;

                    if (model.ImageFile != null)
                    {
                        string guid;

                        if (model.ImageId == null)
                        {
                            guid = Guid.NewGuid().ToString() + ".jpg";
                        }
                        else
                        {
                            guid = model.ImageId;
                        }
                        string ruta = "wwwroot\\Categories";
                        imageId = await _imageHelper.UploadImage(model.ImageFile, ruta, guid);
                        model.ImageId = imageId;
                    }


                    Category category = _converterHelper.ToCategory(model, imageId, false);
                    _context.Update(category);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DataRemove = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (DataRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categories.Remove(DataRemove);
                await _context.SaveChangesAsync();

                if (DataRemove.ImageId != null)
                {
                    string ruta = "wwwroot\\Categories";
                    var guid = DataRemove.ImageId;
                    var response = _imageHelper.DeleteImage(ruta, guid);
                    if (response != true)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "Can't delete, Exist Reference with other Register");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
