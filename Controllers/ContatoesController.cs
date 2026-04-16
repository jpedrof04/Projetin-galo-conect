using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Galoto.Data;
using Galoto.Models;

namespace Galoto.Controllers;

public class ContatoesController : Controller
{
    private readonly AppDbContext _context;

    public ContatoesController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Contatos.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Email")] Contato contato)
    {
        if (ModelState.IsValid)
        {
            _context.Add(contato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contato);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null)
            return NotFound();

        return View(contato);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,Email")] Contato contato)
    {
        if (id != contato.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(contato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contato);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var contato = await _context.Contatos.FindAsync(id);
        if (contato == null)
            return NotFound();

        return View(contato);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contato = await _context.Contatos.FindAsync(id);
        if (contato != null)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}