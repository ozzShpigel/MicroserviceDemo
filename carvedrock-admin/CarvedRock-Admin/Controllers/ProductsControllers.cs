using CarvedRock_Admin.Models;
using Microsoft.AspNetCore.Mvc;
using CarvedRock_Admin.Logic;

namespace CarvedRock_Admin.Controllers;

public class ProductsController: Controller{
    private readonly IProductLogic _logic;
    private readonly ILogger<ProductsController> _logger;

    //public List<ProductModel> Products { get; set; }

    public ProductsController(IProductLogic logic, ILogger<ProductsController> logger)
    {
        //Products = GetSampleProducts();
        _logic = logic;
        _logger = logger;
    }

    public async Task<IActionResult> Index(){
        var products = await _logic.GetAllProducts();
        return View(products);
    }

    public async Task<IActionResult> Details(int id){
        var product = await _logic.GetProductById(id);
        if (product == null){
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        } 

        return View(product);
    }

    public IActionResult Create()
    {
        return View();
    }

    // POST: ProductsDb/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,IsActive")] 
                                            ProductModel product)
    {
        if (ModelState.IsValid)
        {
            await _logic.AddNewProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: ProductsDb/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null){
            _logger.LogInformation("No id passed for edit {id}", id);
            return View("NotFound");
        }
        
        var productModel = await _logic.GetProductById(id.Value);
        if (productModel == null)
        {
            _logger.LogInformation("Edit Details not found for id {id}", id);
            return View("NotFound");
        }
        return View(productModel);
    }

    // POST: ProductsDb/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsActive")] ProductModel product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _logic.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
    
        // GET: ProductsDb/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var productModel = await _logic.GetProductById(id.Value);
        if (productModel == null) return NotFound();
        
        return View(productModel);
    }
    // POST: ProductsDb/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }                

    // private List<ProductModel>? GetSampleProducts(){
    //     return new List<ProductModel>{
    //         new ProductModel {Id = 1, Name = "Tr", Price = 69.99M, IsActive = true, Description = "Great"},
    //         new ProductModel {Id = 2, Name = "dd", Price = 69.99M, IsActive = true, Description = "Great dddd"},
    //     };
    // }
}