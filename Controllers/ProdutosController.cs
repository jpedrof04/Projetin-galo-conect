using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galoto.Data;
using Galoto.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Galoto.Controllers;

public class ProdutosController : Controller
{
    private readonly AppDbContext _context;

    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        return View(produtos);
    }

    public IActionResult Create()
    {
        ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Preco,CategoriaId")] Produto produto)
    {
        if (ModelState.IsValid)
        {
            _context.Add(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
        return View(produto);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
            return NotFound();

        ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,CategoriaId")] Produto produto)
    {
        if (id != produto.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Descricao", produto.CategoriaId);
        return View(produto);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var produto = await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(m => m.Id == id);
        if (produto == null)
            return NotFound();

        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}