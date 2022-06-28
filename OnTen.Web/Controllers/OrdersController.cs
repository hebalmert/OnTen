using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnTen.Common.Enum;
using OnTen.Web.Data;
using OnTen.Web.Data.Entities;
using OnTen.Web.Helper;
using OnTen.Web.Models;

namespace OnTen.Web.Controllers
{
    [Authorize(Roles = "Admin")]


    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public OrdersController(DataContext context,
            ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders
                .Include(p => p.User)
                .Include(p => p.OrderDetails)
                .ToListAsync());

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .ThenInclude(u => u.City)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(od => od.Category)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(od => od.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,OrderStatus,DateSent,DateConfirmed,Remarks,PaymentMethod")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ChangeOrderStatusViewModel modelo = new()
            {
                Id = order.Id,
                Date = DateTime.Now,
                OrderStatuses = _combosHelper.GetOrderStatuses(),
                OrderStatusId = (int)order.OrderStatus
            };
            return View(modelo);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChangeOrderStatusViewModel model)
        {

            if (ModelState.IsValid)
            {
                Order order = await _context.Orders.FindAsync(model.Id);
                order.OrderStatus = ToOrderStatus(model.OrderStatusId);
                if (model.OrderStatusId == 2) // sent
                {
                    order.DateSent = model.Date.ToUniversalTime();
                }
                else if (model.OrderStatusId == 3) // confirmed
                {
                    order.DateConfirmed = model.Date.ToUniversalTime();
                }

                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction($"{nameof(Details)}/{model.Id}");

            }

            model.OrderStatuses = _combosHelper.GetOrderStatuses();
            return View(model);
        }

        private OrderStatus ToOrderStatus(int orderStatusId)
        {
            switch (orderStatusId)
            {
                case 0: return OrderStatus.Pending;
                case 1: return OrderStatus.Spreading;
                case 2: return OrderStatus.Sent;
                case 3: return OrderStatus.Confirmed;
                default: return OrderStatus.Cancelled;
            }

        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
