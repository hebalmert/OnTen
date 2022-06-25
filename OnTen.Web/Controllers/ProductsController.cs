using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnTen.Common.Entities;
using OnTen.Web.Data;
using OnTen.Web.Data.Entities;
using OnTen.Web.Helper;
using OnTen.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTen.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public ProductsController(DataContext context,
            IImageHelper imageHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.Qualifications);

            return View(await dataContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(c => c.Category)
                .Include(c => c.ProductImages)
                .Include(c => c.Qualifications)
                .ThenInclude(q => q.User)

                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            AddProductImageViewModel model = new AddProductImageViewModel
            {
                ProductId = product.ProductId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddProductImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = await _context.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefaultAsync(p => p.ProductId == model.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                try
                {
                    //Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");

                    string imageId = string.Empty;

                    if (model.ImageFile != null)
                    {
                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        string ruta = "wwwroot\\Products";
                        imageId = await _imageHelper.UploadImage(model.ImageFile, ruta, guid);

                        if (product.ProductImages == null)
                        {
                            product.ProductImages = new List<ProductImage>();
                        }
                        product.ProductImages.Add(new ProductImage { ImageId = imageId });
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }

                    //return RedirectToAction($"{nameof(Details)}/{product.Id}");
                    return RedirectToAction("Details", new { id = product.ProductId });


                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImage productImage = await _context.ProductImages
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            if (productImage.ImageId != null)
            {
                string ruta = "wwwroot\\Products";
                var guid = productImage.ImageId;
                var response = _imageHelper.DeleteImage(ruta, guid);
                if (response != true)
                {
                    return NotFound();
                }
            }

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.ProductImages.FirstOrDefault(pi => pi.ProductImageId == productImage.ProductImageId) != null);
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();

            //return RedirectToAction($"{nameof(Details)}/{product.Id}");
            return RedirectToAction("Details", new { id = product.ProductId });
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            ProductViewModel model = new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                IsActive = true,
                Name = "Pedro Prueba"
            };


            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = await _converterHelper.ToProductAsync(model, true);
                    string imageId = string.Empty;

                    if (model.ImageFile != null)
                    {
                        //string imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        string ruta = "wwwroot\\Products";
                        imageId = await _imageHelper.UploadImage(model.ImageFile, ruta, guid);

                        product.ProductImages = new List<ProductImage>
                            {
                                new ProductImage { ImageId = imageId }
                            };
                    }

                    _context.Add(product);
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

            model.Categories = _combosHelper.GetComboCategories();

            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel model = _converterHelper.ToProductViewModel(product);

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = await _converterHelper.ToProductAsync(model, false);

                    string imageId = string.Empty;

                    if (model.ImageFile != null)
                    {
                        string guid = Guid.NewGuid().ToString() + ".jpg";
                        string ruta = "wwwroot\\Products";
                        imageId = await _imageHelper.UploadImage(model.ImageFile, ruta, guid);

                        if (product.ProductImages == null)
                        {
                            product.ProductImages = new List<ProductImage>();
                        }
                        product.ProductImages.Add(new ProductImage { ImageId = imageId });

                    }

                    _context.Update(product);
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

            model.Categories = _combosHelper.GetComboCategories();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var DataRemove = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (DataRemove == null)
            {
                return NotFound();
            }

            try
            {
                _context.Products.Remove(DataRemove);
                await _context.SaveChangesAsync();

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
